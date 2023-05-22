using ChamaLibrary.DataAccess;
using ChamaLibrary.Models;
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
    public partial class Frm_Login : Form
    {
        private Frm_Main _Main;

        private void LoadRoles()
        {
            List<FinYear> FinYears = new List<FinYear>();
            FinYears = SqliteDataAccess.GetALLFinYears();
            if (FinYears != null && FinYears.Count > 0)
            {
                cboFinYear.DisplayMember = "YearName";
                cboFinYear.ValueMember = "Id";
                cboFinYear.DataSource = FinYears;
            }
        }
            public Frm_Login(Frm_Main Main)
        {
            InitializeComponent();
            _Main = Main;   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            appClosing();       
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //perform checks for validity of entries
                if (string.IsNullOrWhiteSpace(cboFinYear.Text.Trim()))
                {
                    MessageBox.Show("Please Select the Financial Year", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboFinYear.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
                {
                    MessageBox.Show("Please Enter a Valid Username", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Please Enter a Valid Password", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                else
                {

                    if (SqliteDataAccess.VerifyUserLogin(SqliteDataAccess.ToPropercase(txtUserName.Text.Trim()), txtPassword.Text.Trim(),(int)cboFinYear.SelectedValue))
                    {
                        MessageBox.Show("WELCOME " + txtUserName.Text.Trim(), "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Close();

                        DialogResult = DialogResult.OK;

                    }
                    else
                    {
                        MessageBox.Show("Wrong Password or Username", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Focus();
                        return;
                    }

                }

            }
            catch (Exception)
            {
                AppCloser.CloseApp();
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            appClosing();
        }
        private void appClosing()
        {
            DialogResult Res = MessageBox.Show("Application will close. Are you sure?? ", "@Chamaz", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Res == DialogResult.Yes)
            {
                AppCloser.CloseApp();
            }
            else
            {
                txtPassword.Focus();
            }
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }
    }
}
