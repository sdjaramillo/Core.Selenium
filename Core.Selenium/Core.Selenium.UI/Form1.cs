using Core.Selenium.Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Security.Policy;

namespace Core.Selenium.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Filter = "|*.side";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(fd.FileName))
                {
                    var json = sr.ReadToEnd();
                    SeleniumScript myDeserializedClass = JsonConvert.DeserializeObject<SeleniumScript>(json);
                    _grdScript.DataSource = myDeserializedClass.tests[0].commands;
                }
            }
        }

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            var script = new ScriptBase();
            script.Nombre = _txtNombrePrueba.Text;
            

            var commands = (List<Comando>)_grdScript.DataSource;
            commands.ForEach(cmd =>
            {
                cmd.Orden = commands.IndexOf(cmd);
                cmd.Tipo = string.IsNullOrEmpty(cmd.Tipo) ? "comando" : cmd.Tipo;
            });
            script.Comandos = commands;
            script.ScriptData = new ParametroScript();

            List<Tuple<string, string, string>> VariablesInicio = new List<Tuple<string, string, string>>();

            foreach (DataGridViewRow row in _grdDatosExternos.Rows)
            {
                var key = row.Cells[clmKey.Index].Value ?? string.Empty;
                var valor = row.Cells[clmValue.Index].Value ?? string.Empty;
                var test = row.Cells[clmTestInicio.Index].Value ?? string.Empty;

                if (!string.IsNullOrEmpty(key.ToString()) && !string.IsNullOrEmpty(valor.ToString()) && !string.IsNullOrEmpty(test.ToString()))
                {
                    VariablesInicio.Add(new Tuple<string, string, string>(key.ToString(), valor.ToString(), test.ToString()));
                }
            }

            if (VariablesInicio.Count > 0)
            {

            }
            else
            {
                script.ScriptData.DataTest = new DataTest[] {
                    new DataTest{

                    }
                };
            }

            _grdScript.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                LeerExcel(fd.FileName, _grdDatosExternos);
            }
        }
    }
}