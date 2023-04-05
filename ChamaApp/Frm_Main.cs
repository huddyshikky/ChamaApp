﻿using System;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            HideAllSubMenus();
        }
        
        private void HideAllSubMenus()
        {
            HideSubMunu(panelRegistrationSubMenu);
            HideSubMunu(panelTransactionsSubMenu);
            HideSubMunu(panelLoanSubMenu);
            HideSubMunu(panelUserSubMenu);

        }
        private void ShowSubMenu(Panel SubMenu)
        {
            if (SubMenu.Visible == false)
            {
                HideAllSubMenus();
                SubMenu.Visible = true;
            }
            else
            {
                SubMenu.Visible = false;
            }
        }
        private void HideSubMunu(Panel SubMenu)
        {
            SubMenu.Visible = false;
            
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelRegistrationSubMenu);
        }
        #region RegistrationSubMenu
        private void btnVotes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Frm_Votes());
            HideAllSubMenus();
        }

        private void btnLoanType_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Frm_LoanTypes());
            HideAllSubMenus();
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Frm_Members());
            //OpenChildForm(new FrmVoteMaterial());
            HideAllSubMenus();
        }
        #endregion

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelTransactionsSubMenu);
        }
        #region TransactionSubMenu
        private void btnReceiptMembers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Frm_ReceiptMembers());
            HideAllSubMenus();
        }

        private void btnReceiptOthers_Click(object sender, EventArgs e)
        {
            HideAllSubMenus();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            HideAllSubMenus();
        }

        #endregion

        private void btnLoanManagement_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelLoanSubMenu);
        }
        #region LoanManagementSubMenu
        private void btnIssueLoan_Click(object sender, EventArgs e)
        {
            HideAllSubMenus();
        }

        private void btnRepayLoan_Click(object sender, EventArgs e)
        {
            HideAllSubMenus();
        }
        #endregion

        private void btnUserManagement_Click(object sender, EventArgs e)
        {

            ShowSubMenu(panelUserSubMenu);
        }
        #region UserManagementSubMenu
        private void btnRegisterNonMembers_Click(object sender, EventArgs e)
        {
            HideAllSubMenus();
        }

        private void btnAssignRoles_Click(object sender, EventArgs e)
        {
            HideAllSubMenus();
        }
        #endregion


        #region ChildForm Logic
        private Form ActiveForm = null;
        private void OpenChildForm(Form ChildForm)
        {
            //close any open child form
            if (ActiveForm != null)
                ActiveForm.Close();


            ActiveForm = ChildForm;

            //make childform as control
            ChildForm.TopLevel= false;
            ChildForm.FormBorderStyle= FormBorderStyle.None;
            ChildForm.Dock= DockStyle.Fill;
            panelChildForm.Controls.Add(ChildForm);
            //asssociate the form with the continer panel
            panelChildForm.Tag= ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        #endregion

        private void btnRegistration_MouseHover(object sender, EventArgs e)
        {
            //btnRegistration.ForeColor = Color.FromArgb(11, 7, 17);
           
        }

        private void btnRegistration_MouseLeave(object sender, EventArgs e)
        {
            btnRegistration.ForeColor = Color.Gainsboro;
        }
    }
}