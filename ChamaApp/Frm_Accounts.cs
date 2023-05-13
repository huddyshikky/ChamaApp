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
    public partial class Frm_Accounts : Form
    {
        private bool EditMode = false;

        private List<MemberModel> Members = null;
        private void LoadMembers()
        {
            Members = SqliteDataAccess.GetALLNonMembers();
            dtgMembers.DataSource = Members;
            dtgMembers.Columns[0].Visible = false;
            dtgMembers.Columns[4].Visible = false;
        }
        public Frm_Accounts()
        {
            InitializeComponent();
        }

        private void Frm_NonMembers_Load(object sender, EventArgs e)
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
            LoadMembers();

            MemberAddEditPanel.Visible = false;
            MemberShowAllPanel.Visible = true;
            MemberShowAllPanel.Left = (MainGBox.Width - MemberShowAllPanel.Width) / 2;
            MemberShowAllPanel.Top = (MainGBox.Height - MemberShowAllPanel.Height) / 2;
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

            MemberShowAllPanel.Visible = false;
            MemberAddEditPanel.Visible = true;
            MemberAddEditPanel.Left = (MainGBox.Width - MemberAddEditPanel.Width) / 2;
            MemberAddEditPanel.Top = (MainGBox.Height - MemberAddEditPanel.Height) / 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
        }
        private void ClearFields()
        {
            txtId.Text = "0";
            txtIdNumber.Text = "";
            txtMemberName.Text = "";
            chkActive.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtIdNumber.Text.Trim()))
            {
                MessageBox.Show("Number Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtIdNumber.Focus();
                return;
            }
            if (!Int64.TryParse(txtIdNumber.Text.Trim(), out Int64 _))
            {
                MessageBox.Show("Number should be Numeric", "@Chamaz", MessageBoxButtons.OK);
                txtIdNumber.Focus();
                return;
            }
            if (SqliteDataAccess.CheckIfNonMemberIdNumberExist(Int64.Parse(txtIdNumber.Text.Trim()), int.Parse(txtId.Text.Trim())))
            {
                MessageBox.Show("Number is already in use", "@Chamaz", MessageBoxButtons.OK);
                txtIdNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMemberName.Text.Trim()))
            {
                MessageBox.Show("Non Member Name Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtMemberName.Focus();
                return;
            }

            if (SqliteDataAccess.CheckIfNonMemberExist(txtMemberName.Text.Trim(), int.Parse(txtId.Text.Trim())))
            {
                MessageBox.Show("Non Member Name is already in use", "@Chamaz", MessageBoxButtons.OK);
                txtMemberName.Focus();
                return;
            }



            MemberModel Member = new MemberModel()
            {
                Id = Convert.ToInt32(txtId.Text.Trim()),
                IdentityNo = Convert.ToInt64(txtIdNumber.Text.Trim()),
                MemberName = SqliteDataAccess.ToPropercase(txtMemberName.Text.Trim()),
                IsActive = ((int)chkActive.CheckState),
                IsMember = 0
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateNonMember(Member) > 0)
                {
                    MessageBox.Show($" {txtMemberName.Text.Trim()} Details Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update : {txtMemberName.Text.Trim()} Details", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {

                if (SqliteDataAccess.InsertNonMember(Member) > 0)
                {
                    MessageBox.Show($" {txtMemberName.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add : {txtMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }

            ShowAllPanel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgMembers_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgMembers.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString();
            txtIdNumber.Text = dtgrow.Cells[1].Value.ToString();
            txtMemberName.Text = dtgrow.Cells[2].Value.ToString();
            Int64 val = Int64.Parse(dtgrow.Cells[3].Value.ToString());
            ShowAddEditPanel(false);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to detete Non Member :  {txtMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteNonMember(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Non Member : {txtMemberName.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Non Member : {txtMemberName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }
    }
}
