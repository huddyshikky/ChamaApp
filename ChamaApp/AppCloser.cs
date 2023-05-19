using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamaApp
{
    public class AppCloser
    {
        public static void CloseApp()
        {
            if (Application.MessageLoop)
            {
                Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }
    }
}
