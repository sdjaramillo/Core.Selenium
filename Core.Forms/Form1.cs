using BancaDigitalSelenium;
using BancaDigitalSelenium.Scripts;
using BancaDigitalSelenium.Scripts.Afiliacion;
using BancaDigitalSelenium.Scripts.Beneficiarios;
using BancaDigitalSelenium.Scripts.RecuperarContrasena;
using BancaDigitalSelenium.Scripts.Transferencias;
using Core.Config;
using Core.Models;
using Core.Models.Entidad;
using Core.Models.Entidad.Script;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Reflection;

namespace Core.Forms
{
    public partial class Form1 : Form
    {

        private int IndexScript = 0;
        private IWebDriver Driver;
        private IJavaScriptExecutor JS;
        private List<Script> ScriptList = new List<Script>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetConfig();
            LoadScripts();
        }

        private void LoadScripts()
        {
            _lstScripts.DisplayMember = "nombre";
            using (StreamReader sr = new StreamReader($"{AppConfig.ScriptListJson}"))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                var ScriptList = json.SelectToken("scripts").ToObject<List<Script>>();
                _lstScripts.DisplayMember = "nombre";
                _lstScripts.DataSource = ScriptList;
            }
        }

        private void SetConfig()
        {
            AppConfig.TimeOut = Properties.Settings.Default.TimeOut;
            AppConfig.ScriptListJson = Properties.Settings.Default.ScriptList;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var scriptSeleccionado = (Script)_lstScripts.SelectedItem;

            try
            {
                using (StreamReader sr = new StreamReader($"{scriptSeleccionado.Json}"))
                {
                    this._txtJson.Text = sr.ReadToEnd();

                    var tipo = ModelHelper.GetObject(scriptSeleccionado.Squema);
                    ScriptParametroBase instance = (ScriptParametroBase)Activator.CreateInstance(tipo);

                    JObject json = JObject.Parse(sr.ReadToEnd());
                    //var TestData = json.SelectToken("data").ToObject(tipo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Driver = GetSelectedDriver();
            switch (IndexScript)
            {
                case 0:
                    DriverTemplate.EjecutarScript(new AfiliacionScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;

                case 1:
                    DriverTemplate.EjecutarScript(new RecuperarUsuarioScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;

                case 2:
                    DriverTemplate.EjecutarScript(new TransferenciaInternaScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;
                case 3:
                    DriverTemplate.EjecutarScript(new RecuperarContrasenaScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;
                case 4:
                    DriverTemplate.EjecutarScript(new AgregarBeneficiarioScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;
            }
            MessageBox.Show("Ejecución Finalizada");
        }

        private void btnGuardarJson_Click(object sender, EventArgs e)
        {
            var jsonNuevo = _txtJson.Text;

            switch (IndexScript)
            {
                case 0:
                    using (StreamWriter sw = new StreamWriter($"scripts/Afiliacion/AfiliacionScript.json"))
                    {
                        sw.Write(jsonNuevo);

                    }
                    MessageBox.Show("Guardado!");
                    break;
                case 1:
                    using (StreamWriter sw = new StreamWriter($"scripts/RecuperarContrasena/RecuperarUsuarioScript.json"))
                    {
                        sw.Write(jsonNuevo);
                    }
                    MessageBox.Show("Guardado!");
                    break;
                case 2:
                    using (StreamWriter sw = new StreamWriter($"scripts/Transferencias/TransferenciaInternaScript.json"))
                    {
                        sw.Write(jsonNuevo);
                    }
                    MessageBox.Show("Guardado!");
                    break;
                case 3:
                    using (StreamWriter sw = new StreamWriter($"scripts/RecuperarContrasena/RecuperarContrasenaScript.json"))
                    {
                        sw.Write(jsonNuevo);
                    }
                    MessageBox.Show("Guardado!");
                    break;
                case 4:
                    using (StreamWriter sw = new StreamWriter($"scripts/Beneficiarios/AgregarBeneficiarioScript.json"))
                    {
                        sw.Write(jsonNuevo);
                    }
                    MessageBox.Show("Guardado!");
                    break;
            }

        }

        private void _txtPath_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();
            _txtPath.Text = fd.ShowDialog() == DialogResult.OK ? $@"{fd.SelectedPath}\" : _txtPath.Text;
        }

        private void _txtScript_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)System.Windows.Forms.Keys.Enter)
            {

                var result = JS.ExecuteScript(_txtJson.Text);
                _txtJson.Text = result.ToString();
            }
        }

        private void _btnDebug_Click(object sender, EventArgs e)
        {
            Driver = new EdgeDriver();
            JS = (IJavaScriptExecutor)Driver;
        }

        private IWebDriver GetSelectedDriver()
        {
            IWebDriver driver = null;
            if (_rdEdge.Checked)
            {
                driver = new EdgeDriver("drivers");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(AppConfig.TimeOut); ;
                return driver;
            }
            if (_rdChrome.Checked)
            {
                driver = new ChromeDriver("drivers");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(AppConfig.TimeOut); ;
                return driver;
            }

            throw new Exception("Seleccione Driver");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void _rdJson_CheckedChanged(object sender, EventArgs e)
        {
            if (_rdJson.Checked)
            {
                _txtJson.Visible = true;
                btnGuardarJson.Visible = true;

                _grdExcel.Visible = false;
            }
        }

        private void _rdExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (_rdExcel.Checked)
            {
                try
                {
                    _txtJson.Visible = false;
                    btnGuardarJson.Visible = false;

                    _grdExcel.Visible = true;

                    GetSelectedSquema();
                    LeerExcel("", _grdExcel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void GetSelectedSquema()
        {
            if (_lstScripts.SelectedItems.Count > 0)
            {


                return;
            }
            throw new Exception("Seleccione un script");
        }

        private void LeerExcel(string fname, DataGridView grid, bool hasheader = false)
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    _grdExcel.Rows.Clear();
                    _grdExcel.Columns.Clear();
                    //provide file path
                    FileInfo existingFile = new FileInfo($"{fd.FileName}");
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
                            _grdExcel.Columns.Add(celda.Value.ToString(), celda.Value.ToString());
                        }

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var rowNueva = _grdExcel.Rows.Add();

                            for (int col = 1; col <= colCount; col++)
                            {
                                var celdaGrid = _grdExcel[col - 1, rowNueva];
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
        }
    }
}