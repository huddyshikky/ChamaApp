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
    public partial class Frm_LoanTypes : Form
    {
        private bool EditMode = false;

        private List<LoanType> LoanTypes = null;
        private void LoadLoanTypes()
        {
            LoanTypes = SqliteDataAccess.GetALLLoanTypes();
            dtgLoanType.DataSource = LoanTypes;
            dtgLoanType.Columns[0].Visible = false;
        }
        public Frm_LoanTypes()
        {
            InitializeComponent();
        }

        private void Frm_LoanTypes_Load(object sender, EventArgs e)
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
            LoadLoanTypes();

            LoanTypeAddEditPanel.Visible = false;
            LoanTypeShowAllPanel.Visible = true;
            LoanTypeShowAllPanel.Left = (MainGBox.Width - LoanTypeShowAllPanel.Width) / 2;
            LoanTypeShowAllPanel.Top = (MainGBox.Height - LoanTypeShowAllPanel.Height) / 2;
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

            LoanTypeShowAllPanel.Visible = false;
            LoanTypeAddEditPanel.Visible = true;
            LoanTypeAddEditPanel.Left = (MainGBox.Width - LoanTypeAddEditPanel.Width) / 2;
            LoanTypeAddEditPanel.Top = (MainGBox.Height - LoanTypeAddEditPanel.Height) / 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
        }
        private void ClearFields()
        {
            txtId.Text = "0";
            txtLoanType.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtLoanType.Text.Trim()))
            {
                MessageBox.Show("Loan type Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtLoanType.Focus();
                return;
            }

            if (!EditMode && SqliteDataAccess.CheckIfVoteExist(txtLoanType.Text.Trim(), Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("Loan type is already available", "@Chamaz", MessageBoxButtons.OK);
                txtLoanType.Focus();
                return;
            }

            //check if votename exists


            LoanType LoanType = new LoanType()
            {
                Id = Convert.ToInt32(txtId.Text),
                LoanTypeName = SqliteDataAccess.ToPropercase(txtLoanType.Text),
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateLoanType(LoanType) > 0)
                {
                    MessageBox.Show($"Loan type : {txtLoanType.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update Loantype : {txtLoanType.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertLoanType(LoanType) > 0)
                {
                    MessageBox.Show($"Loan type : {txtLoanType.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add Loan type : {txtLoanType.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }

            ShowAllPanel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgLoanType_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgLoanType.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString(); ;
            txtLoanType.Text = dtgrow.Cells[1].Value.ToString();
            ShowAddEditPanel(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to delete Loan type :  {txtLoanType.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SqliteDataAccess.CheckIfLoanTypeIsReferenced(Convert.ToInt32(txtId.Text.Trim())))
                {
                    MessageBox.Show($"Bank cannot be Deleted because it is used by account Details", "@Chamaz", MessageBoxButtons.OK);
                    return;
                }

                if (SqliteDataAccess.DeleteLoanType(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Loan type : {txtLoanType.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Loan type : {txtLoanType.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }
    }
}
