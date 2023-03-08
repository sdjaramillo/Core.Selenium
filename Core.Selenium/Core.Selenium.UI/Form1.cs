using Core.Selenium.Logic;
using Core.Selenium.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Security.Policy;

namespace Core.Selenium.UI
{
    public partial class Form1 : Form
    {
        private string URL
        {
            get
            {
                return _txtUrl.Text;
            }
        }
        public string NombreTest
        {
            get
            {
                return _txtNombreTest.Text;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento carga inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ObtenerArbolScripts();
        }

        /// <summary>
        /// Metodo para cargar información de un archivo excel en un grid
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="grid"></param>
        /// <param name="hasheader"></param>
        private void LeerExcel(string fname, DataGridView grid, bool hasheader = false)
        {
            try
            {
                grid.Rows.Clear();
                grid.Columns.Clear();
                //provide file path
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo($"{fname}");
                //use EPPlus
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count                        

                    //ObtenerCabeceraDinamica
                    int cabecera = 1;
                    for (int i = 1; i <= colCount; i++)
                    {
                        var celda = worksheet.Cells[cabecera, i];
                        grid.Columns.Add(celda.Value.ToString(), celda.Value.ToString());
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var rowNueva = grid.Rows.Add();

                        for (int col = 1; col <= colCount; col++)
                        {
                            var celdaGrid = grid[col - 1, rowNueva];
                            celdaGrid.Value = worksheet.Cells[row, col].Value?.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        /// <summary>
        /// Metodo recursivo para leer scripts/carpetas de un directorio
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nodo"></param>
        public void LoadTreeView(string path, TreeNode nodo)
        {
            var archivosNodo = Directory.GetFiles(path);
            var carpetas = Directory.GetDirectories(path);

            foreach (var arc in archivosNodo)
            {
                nodo.Nodes.Add(arc);
            }

            foreach (var carp in carpetas)
            {
                var nodoHijo = nodo.Nodes.Add(carp);
                LoadTreeView(carp, nodoHijo);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                LeerExcel(fd.FileName, _grdDatosExternos);
            }
        }

        private void eliminarFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fila = _grdScript.SelectedRows;

            foreach (DataGridViewRow f in fila)
            {
                _grdScript.Rows.Remove(f);
            }
        }

        private void agregarFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _grdScript.Rows.Add();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObtenerArbolScripts();
        }

        public void ObtenerArbolScripts()
        {
            if (Directory.Exists("Scripts"))
            {
                var nodoRoot = new TreeNode("Scripts");
                LoadTreeView("Scripts", nodoRoot);
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(nodoRoot);
            }
            else
            {
                Directory.CreateDirectory("Scripts");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 0)
            {
                try
                {
                    using (var sr = new StreamReader(e.Node.Text))
                    {
                        var scriptText = sr.ReadToEnd();
                        _txtJson.Text = scriptText;

                        ScriptBase script = JsonConvert.DeserializeObject<ScriptBase>(_txtJson.Text);
                        CargarScriptData(script);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private ScriptBase _scriptBase { get; set; }
        private void CargarScriptData(ScriptBase scriptBase)
        {
            _scriptBase = scriptBase;
            _grdScript.DataSource = _scriptBase.Comandos;
            _txtNombreTest.Text = _scriptBase.ScriptData.DataTest[0].NombrePrueba;
            _txtUrl.Text = _scriptBase.ScriptData.Url;
            CargarDatosPrueba(_scriptBase.ScriptData.DataTest[0].TestsVars);
            _grdSuites.Rows.Clear();
            _scriptBase.ScriptData.DataTest[0].SuiteVars.ToList().ForEach(x =>
            {
                _grdSuites.Rows.Add(x.Key, x.Value);
            });
        }

        private void CargarDatosPrueba(List<Dictionary<string, string>> diccionario)
        {
            var columnas = (from cols in diccionario
                            from col in cols
                            select col.Key).Distinct().ToList();
            _grdDatosExternos.Columns.Clear();
            columnas.ForEach(f => { _grdDatosExternos.Columns.Add(f, f); });


            foreach (var row in diccionario)
            {
                var datosfila = row;
                var indexAddRow = _grdDatosExternos.Rows.Add();

                foreach (DataGridViewColumn col in _grdDatosExternos.Columns)
                {
                    if (datosfila.ContainsKey(col.Name))
                    {
                        _grdDatosExternos.Rows[indexAddRow].Cells[col.Index].Value = $"{datosfila[col.Name]}";
                    }
                }
            }
        }

        private void GenerarScript(string driver = "chrome")
        {
            var commands = (List<Comando>)_grdScript.DataSource;
            commands.ForEach(cmd =>
            {
                cmd.Orden = commands.IndexOf(cmd);
                cmd.Tipo = string.IsNullOrEmpty(cmd.Tipo) ? "comando" : cmd.Tipo;
            });


            Dictionary<string, string> variablesInicio = new Dictionary<string, string>();
            foreach (DataGridViewRow rw in _grdSuites.Rows)
            {
                var key = rw.Cells[clmKey.Index].Value;
                var value = rw.Cells[clmValue.Index].Value;

                if (key != null && value != null)
                {
                    variablesInicio.Add(key.ToString(), value.ToString());
                }
            }

            List<Dictionary<string, string>> VariablesPruebas = new List<Dictionary<string, string>>();
            foreach (DataGridViewRow row in _grdDatosExternos.Rows)
            {
                var variablesPrueba = new Dictionary<string, string>();
                foreach (DataGridViewCell celda in row.Cells)
                {
                    var columna = celda.ColumnIndex;
                    var valor = celda.Value;

                    if (valor != null)
                        variablesPrueba.Add(celda.OwningColumn.HeaderText, valor?.ToString());
                }
                VariablesPruebas.Add(variablesPrueba);
            }



            var script = new ScriptBase
            {
                Nombre = NombreTest,
                Comandos = (List<Comando>)_grdScript.DataSource,
                ScriptData = new ParametroScript
                {
                    Url = URL,
                    Driver = driver,
                    DataTest = new DataTest[] {
                                                new DataTest{
                                                NombrePrueba = NombreTest,
                                                SuiteVars= variablesInicio,
                                                TestsVars= VariablesPruebas
                 }
                 }
                }
            };


            var json = JsonConvert.SerializeObject(script);
            JToken parsedJson = JToken.Parse(json);
            json = parsedJson.ToString(Formatting.Indented);
            _txtJson.Text = json;
        }

        /// <summary>
        /// Acciones menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerarScript();
            EjecutarScript(_txtJson.Text);
        }

        public void EjecutarScript(string jsonScript)
        {

            ScriptBase script = JsonConvert.DeserializeObject<ScriptBase>(_txtJson.Text);
            LogicTemplate.EjecutarScript(script);
        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerarScript("edge");
            EjecutarScript(_txtJson.Text);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter(treeView1.SelectedNode.Text))
            {
                sw.Write(_txtJson.Text);
            }
        }

        private void generarScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerarScript();
        }
        //FIN MENUS
    }
}