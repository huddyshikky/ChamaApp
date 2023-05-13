using ChamaLibrary.DataAccess;
using ChamaLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChamaApp
{
    public partial class Frm_Votes : Form
    {
        private bool EditMode = false;

        private List<VoteModel> votes=null;
        private void LoadVotes()
        {
            votes= SqliteDataAccess.GetALLVotes();
            dtgVotes.DataSource = votes;
            dtgVotes.Columns[0].Visible= false;
        }
        public Frm_Votes()
        {
            InitializeComponent();
        }

        private void FrmVotes_Load(object sender, EventArgs e)
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
            LoadVotes();
            
            VoteAddEditPanel.Visible = false;
            VoteShowAllPanel.Visible = true;
            VoteShowAllPanel.Left = (MainGBox.Width- VoteShowAllPanel.Width)/2;
            VoteShowAllPanel.Top = (MainGBox.Height - VoteShowAllPanel.Height) / 2;
        }
        private void ShowAddEditPanel(bool Add)
        {
            if (Add)
            {
                btnSave.Text = "Save"; 
                btnDelete.Visible= false;   
                EditMode = false;
            }
            else
            {
                btnSave.Text = "Update";
                btnDelete.Visible = true;
                EditMode = true;
            }

            VoteShowAllPanel.Visible = false;
            VoteAddEditPanel.Visible = true;
            VoteAddEditPanel.Left = (MainGBox.Width - VoteAddEditPanel.Width) / 2;
            VoteAddEditPanel.Top = (MainGBox.Height - VoteAddEditPanel.Height) / 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ShowAllPanel();
        }
        private void ClearFields()
        {
            txtId.Text = "0";
            txtVoteName.Text = "";
            txtVoteAbbrev.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate textbox

            if (string.IsNullOrEmpty(txtVoteName.Text.Trim()))
            {
                MessageBox.Show("VoteName Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtVoteName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtVoteAbbrev.Text.Trim()))
            {
                MessageBox.Show("Vote Abbreviation Cannot be Empty", "@Chamaz", MessageBoxButtons.OK);
                txtVoteAbbrev.Focus();
                return;
            }
            
            if (SqliteDataAccess.CheckIfVoteExist(txtVoteName.Text.Trim(), Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("VoteName is already available", "@Chamaz", MessageBoxButtons.OK);
                txtVoteName.Focus();
                return;
            }
            
            //check if votename exists


            VoteModel vote = new VoteModel()
            {
                Id=Convert.ToInt32(txtId.Text),
                VoteName = SqliteDataAccess.ToPropercase(txtVoteName.Text),
                VoteAbbrev = SqliteDataAccess.ToPropercase(txtVoteAbbrev.Text),
            };

            if (EditMode) //update data
            {
                if (SqliteDataAccess.UpdateVote(vote) > 0)
                {
                    MessageBox.Show($"Vote : {txtVoteName.Text.Trim()} Updated", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Update Vote : {txtVoteName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            else //save new record
            {
                if (SqliteDataAccess.InsertVote(vote) > 0)
                {
                    MessageBox.Show($"Vote : {txtVoteName.Text.Trim()} Added", "@Chamaz", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show($"Failed to Add Vote : {txtVoteName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }

            ShowAllPanel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgVotes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                     
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to detete Vote :  {txtVoteName.Text.Trim()}", "@Chamaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (SqliteDataAccess.DeleteVote(Convert.ToInt32(txtId.Text.Trim())) > 0)
                {
                    MessageBox.Show($"Vote : {txtVoteName.Text.Trim()} Deleted", "@Chamaz", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Failed to Delete Vote : {txtVoteName.Text.Trim()}", "@Chamaz", MessageBoxButtons.OK);
                }
            }
            ShowAllPanel();
        }

        private void dtgVotes_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dtgrow = dtgVotes.CurrentRow;
            txtId.Text = dtgrow.Cells[0].Value.ToString(); ;
            txtVoteName.Text = dtgrow.Cells[1].Value.ToString();
            txtVoteAbbrev.Text = dtgrow.Cells[2].Value.ToString();
            ShowAddEditPanel(false);
        }
    }
}
