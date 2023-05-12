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
    public partial class Frm_NonLoanPaymentToMembers : Form
    {
        private bool EditMode = false;
        decimal AmountAllocated = 0;
        decimal AmountRemaining = 0;
        decimal AmountTotal = 0;

        List<CashBookDetailsModel> CashBookDetails = new List<CashBookDetailsModel>();
        List<CashBookDetailsViewModel> CashBookDetailsView = new List<CashBookDetailsViewModel>();

        CashBookModel CashBook = new CashBookModel();
        List<CashBookModel> CashBookList = new List<CashBookModel>();

        private void ShowAllPanel()
        {
            LoadMembers();

            MemberCreditAddEditPanel.Visible = false;
            MemberCreditShowAllPanel.Visible = true;
            MemberCreditShowAllPanel.Left = (MainGBox.Width - MemberCreditShowAllPanel.Width) / 2;
            MemberCreditShowAllPanel.Top = (MainGBox.Height - MemberCreditShowAllPanel.Height) / 2;
        }
        private void LoadMembers()
        {
            List<MemberModel> Members = new List<MemberModel>();
            Members = SqliteDataAccess.GetALLMembers();
            if (Members != null && Members.Count > 0)
            {
                cboMemberName.DisplayMember = "MemberName";
                cboMemberName.ValueMember = "Id";
                cboMemberName.DataSource = Members;
            }
        }
        private void LoadVotes()
        {
            List<VoteModel> Votes = new List<VoteModel>();
            Votes = SqliteDataAccess.GetALLVotes();
            if (Votes != null && Votes.Count > 0)
            {
                cboItemName.DisplayMember = "VoteName";
                cboItemName.ValueMember = "Id";
                cboItemName.DataSource = Votes;
            }
        }
        public Frm_NonLoanPaymentToMembers()
        {
            InitializeComponent();
        }

        private void Frm_NonLoanPaymentToMembers_Load(object sender, EventArgs e)
        {
            ShowAllPanel();
            LoadVotes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMemberName.Text = cboMemberName.Text;
            MemberModel selectedMember = (MemberModel)cboMemberName.SelectedItem;
            txtMemberId.Text = selectedMember.Id.ToString();
            //get Payments for the selected member
            LoadMemberPayments(Convert.ToInt32(cboMemberName.SelectedValue.ToString()));

        }
        private void LoadMemberPayments(int Member_Id)
        {
            CashBookList = SqliteDataAccess.GetCashBookByMemberId(Member_Id,"Debit");
            dtgMemberPayments.DataSource = CashBookList;
            dtgMemberPayments.Columns[0].Visible = false;
            dtgMemberPayments.Columns[1].Visible = false;
            dtgMemberPayments.Columns[2].Visible = false;
            dtgMemberPayments.Columns[5].Visible = false;
            dtgMemberPayments.Columns[6].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAddEditPanel(true);
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

            MemberCreditShowAllPanel.Visible = false;
            MemberCreditAddEditPanel.Visible = true;
            MemberCreditAddEditPanel.Left = (MainGBox.Width - MemberCreditAddEditPanel.Width) / 2;
            MemberCreditAddEditPanel.Top = (MainGBox.Height - MemberCreditAddEditPanel.Height) / 2;
        }
        private void ClearFields()
        {
            txtCsbkId.Text = "0";
            dtpTransDate.Value = DateTime.Now;
            TxtTotalAmount.Text = "0.00";
            cboPayMode.SelectedIndex = 0;
            cboItemName.SelectedIndex = 0;
            TxtItemAmount.Text = "0.00";
            lbl_AmountAllocated.Text = "0.00";
            lbl_AmountRemaining.Text = "0.00";
            if (dtgItems.Rows.Count > 0)
            {
                dtgItems.Rows.Clear();
            }
            AmountAllocated = 0;
            AmountRemaining = 0;
            AmountTotal = 0;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
        }

        private void btnAddIem_Click(object sender, EventArgs e)
        {


            if (!Itemexists(cboItemName.Text.Trim()))
            {
                if (!decimal.TryParse(TxtItemAmount.Text.Trim(), out _))
                {
                    MessageBox.Show("Item Amount should be numeric", "@Chamaz", MessageBoxButtons.OK);
                    TxtItemAmount.Focus();
                    return;
                }
                if (decimal.Parse(TxtItemAmount.Text.Trim()) < 1)
                {
                    MessageBox.Show("Item amount should not be negative or Zero", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtItemAmount.Focus();
                    return;
                }
                if (decimal.Parse(TxtItemAmount.Text.Trim()) > decimal.Parse(lbl_AmountRemaining.Text.Trim()))
                {
                    MessageBox.Show("Item Amount Allocated is more than the amount remaining", "@Chamaz", MessageBoxButtons.OK);
                    TxtItemAmount.Focus();
                    return;
                }

                //prepare
                string[] rodata = { cboItemName.SelectedValue.ToString(), cboItemName.Text.ToString(), TxtItemAmount.Text };
                dtgItems.Rows.Add(rodata);
                CalcAmountRemaining();
                TxtItemAmount.Text = "0.00";
            }
            else
            {
                MessageBox.Show("Vote already assigned.Please select another vote or clear the vote before re-assigning", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TxtTotalAmount_Leave(object sender, EventArgs e)
        {

            if (!decimal.TryParse(TxtTotalAmount.Text.Trim(), out _))
            {
                MessageBox.Show("Total Amount should be numeric", "@Chamaz", MessageBoxButtons.OK);
                TxtTotalAmount.Focus();
                return;
            }
            if (decimal.Parse(TxtTotalAmount.Text.Trim()) < AmountAllocated)
            {
                MessageBox.Show("Total Amount is less than what has been allocated.Increase the value or remove allocated items", "@Chamaz", MessageBoxButtons.OK);
                TxtTotalAmount.Focus();
                return;
            }
            AmountTotal = decimal.Parse(TxtTotalAmount.Text.Trim());

            CalcAmountRemaining();

        }

        private void CalcAmountRemaining()
        {
            CalcAmountAllocated();
            AmountRemaining = AmountTotal - AmountAllocated;
            lbl_AmountRemaining.Text = AmountRemaining.ToString();
        }

        private void CalcAmountAllocated()
        {
            AmountAllocated = 0;
            if (dtgItems.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dtgItems.Rows)
                {
                    AmountAllocated = AmountAllocated + decimal.Parse(item.Cells[2].Value.ToString());
                }
            }
            lbl_AmountAllocated.Text = AmountAllocated.ToString();
        }

        private void btnRemoveAllIIems_Click(object sender, EventArgs e)
        {
            dtgItems.Rows.Clear();
            CalcAmountRemaining();
        }

        private void btnRemoveSelectedIIems_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtgItems.SelectedRows)
            {
                dtgItems.Rows.RemoveAt(row.Index);
            }
            CalcAmountRemaining();
        }
        private bool Itemexists(string Vote)
        {
            foreach (DataGridViewRow row in dtgItems.Rows)
            {
                if (Vote == row.Cells[2].Value.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (decimal.Parse(TxtTotalAmount.Text.Trim()) < 1)
            {
                MessageBox.Show("Amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtTotalAmount.Focus();
                return;
            }

            if (dtgItems.Rows.Count < 1)
            {
                MessageBox.Show("Please add the items.You cannot make payment without items", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboItemName.Focus();
                return;
            }

            if (decimal.Parse(lbl_AmountRemaining.Text.Trim()) < 0)
            {
                MessageBox.Show("Total Amount is less than the amount already allocated on items.Please Delete the allocated items first or enter a higher value", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtTotalAmount.Focus();
                return;
            }
            if (decimal.Parse(lbl_AmountRemaining.Text.Trim()) > 0)
            {
                MessageBox.Show("Total Amount is more than the amount already allocated on Items.Please allocate all the amount on Items", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtTotalAmount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cboPayMode.Text.Trim()))
            {
                MessageBox.Show("Pay Mode Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                cboPayMode.Focus();
                return;
            }

            CashBook = new CashBookModel()
            {
                Id = Convert.ToInt32(txtCsbkId.Text.Trim()),
                Member_Id = Convert.ToInt32(txtMemberId.Text.Trim()),
                Name = SqliteDataAccess.ToPropercase(cboMemberName.Text.Trim()),
                Trans_Date = dtpTransDate.Value.ToString("yyyy-MM-dd"),
                Paymode = cboPayMode.Text.Trim(),
                TransType = "Debit",
                TransCategory = "MemberPayments",
                Amount = decimal.Parse(TxtTotalAmount.Text.Trim()),
            };

            //Cashbook Details
            FillAllocatedItems();


            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateCashBook(CashBook, CashBookDetails) > 0)
                {
                    MessageBox.Show($"Payment To Member : {lblMemberName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Update Payment Details for Member : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {

                if (SqliteDataAccess.InsertCashBook(CashBook, CashBookDetails) > 0)
                {
                    MessageBox.Show($"Payment to Member : {lblMemberName.Text.Trim()} Created", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to create Payment for Member : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            btnCancel_Click(sender, e);
        }

        private void FillAllocatedItems()
        {
            foreach (DataGridViewRow row in dtgItems.Rows)
            {
                CashBookDetails.Add(new CashBookDetailsModel
                {
                    //Id
                    Csbk_Id = Convert.ToInt32(txtCsbkId.Text.Trim()),
                    Vote_Id = Convert.ToInt32(row.Cells[0].Value.ToString().Trim()),
                    Amount = Convert.ToInt32(row.Cells[2].Value.ToString().Trim()),
                });
            }
        }

        private void dtgMemberPayments_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgMemberPayments.CurrentRow;
            txtCsbkId.Text = dtgrow.Cells[0].Value.ToString();
            dtpTransDate.Value = Convert.ToDateTime(dtgrow.Cells[3].Value.ToString());
            cboPayMode.Text = dtgrow.Cells[4].Value.ToString();

            GetVoteDetails(int.Parse(txtCsbkId.Text.Trim()));
            ShowAddEditPanel(false);
        }

        private void GetVoteDetails(int Csbk_Id)
        {

            AmountTotal = 0;
            CashBookDetailsView = SqliteDataAccess.CashBookDetailsByCsbkId(Csbk_Id);
            if (CashBookDetailsView != null && CashBookDetailsView.Count > 0)
            {
                foreach (var Drow in CashBookDetailsView)
                {
                    string[] rodata = { Drow.Id.ToString(), Drow.VoteName, Drow.Amount.ToString() };
                    dtgItems.Rows.Add(rodata);
                    AmountTotal += decimal.Parse(Drow.Amount.ToString());
                }
                AmountAllocated = AmountTotal;
                AmountRemaining = 0;
                TxtTotalAmount.Text = AmountTotal.ToString();
                lbl_AmountAllocated.Text = AmountAllocated.ToString();
                lbl_AmountRemaining.Text = "0.00";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to delete the selected Payment to Member :  {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteCashBook(Convert.ToInt32(txtCsbkId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Payment Details Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Payment", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }

        
    }
}
