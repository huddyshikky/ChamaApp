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

        //List<CashBookVoteVM> CashBookVotes = new List<CashBookVoteVM>();
        //List<ReceiptVM> Receipts = new List<ReceiptVM>(); 
        List<LoanVM> Loans = new List<LoanVM>();
        LoanVM Loan = null;

        private void ShowAllPanel()
        {
            LoadLoans((int)cboPayerName.SelectedValue);

            MemberCreditAddEditPanel.Visible = false;
            MemberCreditShowAllPanel.Visible = true;
            MemberCreditShowAllPanel.Left = (MainGBox.Width - MemberCreditShowAllPanel.Width) / 2;
            MemberCreditShowAllPanel.Top = (MainGBox.Height - MemberCreditShowAllPanel.Height) / 2;
        }
        private void LoadMembers()
        {
           
            List<Member> Members = new List<Member>();
            Members = SqliteDataAccess.GetALLMembers(1);
           
            if (Members != null && Members.Count > 0)
            {

                cboPayerName.DisplayMember = "MemberName";
                cboPayerName.ValueMember = "Id";
                cboPayerName.DataSource = Members;
                cboPayerName.SelectedIndex = 0;
            }
        }
        private void LoadLoanType()
        {
            List<LoanType> LoanTypes = new List<LoanType>();
            LoanTypes = SqliteDataAccess.GetALLLoanTypes();

            if (LoanTypes != null && LoanTypes.Count > 0)
            {
                cboLoanType.DisplayMember = "LoanTypeName";
                cboLoanType.ValueMember = "Id";
                cboLoanType.DataSource = LoanTypes;
                cboLoanType.SelectedIndex = 0;
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
        
        public Frm_LoanProcessing()
        {
            InitializeComponent();
        }

        private void Frm_MemberFine_Load(object sender, EventArgs e)
        {
            LoadLoanType();
            LoadMembers();
            LoadAccounts();
            ShowAllPanel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboPayerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPayerName.Items.Count > 0)
            {
                lblMemberName.Text = cboPayerName.Text;
                txtPayeeId.Text = cboPayerName.SelectedValue.ToString();
                //get loan for the selected member
                LoadLoans((int)cboPayerName.SelectedValue);
            }
        }
        private void LoadLoans(int PayerId)
        { 

            Loans = SqliteDataAccess.GetLoanByPayeeId(PayerId);
            dtgMemberFine.DataSource = Loans;
            dtgMemberFine.Columns[0].Visible = false;
            //dtgMemberFine.Columns[1].Visible = false; //Id
            //dtgMemberFine.Columns[2].Visible = false; //payerid
            //dtgMemberFine.Columns[3].Visible = false; //payer
            //dtgMemberFine.Columns[4].Visible = false; //Details
            //dtgMemberFine.Columns[5].Visible = false; //ReceiptCsbkId
            //dtgMemberFine.Columns[6].Visible = false; //PaymentCsbkId
            //dtgMemberFine.Columns[7].Visible = false; //TransDate
            dtgMemberFine.Columns[8].Visible = false; //accountId
            //dtgMemberFine.Columns[9].Visible = false; //Amount
            dtgMemberFine.Columns[10].Visible = false; //PayMode
            dtgMemberFine.Columns[11].Visible = false; //PayModeNo
            dtgMemberFine.Columns[12].Visible = false; //Category
            //dtgMemberFine.Columns[13].Visible = false; //BankDate
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAddEditPanel(true);
        }
        private void ShowAddEditPanel(bool Add)
        { 
            lblMemberName.Visible = true;
               
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
            txtLoanId.Text = "0";
            txtPayCshbkId.Text = "0";
            dtpTransDate.Value = DateTime.Now;
            if (cboPayerName.Items.Count > 0) { cboPayerName.SelectedIndex = 0; }
            if (cboAccount.Items.Count > 0) { cboAccount.SelectedIndex = 0; }
            if (cboLoanType.Items.Count > 0) { cboLoanType.SelectedIndex = 0; }
            if (cboPayMode.Items.Count > 0) { cboPayMode.SelectedIndex = 0; }
            txtModeNo.Text = "---";
            if (cboPeriodicType.Items.Count > 0) { cboPeriodicType.SelectedIndex = 0; }
            TxtInterest.Text = "1";
            TxtPeriod.Text = "1";
            TxtInterestAmount.Text = "0.00";
            TxtRepayments.Text = "0.00";
            TxtTotalAmount.Text = "0.00";
            txtPenalty.Text = "0.00";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox
            if (string.IsNullOrEmpty(lblMemberName.Text.Trim()))
                {
                    MessageBox.Show("Cannot continue without Name", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(cboLoanType.Text.Trim()))
            {
                MessageBox.Show("Cannot continue without an loan type", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboLoanType.Focus();
                return;
            }
            if (decimal.Parse(TxtPrincipleAmount.Text.Trim()) < 1)
            {
                MessageBox.Show("Loan Amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtPrincipleAmount.Focus();
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
            if (string.IsNullOrEmpty(cboPeriodicType.Text.Trim()))
            {
                MessageBox.Show("Periodic Type Cannot be empty", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPeriodicType.Focus();
                return;
            }
            if (!int.TryParse(TxtInterest.Text.Trim(),out _))
            {
                MessageBox.Show("Interest Rate should be numeric", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtPrincipleAmount.Focus();
                return;
            }
            if (!int.TryParse(TxtPeriod.Text.Trim(), out _))
            {
                MessageBox.Show("Loan Period should be numeric", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtPeriod.Focus();
                return;
            }
            if (decimal.Parse(TxtRepayments.Text.Trim()) < 1)
            {
                MessageBox.Show("Repayment should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtRepayments.Focus();
                return;
            }
            if (decimal.Parse(txtPenalty.Text.Trim()) < 1)
            {
                MessageBox.Show("Penalty amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPenalty.Focus();
                return;
            }
            if (decimal.Parse(TxtTotalAmount.Text.Trim()) < 1)
            {
                MessageBox.Show("Amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtTotalAmount.Focus();
                return;
            }

            //Id,PayeeId,Payee,AccountId,LoanTypeId,TransDate,VoucherNo,PaymentCsbkId,PaymentId,
            //PrincipleAmount,AmountWords,PayMode,PayModeNo,InterestRate,InterestAmount,RepayPeriod,RepayPeriodType,
            //PeriodicRepayAmount,PeriodicPenaltyAmount,TotalAmount

            Loan = new LoanVM
            {
                Id = Convert.ToInt32(txtLoanId.Text.Trim()),
                PayeeId = Convert.ToInt32(txtPayeeId.Text.Trim()),
                Payee = lblMemberName.Text.Trim(),
                AccountId = (int)cboAccount.SelectedValue,
                LoanTypeId = (int)cboLoanType.SelectedValue,
                TransDate = dtpTransDate.Value,
                VoucherNo = Convert.ToInt32(txtVrNo.Text.Trim()),
                PaymentCsbkId = Convert.ToInt32(txtPayCshbkId.Text.Trim()),
                PaymentId=0,
                PrincipleAmount= decimal.Parse(TxtPrincipleAmount.Text.Trim()),
                AmountWords= UsableFunctions.ToWords(decimal.Parse(TxtPrincipleAmount.Text.Trim())),
                PayMode = "Bank",
                PayModeNo = "---",
                InterestRate= decimal.Parse(TxtInterest.Text.Trim()),
                InterestAmount = decimal.Parse(TxtInterestAmount.Text.Trim()),
                RepayPeriod = int.Parse(TxtPeriod.Text.Trim()),
                RepayPeriodType=cboPeriodicType.Text.Trim(),
                PeriodicRepayAmount = decimal.Parse(TxtRepayments.Text.Trim()),
                PeriodicPenaltyAmount = decimal.Parse(txtPenalty.Text.Trim()),
                TotalAmount = decimal.Parse(TxtTotalAmount.Text.Trim()),
                
            };

           
            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateLoan(Loan) > 0)
                {
                    MessageBox.Show($"Loan Details for : {lblMemberName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Update Loan Details for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {

                if (SqliteDataAccess.InsertLoan(Loan) > 0)
                {
                    MessageBox.Show($"Loan Details for : {lblMemberName.Text.Trim()} Created", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to create Loan for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            btnCancel_Click(sender, e);
        }

        private void dtgMemberFine_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        { //Id,PayeeId,Payee,AccountId,LoanTypeId,TransDate,VoucherNo,PaymentCsbkId,PaymentId,
          //PrincipleAmount,AmountWords,PayMode,PayModeNo,InterestRate,InterestAmount,RepayPeriod,RepayPeriodType,
          //PeriodicRepayAmount,PeriodicPenaltyAmount,TotalAmount
            DataGridViewRow dtgrow = dtgMemberFine.CurrentRow;
            txtLoanId.Text = dtgrow.Cells[0].Value.ToString();//loan id
            txtPayeeId.Text = dtgrow.Cells[1].Value.ToString();//payee id
            cboPayerName.SelectedValue = (int)dtgrow.Cells[1].Value; //payee
            cboAccount.SelectedValue = (int)dtgrow.Cells[3].Value;//AccountId
            cboLoanType.SelectedValue = (int)dtgrow.Cells[4].Value;//LoanTypeId
            dtpTransDate.Value = Convert.ToDateTime(dtgrow.Cells[5].Value.ToString()); 
            txtVrNo.Text = dtgrow.Cells[6].Value.ToString();//VoucherNo
            txtPayCshbkId.Text = dtgrow.Cells[7].Value.ToString();
            //PaymentId
            TxtPrincipleAmount.Text = dtgrow.Cells[9].Value.ToString(); 
            //AmountWords
            cboPayMode.Text = dtgrow.Cells[11].Value.ToString(); 
            txtModeNo.Text = dtgrow.Cells[12].Value.ToString();
            TxtInterest.Text = dtgrow.Cells[13].Value.ToString(); 
            TxtInterestAmount.Text = dtgrow.Cells[14].Value.ToString();
            TxtPeriod.Text = dtgrow.Cells[15].Value.ToString();
            cboPeriodicType.Text = dtgrow.Cells[16].Value.ToString();
            TxtRepayments.Text = dtgrow.Cells[17].Value.ToString();
            txtPenalty.Text = dtgrow.Cells[18].Value.ToString();
            TxtTotalAmount.Text = dtgrow.Cells[19].Value.ToString();
            
            ShowAddEditPanel(false);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to delete the selected Loan for :  {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteLoan(Convert.ToInt32(txtPayCshbkId.Text.Trim()), Convert.ToInt32(txtLoanId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Loan Details Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Loan details", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }
    }
}
