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
    public partial class Frm_LoanProcessing : Form
    {
        private bool EditMode = false;
        //decimal amountallocated = 0;
        //decimal amountremaining = 0;
        //decimal amounttotal = 0;

        List<CashBookVote> CashBookDetails = new List<CashBookVote>();
        List<CashBookVoteVM> CashBookDetailsView = new List<CashBookVoteVM>();

        CashBook CashBook = new CashBook();
        List<CashBook> CashBookList = new List<CashBook>();

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
            List<Member> Members = new List<Member>();
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
            List<Vote> Votes = new List<Vote>();
            Votes = SqliteDataAccess.GetALLVotes();
            if (Votes != null && Votes.Count > 0)
            {
                //cboItemName.DisplayMember = "VoteName";
                //cboItemName.ValueMember = "Id";
                //cboItemName.DataSource = Votes;
            }
        }
       


        public Frm_LoanProcessing()
        {
            InitializeComponent();
        }

        private void Frm_LoanProcessing_Load(object sender, EventArgs e)
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
            Member selectedMember = (Member)cboMemberName.SelectedItem;
            txtMemberId.Text = selectedMember.Id.ToString();
            //get receipts for the selected member
            LoadMemberReceipts(Convert.ToInt32(cboMemberName.SelectedValue.ToString()));
        }

        private void LoadMemberReceipts(int Member_Id)
        {
            //CashBookList = SqliteDataAccess.GetCashBookByMemberId(Member_Id, "Credit");
            //dtgMemberReceipts.DataSource = CashBookList;
            dtgMemberReceipts.Columns[0].Visible = false;
            dtgMemberReceipts.Columns[1].Visible = false;
            dtgMemberReceipts.Columns[2].Visible = false;
            dtgMemberReceipts.Columns[5].Visible = false;
            dtgMemberReceipts.Columns[6].Visible = false;
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
            //cboItemName.SelectedIndex = 0;
            //TxtItemAmount.Text = "0.00";
            //lbl_AmountAllocated.Text = "0.00";
            //lbl_AmountRemaining.Text = "0.00";
            //if (dtgItems.Rows.Count > 0)
            //{
            //    dtgItems.Rows.Clear();
            //}
            //AmountAllocated = 0;
            //AmountRemaining = 0;
            //AmountTotal = 0;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
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

            //if (dtgItems.Rows.Count < 1)
            //{
            //    MessageBox.Show("Please add the items.You cannot receipt without items", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cboItemName.Focus();
            //    return;
            //}

            //if (decimal.Parse(lbl_AmountRemaining.Text.Trim()) < 0)
            //{
            //    MessageBox.Show("Total Amount is less than the amount already allocated on items.Please Delete the allocated items first or enter a higher value", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    TxtTotalAmount.Focus();
            //    return;
            //}
            //if (decimal.Parse(lbl_AmountRemaining.Text.Trim()) > 0)
            //{
            //    MessageBox.Show("Total Amount is more than the amount already allocated on Items.Please allocate all the amount on Items", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    TxtTotalAmount.Focus();
            //    return;
            //}

            if (string.IsNullOrEmpty(cboPayMode.Text.Trim()))
            {
                MessageBox.Show("Pay Mode Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                cboPayMode.Focus();
                return;
            }

            CashBook = new CashBook()
            {
                Id = Convert.ToInt32(txtCsbkId.Text.Trim()),
                //Member_Id = Convert.ToInt32(txtMemberId.Text.Trim()),
                //Name = SqliteDataAccess.ToPropercase(cboMemberName.Text.Trim()),
                //Trans_Date = dtpTransDate.Value.ToString("yyyy-MM-dd"),
                //PayMode = cboPayMode.Text.Trim(),
                //TransType = "Credit",
                //TransCategory = "MemberDeposits",
                Amount = decimal.Parse(TxtTotalAmount.Text.Trim()),
            };

            //Cashbook Details
            //FillAllocatedItems();


            if (EditMode) //update data
            {
                //if (SqliteDataAccess.UpdateCashBook(CashBook, CashBookDetails) > 0)
                //{
                //    MessageBox.Show($"Receipt Details for Member : {lblMemberName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);
                //}
                //else
                //{
                //    MessageBox.Show($"Failed to Update Receipt Details for Member : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                //}
            }
            else //save new record
            {

                //if (SqliteDataAccess.InsertCashBook(CashBook, CashBookDetails) > 0)
                //{
                //    MessageBox.Show($"Receipt Details for Member : {lblMemberName.Text.Trim()} Created", "@Chamaz", MessageBoxButtons.OK);

                //}
                //else
                //{
                //    MessageBox.Show($"Failed to create receipt for Member : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                //}
            }
            btnCancel_Click(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to detete the selected receipt for Member :  {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                //if (SqliteDataAccess.DeleteCashBook(Convert.ToInt32(txtCsbkId.Text.Trim())) > 0)
                //{
                //    MessageBox.Show($"Receipt Details Deleted", "@Chamaz", MessageBoxButtons.OK);
                //}
                //else
                //{
                //    MessageBox.Show($"Failed to Delete Receipt", "@Chamaz", MessageBoxButtons.OK);
                //}
            }
            ShowAllPanel();
        }
    }
}
