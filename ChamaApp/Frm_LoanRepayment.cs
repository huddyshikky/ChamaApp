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
    public partial class Frm_LoanRepayment : Form
    {
        private bool EditMode = false;

        //List<CashBookVoteVM> CashBookVotes = new List<CashBookVoteVM>();
        //List<ReceiptVM> Receipts = new List<ReceiptVM>(); 
        List<LoanRepaymentVM> LoanRepayment = new List<LoanRepaymentVM>();
        List<LoanVM> Loans = new List<LoanVM>();
        LoanVM Loan = null;

        private void ShowAllPanel()
        {
            cboPayerName_SelectedIndexChanged(this,EventArgs.Empty);

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
        private void LoadMemberLoans(int PayerId)
        {
            Loans = SqliteDataAccess.GetLoanByPayeeId(PayerId);

            if (Loans != null && Loans.Count > 0)
            {
                cboLoan.DisplayMember = "ShortDescription";
                cboLoan.ValueMember = "Id";
                cboLoan.DataSource = Loans;
                cboLoan.SelectedIndex = 0;
            }
        }
       
        public Frm_LoanRepayment()
        {
            InitializeComponent();
        }

        private void Frm_LoanRepayment_Load(object sender, EventArgs e)
        {
            //LoadLoanRepayments();
            LoadMembers();
            //LoadMemberLoans();
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

                LoadMemberLoans((int)cboPayerName.SelectedValue);
            }
        }
        private void LoadLoanRepayments(int LoanId)
        {
            LoanRepayment = SqliteDataAccess.GetLoanRepaymentsById(LoanId);

            if (Loans != null && Loans.Count > 0)
            {
                cboLoan.DisplayMember = "ShortDescription";
                cboLoan.ValueMember = "Id";
                cboLoan.DataSource = Loans;
                cboLoan.SelectedIndex = 0;
            }
        }

        private void cboLoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLoanRepayments((int)cboLoan.SelectedValue);
        }
    }
}
