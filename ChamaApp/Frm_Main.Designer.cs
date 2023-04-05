namespace ChamaApp
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.panelUserSubMenu = new System.Windows.Forms.Panel();
            this.btnAssignRoles = new System.Windows.Forms.Button();
            this.btnRegisterNonMembers = new System.Windows.Forms.Button();
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.panelLoanSubMenu = new System.Windows.Forms.Panel();
            this.btnRepayLoan = new System.Windows.Forms.Button();
            this.btnIssueLoan = new System.Windows.Forms.Button();
            this.btnLoanManagement = new System.Windows.Forms.Button();
            this.panelTransactionsSubMenu = new System.Windows.Forms.Panel();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnReceiptOthers = new System.Windows.Forms.Button();
            this.btnReceiptMembers = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.panelRegistrationSubMenu = new System.Windows.Forms.Panel();
            this.btnMembers = new System.Windows.Forms.Button();
            this.btnLoanType = new System.Windows.Forms.Button();
            this.btnVotes = new System.Windows.Forms.Button();
            this.btnRegistration = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSideMenu.SuspendLayout();
            this.panelUserSubMenu.SuspendLayout();
            this.panelLoanSubMenu.SuspendLayout();
            this.panelTransactionsSubMenu.SuspendLayout();
            this.panelRegistrationSubMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.AutoScroll = true;
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.panelSideMenu.Controls.Add(this.panelUserSubMenu);
            this.panelSideMenu.Controls.Add(this.btnUserManagement);
            this.panelSideMenu.Controls.Add(this.panelLoanSubMenu);
            this.panelSideMenu.Controls.Add(this.btnLoanManagement);
            this.panelSideMenu.Controls.Add(this.panelTransactionsSubMenu);
            this.panelSideMenu.Controls.Add(this.btnTransactions);
            this.panelSideMenu.Controls.Add(this.panelRegistrationSubMenu);
            this.panelSideMenu.Controls.Add(this.btnRegistration);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(250, 561);
            this.panelSideMenu.TabIndex = 0;
            // 
            // panelUserSubMenu
            // 
            this.panelUserSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panelUserSubMenu.Controls.Add(this.btnAssignRoles);
            this.panelUserSubMenu.Controls.Add(this.btnRegisterNonMembers);
            this.panelUserSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUserSubMenu.Location = new System.Drawing.Point(0, 599);
            this.panelUserSubMenu.Name = "panelUserSubMenu";
            this.panelUserSubMenu.Size = new System.Drawing.Size(233, 88);
            this.panelUserSubMenu.TabIndex = 7;
            // 
            // btnAssignRoles
            // 
            this.btnAssignRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAssignRoles.FlatAppearance.BorderSize = 0;
            this.btnAssignRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnAssignRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAssignRoles.ForeColor = System.Drawing.Color.LightGray;
            this.btnAssignRoles.Location = new System.Drawing.Point(0, 40);
            this.btnAssignRoles.Name = "btnAssignRoles";
            this.btnAssignRoles.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnAssignRoles.Size = new System.Drawing.Size(233, 40);
            this.btnAssignRoles.TabIndex = 2;
            this.btnAssignRoles.Text = "Assign Roles";
            this.btnAssignRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAssignRoles.UseVisualStyleBackColor = true;
            this.btnAssignRoles.Click += new System.EventHandler(this.btnAssignRoles_Click);
            // 
            // btnRegisterNonMembers
            // 
            this.btnRegisterNonMembers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegisterNonMembers.FlatAppearance.BorderSize = 0;
            this.btnRegisterNonMembers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnRegisterNonMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterNonMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnRegisterNonMembers.ForeColor = System.Drawing.Color.LightGray;
            this.btnRegisterNonMembers.Location = new System.Drawing.Point(0, 0);
            this.btnRegisterNonMembers.Name = "btnRegisterNonMembers";
            this.btnRegisterNonMembers.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnRegisterNonMembers.Size = new System.Drawing.Size(233, 40);
            this.btnRegisterNonMembers.TabIndex = 1;
            this.btnRegisterNonMembers.Text = "Non Member Users";
            this.btnRegisterNonMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegisterNonMembers.UseVisualStyleBackColor = true;
            this.btnRegisterNonMembers.Click += new System.EventHandler(this.btnRegisterNonMembers_Click);
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUserManagement.FlatAppearance.BorderSize = 0;
            this.btnUserManagement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnUserManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnUserManagement.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnUserManagement.Location = new System.Drawing.Point(0, 554);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUserManagement.Size = new System.Drawing.Size(233, 45);
            this.btnUserManagement.TabIndex = 6;
            this.btnUserManagement.Text = "User Management";
            this.btnUserManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserManagement.UseVisualStyleBackColor = true;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // panelLoanSubMenu
            // 
            this.panelLoanSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panelLoanSubMenu.Controls.Add(this.btnRepayLoan);
            this.panelLoanSubMenu.Controls.Add(this.btnIssueLoan);
            this.panelLoanSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLoanSubMenu.Location = new System.Drawing.Point(0, 468);
            this.panelLoanSubMenu.Name = "panelLoanSubMenu";
            this.panelLoanSubMenu.Size = new System.Drawing.Size(233, 86);
            this.panelLoanSubMenu.TabIndex = 5;
            // 
            // btnRepayLoan
            // 
            this.btnRepayLoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRepayLoan.FlatAppearance.BorderSize = 0;
            this.btnRepayLoan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnRepayLoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepayLoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnRepayLoan.ForeColor = System.Drawing.Color.LightGray;
            this.btnRepayLoan.Location = new System.Drawing.Point(0, 40);
            this.btnRepayLoan.Name = "btnRepayLoan";
            this.btnRepayLoan.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnRepayLoan.Size = new System.Drawing.Size(233, 40);
            this.btnRepayLoan.TabIndex = 2;
            this.btnRepayLoan.Text = "Loan Repayment";
            this.btnRepayLoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepayLoan.UseVisualStyleBackColor = true;
            this.btnRepayLoan.Click += new System.EventHandler(this.btnRepayLoan_Click);
            // 
            // btnIssueLoan
            // 
            this.btnIssueLoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnIssueLoan.FlatAppearance.BorderSize = 0;
            this.btnIssueLoan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnIssueLoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssueLoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnIssueLoan.ForeColor = System.Drawing.Color.LightGray;
            this.btnIssueLoan.Location = new System.Drawing.Point(0, 0);
            this.btnIssueLoan.Name = "btnIssueLoan";
            this.btnIssueLoan.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnIssueLoan.Size = new System.Drawing.Size(233, 40);
            this.btnIssueLoan.TabIndex = 1;
            this.btnIssueLoan.Text = "Issue Loan";
            this.btnIssueLoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueLoan.UseVisualStyleBackColor = true;
            this.btnIssueLoan.Click += new System.EventHandler(this.btnIssueLoan_Click);
            // 
            // btnLoanManagement
            // 
            this.btnLoanManagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoanManagement.FlatAppearance.BorderSize = 0;
            this.btnLoanManagement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnLoanManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoanManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnLoanManagement.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLoanManagement.Location = new System.Drawing.Point(0, 423);
            this.btnLoanManagement.Name = "btnLoanManagement";
            this.btnLoanManagement.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLoanManagement.Size = new System.Drawing.Size(233, 45);
            this.btnLoanManagement.TabIndex = 4;
            this.btnLoanManagement.Text = "Loan Management";
            this.btnLoanManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoanManagement.UseVisualStyleBackColor = true;
            this.btnLoanManagement.Click += new System.EventHandler(this.btnLoanManagement_Click);
            // 
            // panelTransactionsSubMenu
            // 
            this.panelTransactionsSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panelTransactionsSubMenu.Controls.Add(this.btnPayments);
            this.panelTransactionsSubMenu.Controls.Add(this.btnReceiptOthers);
            this.panelTransactionsSubMenu.Controls.Add(this.btnReceiptMembers);
            this.panelTransactionsSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTransactionsSubMenu.Location = new System.Drawing.Point(0, 297);
            this.panelTransactionsSubMenu.Name = "panelTransactionsSubMenu";
            this.panelTransactionsSubMenu.Size = new System.Drawing.Size(233, 126);
            this.panelTransactionsSubMenu.TabIndex = 3;
            // 
            // btnPayments
            // 
            this.btnPayments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPayments.FlatAppearance.BorderSize = 0;
            this.btnPayments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayments.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPayments.ForeColor = System.Drawing.Color.LightGray;
            this.btnPayments.Location = new System.Drawing.Point(0, 80);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnPayments.Size = new System.Drawing.Size(233, 40);
            this.btnPayments.TabIndex = 3;
            this.btnPayments.Text = "Payments";
            this.btnPayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayments.UseVisualStyleBackColor = true;
            this.btnPayments.Click += new System.EventHandler(this.btnPayments_Click);
            // 
            // btnReceiptOthers
            // 
            this.btnReceiptOthers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReceiptOthers.FlatAppearance.BorderSize = 0;
            this.btnReceiptOthers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnReceiptOthers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceiptOthers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnReceiptOthers.ForeColor = System.Drawing.Color.LightGray;
            this.btnReceiptOthers.Location = new System.Drawing.Point(0, 40);
            this.btnReceiptOthers.Name = "btnReceiptOthers";
            this.btnReceiptOthers.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnReceiptOthers.Size = new System.Drawing.Size(233, 40);
            this.btnReceiptOthers.TabIndex = 2;
            this.btnReceiptOthers.Text = "Receipts (Others)";
            this.btnReceiptOthers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceiptOthers.UseVisualStyleBackColor = true;
            this.btnReceiptOthers.Click += new System.EventHandler(this.btnReceiptOthers_Click);
            // 
            // btnReceiptMembers
            // 
            this.btnReceiptMembers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReceiptMembers.FlatAppearance.BorderSize = 0;
            this.btnReceiptMembers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnReceiptMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceiptMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnReceiptMembers.ForeColor = System.Drawing.Color.LightGray;
            this.btnReceiptMembers.Location = new System.Drawing.Point(0, 0);
            this.btnReceiptMembers.Name = "btnReceiptMembers";
            this.btnReceiptMembers.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnReceiptMembers.Size = new System.Drawing.Size(233, 40);
            this.btnReceiptMembers.TabIndex = 1;
            this.btnReceiptMembers.Text = "Receipts (Members)";
            this.btnReceiptMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceiptMembers.UseVisualStyleBackColor = true;
            this.btnReceiptMembers.Click += new System.EventHandler(this.btnReceiptMembers_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransactions.FlatAppearance.BorderSize = 0;
            this.btnTransactions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnTransactions.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnTransactions.Location = new System.Drawing.Point(0, 252);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnTransactions.Size = new System.Drawing.Size(233, 45);
            this.btnTransactions.TabIndex = 2;
            this.btnTransactions.Text = "Transactions";
            this.btnTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransactions.UseVisualStyleBackColor = true;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            // 
            // panelRegistrationSubMenu
            // 
            this.panelRegistrationSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panelRegistrationSubMenu.Controls.Add(this.btnMembers);
            this.panelRegistrationSubMenu.Controls.Add(this.btnLoanType);
            this.panelRegistrationSubMenu.Controls.Add(this.btnVotes);
            this.panelRegistrationSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRegistrationSubMenu.Location = new System.Drawing.Point(0, 120);
            this.panelRegistrationSubMenu.Name = "panelRegistrationSubMenu";
            this.panelRegistrationSubMenu.Size = new System.Drawing.Size(233, 132);
            this.panelRegistrationSubMenu.TabIndex = 1;
            // 
            // btnMembers
            // 
            this.btnMembers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMembers.FlatAppearance.BorderSize = 0;
            this.btnMembers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnMembers.ForeColor = System.Drawing.Color.LightGray;
            this.btnMembers.Location = new System.Drawing.Point(0, 80);
            this.btnMembers.Name = "btnMembers";
            this.btnMembers.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnMembers.Size = new System.Drawing.Size(233, 40);
            this.btnMembers.TabIndex = 3;
            this.btnMembers.Text = "Members";
            this.btnMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembers.UseVisualStyleBackColor = true;
            this.btnMembers.Click += new System.EventHandler(this.btnMembers_Click);
            // 
            // btnLoanType
            // 
            this.btnLoanType.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoanType.FlatAppearance.BorderSize = 0;
            this.btnLoanType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoanType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnLoanType.ForeColor = System.Drawing.Color.LightGray;
            this.btnLoanType.Location = new System.Drawing.Point(0, 40);
            this.btnLoanType.Name = "btnLoanType";
            this.btnLoanType.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnLoanType.Size = new System.Drawing.Size(233, 40);
            this.btnLoanType.TabIndex = 2;
            this.btnLoanType.Text = "Loan Types";
            this.btnLoanType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoanType.UseVisualStyleBackColor = true;
            this.btnLoanType.Click += new System.EventHandler(this.btnLoanType_Click);
            // 
            // btnVotes
            // 
            this.btnVotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVotes.FlatAppearance.BorderSize = 0;
            this.btnVotes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.btnVotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnVotes.ForeColor = System.Drawing.Color.LightGray;
            this.btnVotes.Location = new System.Drawing.Point(0, 0);
            this.btnVotes.Name = "btnVotes";
            this.btnVotes.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnVotes.Size = new System.Drawing.Size(233, 40);
            this.btnVotes.TabIndex = 1;
            this.btnVotes.Text = "Votes";
            this.btnVotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVotes.UseVisualStyleBackColor = true;
            this.btnVotes.Click += new System.EventHandler(this.btnVotes_Click);
            // 
            // btnRegistration
            // 
            this.btnRegistration.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegistration.FlatAppearance.BorderSize = 0;
            this.btnRegistration.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistration.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRegistration.Location = new System.Drawing.Point(0, 75);
            this.btnRegistration.Name = "btnRegistration";
            this.btnRegistration.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRegistration.Size = new System.Drawing.Size(233, 45);
            this.btnRegistration.TabIndex = 1;
            this.btnRegistration.Text = "Registration";
            this.btnRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistration.UseVisualStyleBackColor = true;
            this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
            this.btnRegistration.MouseLeave += new System.EventHandler(this.btnRegistration_MouseLeave);
            this.btnRegistration.MouseHover += new System.EventHandler(this.btnRegistration_MouseHover);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.pictureBox2);
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(233, 75);
            this.panelLogo.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ChamaApp.Properties.Resources.group;
            this.pictureBox2.Location = new System.Drawing.Point(155, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(68, 49);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Coronet", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(87)))));
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHAMAZ";
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(250, 508);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Size = new System.Drawing.Size(734, 53);
            this.panelStatusBar.TabIndex = 1;
            // 
            // panelChildForm
            // 
            this.panelChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.panelChildForm.Controls.Add(this.pictureBox1);
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.Location = new System.Drawing.Point(250, 0);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(734, 508);
            this.panelChildForm.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ChamaApp.Properties.Resources.group;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(734, 508);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelSideMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChamaApp";
            this.panelSideMenu.ResumeLayout(false);
            this.panelUserSubMenu.ResumeLayout(false);
            this.panelLoanSubMenu.ResumeLayout(false);
            this.panelTransactionsSubMenu.ResumeLayout(false);
            this.panelRegistrationSubMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelChildForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Button btnRegistration;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelRegistrationSubMenu;
        private System.Windows.Forms.Button btnLoanType;
        private System.Windows.Forms.Button btnVotes;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Panel panelTransactionsSubMenu;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnReceiptOthers;
        private System.Windows.Forms.Button btnReceiptMembers;
        private System.Windows.Forms.Panel panelLoanSubMenu;
        private System.Windows.Forms.Button btnRepayLoan;
        private System.Windows.Forms.Button btnIssueLoan;
        private System.Windows.Forms.Button btnLoanManagement;
        private System.Windows.Forms.Panel panelUserSubMenu;
        private System.Windows.Forms.Button btnAssignRoles;
        private System.Windows.Forms.Button btnRegisterNonMembers;
        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Button btnMembers;
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
    }
}

