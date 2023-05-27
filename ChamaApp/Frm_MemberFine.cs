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
    public partial class Frm_MemberFine : Form
    {
        private bool EditMode = false;

        List<CashBookVoteVM> CashBookVotes = new List<CashBookVoteVM>();
        List<ReceiptVM> Receipts = new List<ReceiptVM>(); 
        List<MemberFineVM> MemberFines = new List<MemberFineVM>();
        MemberFineVM MemberFine = null;
        private Vote FineVote = null;
        private Vote FineWithdrawal = null;

        private void ShowAllPanel()
        {
            LoadFines((int)cboPayerName.SelectedValue);

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
        
        public Frm_MemberFine()
        {
            InitializeComponent();
        }

        private void Frm_MemberFine_Load(object sender, EventArgs e)
        {
            //LoadFineAndFineWithdrawalVote();
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
                txtPayerId.Text = cboPayerName.SelectedValue.ToString();
                //get receipts for the selected member
                LoadFines((int)cboPayerName.SelectedValue);
            }
        }
        private void LoadFines(int PayerId)
        { //Id,PayerId,Payer,Details,ReceiptCsbkId,PaymentCsbkId,TransDate,AccountId,Amount,
          //PayMode,PayModeNo,Category,BankDate
            MemberFines = SqliteDataAccess.GetFineByPayerId(PayerId);
            dtgMemberFine.DataSource = MemberFines;
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
            txtFineId.Text = "0";
            txtPayCshbkId.Text = "0";
            txtRctCshbkId.Text = "0";

            if (cboPayerName.Items.Count > 0) { cboPayerName.SelectedIndex = 0; }
            txtDetails.Text = "Fine for " + cboPayerName.Text.Trim();
            if (cboAccount.Items.Count > 0) { cboAccount.SelectedIndex = 0; }
            dtpTransDate.Value = DateTime.Now;
            TxtTotalAmount.Text = "0.00";

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

            if (decimal.Parse(TxtTotalAmount.Text.Trim()) < 1)
            {
                MessageBox.Show("Amount should not be negative or Zero", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtTotalAmount.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDetails.Text.Trim()))
            {
                MessageBox.Show("Please provide Receipt Details", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDetails.Focus();
                return;
            }
            

            MemberFine= new MemberFineVM
            {
                Id = Convert.ToInt32(txtFineId.Text.Trim()),
                PayerId = Convert.ToInt32(txtPayerId.Text.Trim()),
                Payer = lblMemberName.Text.Trim(),
                Details = SqliteDataAccess.ToPropercase(txtDetails.Text.Trim()),
                ReceiptCsbkId = Convert.ToInt32(txtRctCshbkId.Text.Trim()),
                PaymentCsbkId = Convert.ToInt32(txtPayCshbkId.Text.Trim()),
                TransDate = dtpTransDate.Value,
                AccountId = (int)cboAccount.SelectedValue,
                Amount = decimal.Parse(TxtTotalAmount.Text.Trim()),
                PayMode = "Bank",
                PayModeNo = "---",
                Category = "Fines", //MemberDeposits,NonMemberDeposits,Fines 
                BankDate = dtpTransDate.Value,

            };

           
            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateFine(MemberFine) > 0)
                {
                    MessageBox.Show($"Fine Details for : {lblMemberName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Update Fine Details for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {

                if (SqliteDataAccess.InsertFine(MemberFine) > 0)
                {
                    MessageBox.Show($"Fine Details for : {lblMemberName.Text.Trim()} Created", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to create  Fine for : {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            btnCancel_Click(sender, e);
        }

        private void dtgMemberFine_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgMemberFine.CurrentRow;
            txtFineId.Text = dtgrow.Cells[0].Value.ToString();//fine id
            txtPayerId.Text = dtgrow.Cells[1].Value.ToString();//payer id
            cboPayerName.SelectedValue = (int)dtgrow.Cells[1].Value; //payer
            txtDetails.Text = dtgrow.Cells[3].Value.ToString();
            txtRctCshbkId.Text = dtgrow.Cells[4].Value.ToString();
            txtPayCshbkId.Text = dtgrow.Cells[5].Value.ToString();
            dtpTransDate.Value = Convert.ToDateTime(dtgrow.Cells[6].Value.ToString());
            cboAccount.SelectedValue = (int)dtgrow.Cells[7].Value;
            TxtTotalAmount.Text = dtgrow.Cells[8].Value.ToString();
            
            ShowAddEditPanel(false);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to delete the selected fine for :  {lblMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteFine(Convert.ToInt32(txtRctCshbkId.Text.Trim()), Convert.ToInt32(txtPayCshbkId.Text.Trim()), Convert.ToInt32(txtFineId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Fine Details Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete fine details", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }
    }
}
