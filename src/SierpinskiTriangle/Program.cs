namespace SierpinskiTriangle
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    using SierpinskiTriangle.Lang;

    internal static class Program
    {
        #region Static Fields

        private static readonly Mutex _mutex = new Mutex(
            true,
            string.Format(
                @"Global\{0}.{1}.{2}",
                Application.CompanyName,
                Application.ProductName,
                typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]));

        #endregion

        #region Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Controller.Init();
            }
            else
            {
                MessageBox.Show(
                    CoreLang.MessageBox_Text_App_Already_Running,
                    CoreLang.MessageBox_Caption_Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}