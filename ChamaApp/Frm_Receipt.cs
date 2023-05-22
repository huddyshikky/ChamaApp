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
    public partial class Frm_Receipt : Form
    {
        private bool EditMode = false;
        private bool IsNonMember = false;
        decimal AmountAllocated = 0;
        decimal AmountRemaining = 0;
        decimal AmountTotal = 0;

        List<CashBookVoteVM> CashBookVotes = new List<CashBookVoteVM>();
        List<ReceiptVM> Receipts = new List<ReceiptVM>();
        ReceiptVM Receipt = null;
        private Member NonMember = null;


        private void ShowAllPanel()
        {
            LoadReceipts((int)cboPayerName.SelectedValue);

            MemberCreditAddEditPanel.Visible = false;
            MemberCreditShowAllPanel.Visible = true;
            MemberCreditShowAllPanel.Left = (MainGBox.Width - MemberCreditShowAllPanel.Width) / 2;
            MemberCreditShowAllPanel.Top =(MainGBox.Height - MemberCreditShowAllPanel.Height) / 2;
        }
        private void LoadMembers()
        {
            //check default Non Member
            NonMember = SqliteDataAccess.GetOnlyNonMemberById();

            if (NonMember==null)
            {
                //insert default non member
                
                if (SqliteDataAccess.InsertDefaultNonMember()>0)
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
                
                cboPayerName.DisplayMember = "MemberName";
                cboPayerName.ValueMember = "Id";
                cboPayerName.DataSource = Members;
                cboPayerName.SelectedIndex = 0;
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
        public Frm_Receipt()
        {
            InitializeComponent();
        }


        private void Frm_ReceiptMembers_Load(object sender, EventArgs e)
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

        private void cboMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPayerName.Items.Count>0)
            {
                lblMemberName.Text = cboPayerName.Text;
                txtPayerId.Text = cboPayerName.SelectedValue.ToString();
                //get receipts for the selected member
                LoadReceipts((int)cboPayerName.SelectedValue);
            }
            
        }
        private void LoadReceipts(int PayerId)
        {
            Receipts = SqliteDataAccess.GetReceiptByPayerId(PayerId);
            dtgMemberReceipts.DataSource = Receipts;
            dtgMemberReceipts.Columns[0].Visible = false;
            //dtgMemberReceipts.Columns[1].Visible = false;
            dtgMemberReceipts.Columns[2].Visible = false; //payerid
            //dtgMemberReceipts.Columns[3].Visible = false; //payer
            //dtgMemberReceipts.Columns[4].Visible = false;
            dtgMemberReceipts.Columns[5].Visible = false;
            //dtgMemberReceipts.Columns[6].Visible = false;
            dtgMemberReceipts.Columns[8].Visible = false; //accountId
            dtgMemberReceipts.Columns[10].Visible = false; //amountwords
            dtgMemberReceipts.Columns[12].Visible = false; //creditordebit
            dtgMemberReceipts.Columns[13].Visible = false; //creditordebit
            dtgMemberReceipts.Columns[14].Visible = false;      //category
            //dtgMemberReceipts.Columns[15].Visible = false;    // bankdate
            dtgMemberReceipts.Columns[16].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtRctNo.Text = SqliteDataAccess.GetNextReceiptNo().ToString();
            ShowAddEditPanel(true);
        }
        private void ShowAddEditPanel(bool Add)
        {
            if (IsNonMember)
            {
                lblMemberName.Visible= false;
                lblPayer.Visible= true;
                txtPayer.Visible = true;
            }
            else
            {
                lblMemberName.Visible = true;
                lblPayer.Visible = false;
                txtPayer.Visible = false;
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
            
            txtRctId.Text = "0";
            //txtRctNo.Text = "";
            txtCshbkId.Text = "0"; 
            if (cboPayerName.Items.Count > 0) { cboPayerName.SelectedIndex = 0; }
            txtPayer.Text = "";
            txtDetails.Text = "Deposit for "+ cboPayerName.Text.Trim();
            if (cboAccount.Items.Count>0){ cboAccount.SelectedIndex = 0;}
            dtpTransDate.Value = DateTime.Now;
            dtpBankDate.Value = DateTime.Now;
            TxtTotalAmount.Text = "0.00";
            if (cboPayMode.Items.Count > 0) { cboPayMode.SelectedIndex = 0; }
            txtModeNo.Text = "---";
            if (cboItemName.Items.Count > 0) { cboItemName.SelectedIndex = 0; } 
            TxtItemAmount.Text = "0.00";
            lbl_AmountAllocated.Text= "0.00";
            lbl_AmountRemaining.Text = "0.00";
            if (dtgItems.Rows.Count>0)
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
            if (decimal.Parse(TxtTotalAmount.Text.Trim())< AmountAllocated)
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
            if (dtgItems.Rows.Count>0)
            {
                foreach (DataGridViewRow item in dtgItems.Rows)
                {
                    AmountAllocated = AmountAllocated + decimal.Parse(item.Cells[2].Value.ToString());
                }
            }
            lbl_AmountAllocated.Text= AmountAllocated.ToString();    
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
            if (IsNonMember && NonMember==null)
            {
                MessageBox.Show("Non member id is missing", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayer.Focus();
                return;
            }
            if (IsNonMember && string.IsNullOrEmpty(txtPayer.Text.Trim()))
            {
                MessageBox.Show("Payer Name should not be empty", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayer.Focus();
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
            if (string.IsNullOrEmpty(txtRctNo.Text.Trim()))
            {
                MessageBox.Show("Cannot continue without a receipt No", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRctNo.Focus();
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
            if ((string.IsNullOrEmpty(txtModeNo.Text.Trim())) && (cboPayMode.Text.Trim().ToLower()!="cash"))
            {
                MessageBox.Show("Mode/Bank slip Number Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                cboPayMode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDetails.Text.Trim()))
            {
                MessageBox.Show("Please provide Receipt Details", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDetails.Focus();
                return;
            }
            if (dtgItems.Rows.Count < 1)
            {
                MessageBox.Show("Please add the items.You cannot receipt without items", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string RctDetails, PayerDetails = "";
            if (IsNonMember)
            {
                PayerDetails = SqliteDataAccess.ToPropercase(txtPayer.Text.Trim());
            }
            else
            {
                PayerDetails = SqliteDataAccess.ToPropercase(cboPayerName.Text.Trim());
            }

                Receipt = new ReceiptVM {
                Id = Convert.ToInt32(txtRctId.Text.Trim()),
                ReceiptNo = Convert.ToInt32(txtRctNo.Text.Trim()),
                PayerId = Convert.ToInt32(txtPayerId.Text.Trim()),
                Payer = PayerDetails,
                Details = SqliteDataAccess.ToPropercase(txtDetails.Text.Trim()),
                CashBookId = Convert.ToInt32(txtCshbkId.Text.Trim()),
                TransDate =dtpTransDate.Value,
                Month = UsableFunctions.GetMonthInWords(dtpTransDate.Value.Month),
                AccountId =(int)cboAccount.SelectedValue,
                Amount = decimal.Parse(TxtTotalAmount.Text.Trim()),
                AmountWords = UsableFunctions.ToWords(decimal.Parse(TxtTotalAmount.Text.Trim())),
                PayMode = SqliteDataAccess.ToPropercase(cboPayMode.Text.Trim()),
                PayModeNo = SqliteDataAccess.ToPropercase(txtModeNo.Text.Trim()),
                CreditOrDebit ="Credit",
                Category="Deposits", //MemberDeposits,NonMemberDeposits 
                BankDate = dtpBankDate.Value,
                //UserId 
                
            };

            //Cashbook votes
            FillAllocatedItems();

           
            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateReceipt(Receipt, CashBookVotes) > 0)
                {
                    MessageBox.Show($"Receipt Details for : {lblMemberName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Update Receipt Details for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {

                if (SqliteDataAccess.InsertReceipt(Receipt,CashBookVotes) > 0)
                {
                    MessageBox.Show($"Receipt Details for : {lblMemberName.Text.Trim()} Created", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to create receipt for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
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

        private void dtgMemberReceipts_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgMemberReceipts.CurrentRow;
            txtRctId.Text = dtgrow.Cells[0].Value.ToString(); 
            txtRctNo.Text = dtgrow.Cells[1].Value.ToString();
            cboPayerName.SelectedValue = (int)dtgrow.Cells[2].Value;
           txtPayer.Text = dtgrow.Cells[3].Value.ToString(); //for non members
           lblMemberName.Text = dtgrow.Cells[3].Value.ToString(); //for members
            txtDetails.Text= dtgrow.Cells[4].Value.ToString();
            txtCshbkId.Text= dtgrow.Cells[5].Value.ToString();  
            dtpTransDate.Value = Convert.ToDateTime(dtgrow.Cells[6].Value.ToString());
            cboAccount.SelectedValue = (int)dtgrow.Cells[8].Value;
            TxtTotalAmount.Text= dtgrow.Cells[9].Value.ToString();
            cboPayMode.Text = dtgrow.Cells[11].Value.ToString();
            txtModeNo.Text = dtgrow.Cells[12].Value.ToString();
            dtpBankDate.Value = Convert.ToDateTime(dtgrow.Cells[15].Value.ToString());

            GetVoteDetails(int.Parse(txtCshbkId.Text.Trim()));
            ShowAddEditPanel(false);

                //Id = Convert.ToInt32(txtRctId.Text.Trim()), //0
                //ReceiptNo = Convert.ToInt32(txtRctNo.Text.Trim()),//1
                //PayerId = Convert.ToInt32(txtPayerId.Text.Trim()),//2
                //Payer = SqliteDataAccess.ToPropercase(cboPayerName.Text.Trim()),//3
                //ReceiptDetails = SqliteDataAccess.ToPropercase(cboPayerName.Text.Trim()),//4
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
            if (MessageBox.Show($"Are you sure to delete the selected receipt for :  {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteReceipt(Convert.ToInt32(txtCshbkId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Receipt Details Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Receipt", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }


        private void ToggleRdoButtons()
        {
            if (rdoMember.Checked)
            {
                IsNonMember = false;
                cboPayerName.Enabled = true;   
            }
            else
            {
                IsNonMember = true;
                cboPayerName.Enabled = false;
            }
            LoadMembers();
        }

        private void rdoNonMember_Click(object sender, EventArgs e)
        {
            ToggleRdoButtons();
        }

        private void rdoMember_Click(object sender, EventArgs e)
        {
            ToggleRdoButtons();
        }
    }
}
