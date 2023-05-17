using ChamaLibrary.DataAccess;
using ChamaLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamaApp
{
    public partial class Frm_FinYear : Form
    {
        private bool EditMode = false;

        private List<FinYear> FinYears = null;
        private void LoadFinYears()
        {
            FinYears = SqliteDataAccess.GetALLFinYears();
            dtgFinYears.DataSource = FinYears;
            dtgFinYears.Columns[0].Visible = false;
        }
        public Frm_FinYear()
        {
            InitializeComponent();
        }

        private void Frm_FinYear_Load(object sender, EventArgs e)
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
            LoadFinYears();

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
            txtYearName.Text = "";
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtYearName.Text.Trim()))
            {
                MessageBox.Show("FinYear Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtYearName.Focus();
                return;
            }

            if (SqliteDataAccess.CheckIfFinYearExist(txtYearName.Text.Trim(), Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("FinYear Name has already been registered", "@Chamaz", MessageBoxButtons.OK);
                txtYearName.Focus();
                return;
            }

            //check if votename exists


            FinYear FinYear = new FinYear()
            {
                Id = Convert.ToInt32(txtId.Text),
                YearName = SqliteDataAccess.ToPropercase(txtYearName.Text),
                StartDate =dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateFinYear(FinYear) > 0)
                {
                    MessageBox.Show($"FinYear : {txtYearName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update FinYear : {txtYearName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertFinYear(FinYear) > 0)
                {
                    MessageBox.Show($"FinYear : {txtYearName.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add FinYear : {txtYearName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
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
            if (MessageBox.Show($"Are you sure to detete FinYear :  {txtYearName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SqliteDataAccess.CheckIfBankIsReferenced(Convert.ToInt32(txtId.Text.Trim())))
                {
                    MessageBox.Show($"FinYear cannot be Deleted because it is used by other entities", "@Chamaz", MessageBoxButtons.OK);

                    return;
                }
                if (SqliteDataAccess.DeleteFinYear(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"FinYear : {txtYearName.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete FinYear : {txtYearName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }

        private void dtgBanks_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgFinYears.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString(); ;
            txtYearName.Text = dtgrow.Cells[1].Value.ToString();
            ShowAddEditPanel(false);
        }
    }
}
