using MaterialSkin;
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
    public partial class FrmVoteMaterial : MaterialSkin.Controls.MaterialForm
    {
        MaterialSkinManager skinManager;    
        public FrmVoteMaterial()
        {
            InitializeComponent();
            skinManager=MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void FrmVoteMaterial_Load(object sender, EventArgs e)
        {

        }

        private void Switch_CheckedChanged(object sender, EventArgs e)
        {
            Switch.Text = "Dark";

        }
    }
}
