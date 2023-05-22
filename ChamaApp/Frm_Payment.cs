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
    public partial class Frm_Payment : Form
    {
        private bool EditMode = false;
        private bool IsNonMember = false;
        decimal AmountAllocated = 0;
        decimal AmountRemaining = 0;
        decimal AmountTotal = 0;

        List<CashBookVoteVM> CashBookVotes = new List<CashBookVoteVM>();
        List<PaymentVM> Payments = new List<PaymentVM>();
        PaymentVM Payment = null;
        private Member NonMember = null;


        private void ShowAllPanel()
        {
            LoadPayments((int)cboPayeeName.SelectedValue);

            MemberCreditAddEditPanel.Visible = false;
            MemberCreditShowAllPanel.Visible = true;
            MemberCreditShowAllPanel.Left = (MainGBox.Width - MemberCreditShowAllPanel.Width) / 2;
            MemberCreditShowAllPanel.Top = (MainGBox.Height - MemberCreditShowAllPanel.Height) / 2;
        }
        private void LoadMembers()
        {
            //check default Non Member
            NonMember = SqliteDataAccess.GetOnlyNonMemberById();

            if (NonMember == null)
            {
                //insert default non member

                if (SqliteDataAccess.InsertDefaultNonMember() > 0)
                {
                    //check default Non Member
                    NonMember = SqliteDataAccess.GetOnlyNonMemberById();
                }
                else
                {
                    MessageBox.Show("Failed to create default non member", "@Chamaz", MessageBoxButtons.OK);
                }
            }

            List<Member> Members = new List<Member>();
            if (IsNonMember)
            {
                Members = SqliteDataAccess.GetALLMembers(0);
            }
            else
            {
                Members = SqliteDataAccess.GetALLMembers(1);
            }

            if (Members != null && Members.Count > 0)
            {

                cboPayeeName.DisplayMember = "MemberName";
                cboPayeeName.ValueMember = "Id";
                cboPayeeName.DataSource = Members;
                cboPayeeName.SelectedIndex = 0;
            }
        }
        private void LoadAccounts()
        {
            List<AccountVM> Accounts = new List<AccountVM>();
            Accounts = SqliteDataAccess.GetALLAccounts();
            if (Accounts.Count > 0)
            {
                cboAccount.DisplayMember = "AccountName";
                cboAccount.ValueMember = "Id";
                cboAccount.DataSource = Accounts;
            }
        }
        private void LoadVotes()
        {
            List<Vote> Votes = new List<Vote>();
            Votes = SqliteDataAccess.GetALLVotes();
            if (Votes != null && Votes.Count > 0)
            {
                cboItemName.DisplayMember = "VoteName";
                cboItemName.ValueMember = "Id";
                cboItemName.DataSource = Votes;
            }
        }
        public Frm_Payment()
        {
            InitializeComponent();
        }

        private void Frm_Payment_Load(object sender, EventArgs e)
        {
            LoadVotes();
            LoadMembers();
            LoadAccounts();
            ShowAllPanel();
            rdoMember.Checked = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboPayerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPayeeName.Items.Count > 0)
            {
                lblMemberName.Text = cboPayeeName.Text;
                txtPayeeId.Text = cboPayeeName.SelectedValue.ToString();
                //get payments for the selected member
                LoadPayments((int)cboPayeeName.SelectedValue);
            }

        }
        private void LoadPayments(int PayeeId)
        {
            Payments = SqliteDataAccess.GetPaymentByPayeeId(PayeeId);
            dtgPayments.DataSource = Payments;
            dtgPayments.Columns[0].Visible = false;
            //dtgPayments.Columns[1].Visible = false;
            dtgPayments.Columns[2].Visible = false; //payeeid
            //dtgPayments.Columns[3].Visible = false; //payee
            //dtgPayments.Columns[4].Visible = false;
            dtgPayments.Columns[5].Visible = false;
            //dtgPayments.Columns[6].Visible = false;
            dtgPayments.Columns[8].Visible = false; //accountId
            dtgPayments.Columns[10].Visible = false; //amountwords
            dtgPayments.Columns[12].Visible = false; //creditordebit
            dtgPayments.Columns[13].Visible = false; //creditordebit
            dtgPayments.Columns[14].Visible = false;      //category
            //dtgPayments.Columns[15].Visible = false;    // bankdate
            dtgPayments.Columns[16].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtVrNo.Text = SqliteDataAccess.GetNextVoucherNo().ToString();
            ShowAddEditPanel(true);
        }
        private void ShowAddEditPanel(bool Add)
        {
            if (IsNonMember)
            {
                lblMemberName.Visible = false;
                lblPayee.Visible = true;
                txtPayee.Visible = true;
            }
            else
            {
                lblMemberName.Visible = true;
                lblPayee.Visible = false;
                txtPayee.Visible = false;
            }


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

            txtVrId.Text = "0";
            //txtRctNo.Text = "";
            txtCshbkId.Text = "0";
            if (cboPayeeName.Items.Count > 0) { cboPayeeName.SelectedIndex = 0; }
            txtPayee.Text = "";
            txtDetails.Text = "Deposit for " + cboPayeeName.Text.Trim();
            if (cboAccount.Items.Count > 0) { cboAccount.SelectedIndex = 0; }
            dtpTransDate.Value = DateTime.Now;
            dtpBankDate.Value = DateTime.Now;
            TxtTotalAmount.Text = "0.00";
            if (cboPayMode.Items.Count > 0) { cboPayMode.SelectedIndex = 0; }
            txtModeNo.Text = "---";
            if (cboItemName.Items.Count > 0) { cboItemName.SelectedIndex = 0; }
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
                    MessageBox.Show("Item amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Item already assigned.Please select another vote or clear the vote before re-assigning", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (IsNonMember && NonMember == null)
            {
                MessageBox.Show("Non member id is missing", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayee.Focus();
                return;
            }
            if (IsNonMember && string.IsNullOrEmpty(txtPayee.Text.Trim()))
            {
                MessageBox.Show("Payee Name should not be empty", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayee.Focus();
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(lblMemberName.Text.Trim()))
                {
                    MessageBox.Show("Cannot continue without Name", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtVrNo.Text.Trim()))
            {
                MessageBox.Show("Cannot continue without a payment No", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVrNo.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cboAccount.Text.Trim()))
            {
                MessageBox.Show("Cannot continue without an account", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboAccount.Focus();
                return;
            }
            //if (!SqliteDataAccess.IsDateWithinFinancialYear(dtpTransDate.Value))
            //{
            //    MessageBox.Show("Trans Date is not within the current financial year", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    dtpTransDate.Focus();
            //    return;
            //}
            //if (!SqliteDataAccess.IsDateWithinFinancialYear(dtpBankDate.Value))
            //{
            //    MessageBox.Show("Bank Date is not within the current financial year", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    dtpBankDate.Focus();
            //    return;
            //}

            if (decimal.Parse(TxtTotalAmount.Text.Trim()) < 1)
            {
                MessageBox.Show("Amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtTotalAmount.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cboPayMode.Text.Trim()))
            {
                MessageBox.Show("Pay Mode Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                cboPayMode.Focus();
                return;
            }
            if ((string.IsNullOrEmpty(txtModeNo.Text.Trim())) && (cboPayMode.Text.Trim().ToLower() != "cash"))
            {
                MessageBox.Show("Mode/Bank slip Number Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                cboPayMode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDetails.Text.Trim()))
            {
                MessageBox.Show("Please provide payment Details", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDetails.Focus();
                return;
            }
            if (dtgItems.Rows.Count < 1)
            {
                MessageBox.Show("Please add the items.You cannot do payments without items", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string PayeeDetails = "";
            if (IsNonMember)
            {
                PayeeDetails = SqliteDataAccess.ToPropercase(txtPayee.Text.Trim());
            }
            else
            {
                PayeeDetails = SqliteDataAccess.ToPropercase(cboPayeeName.Text.Trim());
            }

            Payment = new PaymentVM
            {
                Id = Convert.ToInt32(txtVrId.Text.Trim()),
                VoucherNo = Convert.ToInt32(txtVrNo.Text.Trim()),
                PayeeId = Convert.ToInt32(txtPayeeId.Text.Trim()),
                Payee = PayeeDetails,
                PaymentDetails = SqliteDataAccess.ToPropercase(txtDetails.Text.Trim()),
                CashBookId = Convert.ToInt32(txtCshbkId.Text.Trim()),
                TransDate = dtpTransDate.Value,
                Month = UsableFunctions.GetMonthInWords(dtpTransDate.Value.Month),
                AccountId = (int)cboAccount.SelectedValue,
                Amount = decimal.Parse(TxtTotalAmount.Text.Trim()),
                AmountWords = UsableFunctions.ToWords(decimal.Parse(TxtTotalAmount.Text.Trim())),
                PayMode = SqliteDataAccess.ToPropercase(cboPayMode.Text.Trim()),
                PayModeNo = SqliteDataAccess.ToPropercase(txtModeNo.Text.Trim()),
                CreditOrDebit = "Debit",
                Category = "NonDeductiblePayments", //MemberDeposits,NonMemberDeposits 
                BankDate = dtpBankDate.Value,
                //UserId 

            };

            //Cashbook votes
            FillAllocatedItems();


            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdatePayment(Payment, CashBookVotes) > 0)
                {
                    MessageBox.Show($"Payment Details for : {lblMemberName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Update Payment Details for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {

                if (SqliteDataAccess.InsertPayment(Payment, CashBookVotes) > 0)
                {
                    MessageBox.Show($"Payment Details for : {lblMemberName.Text.Trim()} Created", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to create payment for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            btnCancel_Click(sender, e);
        }
        private void FillAllocatedItems()
        {
            CashBookVotes.Clear();

            foreach (DataGridViewRow row in dtgItems.Rows)
            {
                CashBookVotes.Add(new CashBookVoteVM
                {
                    //Id
                    VoteId = Convert.ToInt32(row.Cells[0].Value.ToString().Trim()),
                    VoteAmount = Convert.ToInt32(row.Cells[2].Value.ToString().Trim()),
                    CashBookId = Convert.ToInt32(txtCshbkId.Text.Trim()),
                });
            }
        }

        private void dtgPayments_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgPayments.CurrentRow;
            txtVrId.Text = dtgrow.Cells[0].Value.ToString();
            txtVrNo.Text = dtgrow.Cells[1].Value.ToString();
            cboPayeeName.SelectedValue = (int)dtgrow.Cells[2].Value;
            txtPayee.Text = dtgrow.Cells[3].Value.ToString(); //for non members
            lblMemberName.Text = dtgrow.Cells[3].Value.ToString(); //for members
            txtDetails.Text = dtgrow.Cells[4].Value.ToString();
            txtCshbkId.Text = dtgrow.Cells[5].Value.ToString();
            dtpTransDate.Value = Convert.ToDateTime(dtgrow.Cells[6].Value.ToString());
            cboAccount.SelectedValue = (int)dtgrow.Cells[8].Value;
            TxtTotalAmount.Text = dtgrow.Cells[9].Value.ToString();
            cboPayMode.Text = dtgrow.Cells[11].Value.ToString();
            txtModeNo.Text = dtgrow.Cells[12].Value.ToString();
            dtpBankDate.Value = Convert.ToDateTime(dtgrow.Cells[15].Value.ToString());

            GetVoteDetails(int.Parse(txtCshbkId.Text.Trim()));
            ShowAddEditPanel(false);

            //Id = Convert.ToInt32(txtRctId.Text.Trim()), //0
            //PaymentNo = Convert.ToInt32(txtRctNo.Text.Trim()),//1
            //PayerId = Convert.ToInt32(txtPayerId.Text.Trim()),//2
            //Payer = SqliteDataAccess.ToPropercase(cboPayerName.Text.Trim()),//3
            //PaymentDetails = SqliteDataAccess.ToPropercase(cboPayerName.Text.Trim()),//4
            //CashBookId = Convert.ToInt32(txtCshbkId.Text.Trim()),//5
            //TransDate = dtpTransDate.Value,//6
            //Month = UsableFunctions.GetMonthInWords(dtpTransDate.Value.Month),//8
            //AccountId = (int)cboAccount.SelectedValue,//9
            //Amount = decimal.Parse(TxtTotalAmount.Text.Trim()),//10
            //AmountWords = UsableFunctions.ToWords(decimal.Parse(TxtTotalAmount.Text.Trim())),//11
            //PayMode = SqliteDataAccess.ToPropercase(cboPayMode.Text.Trim()),//12
            //PayModeNo = SqliteDataAccess.ToPropercase(txtModeNo.Text.Trim()),//13
            //CreditOrDebit = "Credit",//14
            //Category = "Deposits", //MemberDeposits,NonMemberDeposits //15
            //BankDate = dtpBankDate.Value,//16
        }

        private void GetVoteDetails(int CashBookId)
        {

            AmountTotal = 0;
            CashBookVotes = SqliteDataAccess.CashBookVotesByCsbkId(CashBookId);
            if (CashBookVotes != null && CashBookVotes.Count > 0)
            {
                dtgItems.Rows.Clear();
                foreach (var Drow in CashBookVotes)
                {
                    string[] rodata = { Drow.VoteId.ToString(), Drow.VoteName, Drow.VoteAmount.ToString() };
                    dtgItems.Rows.Add(rodata);
                    AmountTotal += decimal.Parse(Drow.VoteAmount.ToString());
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
            if (MessageBox.Show($"Are you sure to delete the selected payment for :  {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeletePayment(Convert.ToInt32(txtCshbkId.Text.Trim())) > 0)
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


        private void ToggleRdoButtons()
        {
            if (rdoMember.Checked)
            {
                IsNonMember = false;
                cboPayeeName.Enabled = true;
            }
            else
            {
                IsNonMember = true;
                cboPayeeName.Enabled = false;
            }
            LoadMembers();
        }

        private void rdoMember_Click(object sender, EventArgs e)
        {
            ToggleRdoButtons();
        }

        private void rdoNonMember_Click(object sender, EventArgs e)
        {
            ToggleRdoButtons();
        }
    }

}
