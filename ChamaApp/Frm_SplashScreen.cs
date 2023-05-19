using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamaApp
{
    public partial class Frm_SplashScreen : Form
    {
        public Frm_SplashScreen()
        {
            InitializeComponent();
        }

        private void Frm_SplashScreen_Load(object sender, EventArgs e)
        {

            splashTimer.Start();
        }

        private void splashTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
