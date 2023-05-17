using ChamaLibrary.DataAccess;
using ChamaLibrary.Models;
using ChamaLibrary.ViewModel;
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
    public partial class Frm_Accounts : Form
    {
        private bool EditMode = false;

        private List<Account> Accounts = null;
        private List<AccountVM> AccountsView = null;
        private void LoadAccounts()
        {
            AccountsView = SqliteDataAccess.GetALLAccounts();
            if (AccountsView.Count>0)
            {
                dtgAccounts.DataSource = AccountsView;
                dtgAccounts.Columns[0].Visible = false;
                dtgAccounts.Columns[2].Visible = false;
                dtgAccounts.Columns[3].HeaderText = "Bank";
                dtgAccounts.Columns[3].Width = 60;

            }
           
        }
        private void LoadBanks()
        {
            List<Bank> Banks = new List<Bank>();
            Banks = SqliteDataAccess.GetALLBanks();
            if (Banks != null && Banks.Count > 0)
            {
                cboBank.DisplayMember = "BankName";
                cboBank.ValueMember = "Id";
                cboBank.DataSource = Banks;
            }
        }
        public Frm_Accounts()
        {
            InitializeComponent();
        }

        private void Frm_Accounts_Load(object sender, EventArgs e)
        {
            LoadBanks();
            ShowAllPanel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAddEditPanel(true);
        }
        private void ShowAllPanel()
        {
            LoadAccounts();

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
            txtAccountName.Text = "";
            if(cboBank.Items.Count>0) { cboBank.SelectedIndex = 1; }
            txtBranch.Text = "";
            txtAccountNo.Text = "";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtAccountName.Text.Trim()))
            {
                MessageBox.Show("AccountName Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtAccountName.Focus();
                return;
            }

            //if (SqliteDataAccess.CheckIfAExist(txtAccountName.Text.Trim(), Convert.ToInt32(txtId.Text)))
            //{
            //    MessageBox.Show("AccountName is already available", "@Chamaz", MessageBoxButtons.OK);
            //    txtAccountName.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(cboBank.Text.Trim()))
            {
                MessageBox.Show("Account Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                cboBank.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtBranch.Text.Trim()))
            {
                MessageBox.Show("Account Branch Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtBranch.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
            {
                MessageBox.Show("Account Number Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtAccountNo.Focus();
                return;
            }
           



            AccountVM Account = new AccountVM()
            {
                Id = Convert.ToInt32(txtId.Text),
                AccountName = SqliteDataAccess.ToPropercase(txtAccountName.Text),
                BankId = (int)cboBank.SelectedValue,
                Branch = SqliteDataAccess.ToPropercase(txtBranch.Text),
                AccountNumber = SqliteDataAccess.ToPropercase(txtAccountNo.Text),
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateAccount(Account) > 0)
                {
                    MessageBox.Show($"Account : {txtAccountName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update Account : {txtAccountName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertAccount(Account) > 0)
                {
                    MessageBox.Show($"Account : {txtAccountName.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add Account : {txtAccountName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
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
            if (MessageBox.Show($"Are you sure to detete Account :  {txtAccountName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteAccount(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Account : {txtAccountName.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Account : {txtAccountName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }

        private void dtgAccounts_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgAccounts.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString();
            txtAccountName.Text = dtgrow.Cells[1].Value.ToString();
            cboBank.SelectedValue = (int)dtgrow.Cells[2].Value;
            txtBranch.Text = dtgrow.Cells[4].Value.ToString();
            txtAccountNo.Text = dtgrow.Cells[5].Value.ToString();
            ShowAddEditPanel(false);
        }
    }
}
