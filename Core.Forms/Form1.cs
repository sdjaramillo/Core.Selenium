using BancaDigitalSelenium;
using BancaDigitalSelenium.Scripts;
using BancaDigitalSelenium.Scripts.Afiliacion;
using BancaDigitalSelenium.Scripts.Beneficiarios;
using BancaDigitalSelenium.Scripts.RecuperarContrasena;
using BancaDigitalSelenium.Scripts.Transferencias;
using Core.Models.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int IndexScript = 0;
        private IWebDriver Driver;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndexScript = listBox1.SelectedIndex;

            switch (IndexScript)
            {
                case 0:
                    using (StreamReader sr = new StreamReader($"scripts/Afiliacion/AfiliacionScript.json"))
                    {
                        this.richTextBox1.Text = sr.ReadToEnd();
                    }

                    break;
                case 1:
                    using (StreamReader sr = new StreamReader($"scripts/RecuperarContrasena/RecuperarUsuarioScript.json"))
                    {
                        this.richTextBox1.Text = sr.ReadToEnd();
                    }

                    break;
                case 2:
                    using (StreamReader sr = new StreamReader($"scripts/Transferencias/TransferenciaInternaScript.json"))
                    {
                        this.richTextBox1.Text = sr.ReadToEnd();
                    }

                    break;
                case 3:
                    using (StreamReader sr = new StreamReader($"scripts/RecuperarContrasena/RecuperarContrasenaScript.json"))
                    {
                        this.richTextBox1.Text = sr.ReadToEnd();
                    }

                    break;
                case 4:
                    using (StreamReader sr = new StreamReader($"scripts/Beneficiarios/AgregarBeneficiarioScript.json"))
                    {
                        this.richTextBox1.Text = sr.ReadToEnd();
                    }

                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (IndexScript)
            {
                case 0:
                    Driver = new EdgeDriver();
                    DriverTemplate.EjecutarScript(new AfiliacionScript() { PathGuardado = _txtPath.Text}, Driver);
                    break;

                case 1:
                    Driver = new EdgeDriver();
                    DriverTemplate.EjecutarScript(new RecuperarUsuarioScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;

                case 2:
                    Driver = new EdgeDriver();
                    DriverTemplate.EjecutarScript(new TransferenciaInternaScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;
                case 3:
                    Driver = new EdgeDriver();
                    DriverTemplate.EjecutarScript(new RecuperarContrasenaScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;
                case 4:
                    Driver = new EdgeDriver();
                    DriverTemplate.EjecutarScript(new AgregarBeneficiarioScript() { PathGuardado = _txtPath.Text }, Driver);
                    break;
            }
            MessageBox.Show("Ejecución Finalizada");
        }

        private void btnGuardarJson_Click(object sender, EventArgs e)
        {
            var jsonNuevo = richTextBox1.Text;

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
    }
}