using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamaApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_SplashScreen());

            Frm_Login fLogin = new Frm_Login(new Frm_Main());

            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Frm_Main());
            }
            else
            {
                Application.Exit();
            }

        }
    }
}
