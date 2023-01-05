namespace Core.BancaDigital
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var chrome = new ChromeDriver("drivers");

            DriverTemplate.EjecutarScript(new ValidarCredencialesIncorrectas(), chrome);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }
    }
}