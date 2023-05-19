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
    public partial class Frm_Users : Form
    {
        private bool EditMode = false;

        private List<User> Users = null;
        private void LoadBanks()
        {
            Users = SqliteDataAccess.GetALLUsers();
            dtgUsers.DataSource = Users;
            dtgUsers.Columns[0].Visible = false;
        }
        public Frm_Users()
        {
            InitializeComponent();
        }

        private void Frm_Users_Load(object sender, EventArgs e)
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
            LoadBanks();

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
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtTelephone.Text = "";
            txtUserName.Text = "";
            txtPassWord.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                MessageBox.Show("First Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtFirstName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                MessageBox.Show("Last Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtLastName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtTelephone.Text.Trim()))
            {
                MessageBox.Show("Telephone Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtTelephone.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBox.Show("User Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassWord.Text.Trim()))
            {
                MessageBox.Show("Pass Word Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtPassWord.Focus();
                return;
            }

            if (SqliteDataAccess.CheckIfUserExist(txtUserName.Text.Trim()))
            {
                MessageBox.Show("User Name has already been registered", "@Chamaz", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }

           
            //

            User User = new User()
            {
                Id = Convert.ToInt32(txtId.Text),
                FirstName = SqliteDataAccess.ToPropercase(txtFirstName.Text),
                LastName = SqliteDataAccess.ToPropercase(txtLastName.Text),
                UserName = SqliteDataAccess.ToPropercase(txtUserName.Text),
                PassWord = SqliteDataAccess.ToPropercase(txtPassWord.Text),
                Telephone = SqliteDataAccess.ToPropercase(txtTelephone.Text),
                
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateUser(User) > 0)
                {
                    MessageBox.Show($"User : {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update User : {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} ", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertUser(User) > 0)
                {
                    MessageBox.Show($"User : {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add User : {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} ", "@Chamaz", MessageBoxButtons.OK);
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
            if (MessageBox.Show($"Are you sure to detete User :  {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} ", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SqliteDataAccess.CheckIfUserIsReferenced(Convert.ToInt32(txtId.Text.Trim())))
                {
                    MessageBox.Show($"User cannot be Deleted because he/she is used by other entities", "@Chamaz", MessageBoxButtons.OK);

                    return;
                }
                if (SqliteDataAccess.DeleteUser(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"User : {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete User : {txtFirstName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }

        private void dtgUsers_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgUsers.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString(); 
            txtFirstName.Text = dtgrow.Cells[1].Value.ToString();
            txtLastName.Text = dtgrow.Cells[2].Value.ToString();
            txtUserName.Text = dtgrow.Cells[3].Value.ToString();
            txtPassWord.Text = dtgrow.Cells[4].Value.ToString();
            txtTelephone.Text = dtgrow.Cells[5].Value.ToString();
            ShowAddEditPanel(false);
        }
    }
}
