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
    public partial class Frm_Banks : Form
    {
        private bool EditMode = false;

        private List<Bank> Banks = null;
        private void LoadBanks()
        {
            Banks = SqliteDataAccess.GetALLBanks();
            dtgBanks.DataSource = Banks;
            dtgBanks.Columns[0].Visible = false;
        }

        public Frm_Banks()
        {
            InitializeComponent();
        }

        private void Frm_Banks_Load(object sender, EventArgs e)
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
            txtBankName.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
            {
                MessageBox.Show("BankName Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtBankName.Focus();
                return;
            }

            if (!EditMode && SqliteDataAccess.CheckIfBankExist(txtBankName.Text.Trim(), Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("BankName has already been registered", "@Chamaz", MessageBoxButtons.OK);
                txtBankName.Focus();
                return;
            }

            //check if votename exists


            Bank Bank = new Bank()
            {
                Id = Convert.ToInt32(txtId.Text),
                BankName = SqliteDataAccess.ToPropercase(txtBankName.Text),
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateBank(Bank) > 0)
                {
                    MessageBox.Show($"Bank : {txtBankName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update Bank : {txtBankName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertBank(Bank) > 0)
                {
                    MessageBox.Show($"Bank : {txtBankName.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add Bank : {txtBankName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
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
            if (MessageBox.Show($"Are you sure to delete Bank :  {txtBankName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SqliteDataAccess.CheckIfBankIsReferenced(Convert.ToInt32(txtId.Text.Trim())))
                {
                    MessageBox.Show($"Bank cannot be Deleted because it is used by account Details", "@Chamaz", MessageBoxButtons.OK);
                    return;
                }
                if (SqliteDataAccess.DeleteBank(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Bank : {txtBankName.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Bank : {txtBankName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }


        private void dtgBanks_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgBanks.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString(); ;
            txtBankName.Text = dtgrow.Cells[1].Value.ToString();
            ShowAddEditPanel(false);
        }
    }
}
