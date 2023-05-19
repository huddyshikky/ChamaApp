using ChamaLibrary.DataAccess;
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
                if (string.IsNullOrWhiteSpace(cboUserName.Text.Trim()))
                {
                    MessageBox.Show("Please Select the Username", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboUserName.Focus();
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

                    if (SqliteDataAccess.VerifyUserLogin(cboUserName.Text.Trim(), txtPassword.Text.Trim()))
                    {
                        //            MessageBox.Show("WELCOME " + CboAccountname.Text.Trim() + "", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //            SqlDataFunctions.Uname = CboAccountname.Text.Trim();
                        //            SqlDataFunctions.FullName = CboAccountFirstName.Text.Trim() + " " + CboAccountLastName.Text.Trim();
                        //            SqlDataFunctions.CurYear = CboYear.Text.Trim();
                        //            SqlDataFunctions.SetSMSCredentials();
                        //            //SqlDataFunctions.SetFinancialYearDates();
                        //            SqlDataFunctions.GetSchoolGender();
                        //            SqlDataFunctions.GetSchoolCategory(); //primary or secondary school
                        //            SqlDataFunctions.GetSchoolType(); //Day or Boarding

                        //            //Check Version of software available
                        //            int Version_ = SqlDataFunctions.CurVersion = SqlDataFunctions.CheckVersion();

                        //            //if version is less then upgrade
                        //            if (SqlDataFunctions.UpgradeSystem() > 0)
                        //            {
                        //                MessageBox.Show("There were Errors during system upgrade to current version.Please consult us on 0725 319 665", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //            }




                        //            Close();


                        //            if (SqlDataFunctions.LogOut)
                        //            {
                        //                DialogResult = DialogResult.Yes;
                        //            }
                        //            else
                        //            {
                                        DialogResult = DialogResult.OK;
                        //            }

                        //            //Enable all menus first
                        //            _MDIStart.EnableMenus();

                        //            //get all disabled menus for the current user
                        //            _MDIStart.DisableMenus();



                        //            //if maintenance has not been catered for then
                        //            if (SqlDataFunctions.S_Maintenance_Code.StartsWith("Fatal_"))
                        //            {
                        //                //Fatal_EmptyOrNull //dates are absent or zero
                        //                //Fatal_Clock //system clock rolled back
                        //                //Fatal_Expired //maintenance expired
                        //                //Fatal_Unupdated_LR //failed to update last read date
                        //                //Fatal_Terminate //something wrong on system data

                        //                // MessageBox.Show(SqlDataFunctions.S_Maintenance_Code + ": Maintenance contract fee NOT PAID. System will hibernate.Please consult us on 0725 319 665", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //            }
                        //            else
                        //            {
                        //                //get the number of days if less than 60 days

                        //                if (int.TryParse(SqlDataFunctions.S_Maintenance_Code, out _))
                        //                {
                        //                    if (int.Parse(SqlDataFunctions.S_Maintenance_Code) < 61)
                        //                    {
                        //                        MessageBox.Show(SqlDataFunctions.S_Maintenance_Code + "Days Remaining for you to honor the maintenance contract fee. System will hibernate thereafter if not paid", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //                    }
                        //                }

                        //            }
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password or Username", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult Res = MessageBox.Show("Application will close. Are you sure?? ", "Esent@ Accountant", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Res == DialogResult.Yes)
            {
                AppCloser.CloseApp();
            }
            else
            {
                txtPassword.Focus();
            }
        }
    }
}
