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
    public partial class Frm_UserRole : Form
    {
        private bool EditMode = false;

        private List<UserRole> UserRoles = null;
        private void LoadUserRoles()
        {
            UserRoles = SqliteDataAccess.GetALLUserRoles();
            dtgUserRoles.DataSource = UserRoles;
            dtgUserRoles.Columns[0].Visible = false;
        }
        public Frm_UserRole()
        {
            InitializeComponent();
        }

        private void Frm_UserRole_Load(object sender, EventArgs e)
        {

            ShowAllPanel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAddEditPanel(true);
        }
        private void ShowAllPanel()
        {
            LoadUserRoles();

            BankAddEditPanel.Visible = false;
            BankShowAllPanel.Visible = true;
            BankShowAllPanel.Left = (MainGBox.Width - BankShowAllPanel.Width) / 2;
            BankShowAllPanel.Top = (MainGBox.Height - BankShowAllPanel.Height) / 2;
        }
        private void ShowAddEditPanel(bool Add)
        {
            if (Add)
            {
                btnSave.Text = "Save";
                btnDelete.Visible = false;
                EditMode = false;
            }
            else
            {
                btnSave.Text = "Update";
                btnDelete.Visible = true;
                EditMode = true;
            }

            BankShowAllPanel.Visible = false;
            BankAddEditPanel.Visible = true;
            BankAddEditPanel.Left = (MainGBox.Width - BankAddEditPanel.Width) / 2;
            BankAddEditPanel.Top = (MainGBox.Height - BankAddEditPanel.Height) / 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
        }
        private void ClearFields()
        {
            txtId.Text = "0";
            txtUserRole.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtUserRole.Text.Trim()))
            {
                MessageBox.Show("First Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtUserRole.Focus();
                return;
            }
            
            if (!EditMode && SqliteDataAccess.CheckIfUserRoleExist(txtUserRole.Text.Trim()))
            {
                MessageBox.Show("User Role has already been registered", "@Chamaz", MessageBoxButtons.OK);
                txtUserRole.Focus();
                return;
            }


            //

            UserRole UserRole = new UserRole()
            {
                Id = Convert.ToInt32(txtId.Text),
                Role = SqliteDataAccess.ToPropercase(txtUserRole.Text),

            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateUserRole(UserRole) > 0)
                {
                    MessageBox.Show($"UserRole : {txtUserRole.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update User Role : {txtUserRole.Text.Trim()} ", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertUserRole(UserRole) > 0)
                {
                    MessageBox.Show($"User Role : {txtUserRole.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add User Role : {txtUserRole.Text.Trim()} ", "@Chamaz", MessageBoxButtons.OK);
                }
            }

            ShowAllPanel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to delete User Role :  {txtUserRole.Text.Trim()} ", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SqliteDataAccess.CheckIfUserRoleIsReferenced(Convert.ToInt32(txtId.Text.Trim())))
                {
                    MessageBox.Show($"User Role cannot be Deleted because it is assigned to User(s)", "@Chamaz", MessageBoxButtons.OK);

                    return;
                }
                if (SqliteDataAccess.DeleteUserRole(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"User Role : {txtUserRole.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete User Role : {txtUserRole.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }

        private void dtgUserRoles_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgUserRoles.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString();
            txtUserRole.Text = dtgrow.Cells[1].Value.ToString();
            
            ShowAddEditPanel(false);
        }
    }
}
