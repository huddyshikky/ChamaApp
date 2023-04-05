﻿namespace ChamaApp
{
    partial class Frm_ReceiptMembers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCsbkId = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.MemberCreditAddEditPanel = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblMemberName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtTotalAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboPayMode = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_AmountAllocated = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_AmountRemaining = new System.Windows.Forms.Label();
            this.btnRemoveSelectedIIems = new System.Windows.Forms.Button();
            this.btnRemoveAllIIems = new System.Windows.Forms.Button();
            this.btnAddIem = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtItemAmount = new System.Windows.Forms.TextBox();
            this.dtgItems = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MainGBox = new System.Windows.Forms.GroupBox();
            this.MemberCreditShowAllPanel = new System.Windows.Forms.Panel();
            this.dtgMemberReceipts = new System.Windows.Forms.DataGridView();
            this.txtMemberId = new System.Windows.Forms.TextBox();
            this.cboMemberName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MemberCreditAddEditPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgItems)).BeginInit();
            this.panel2.SuspendLayout();
            this.MainGBox.SuspendLayout();
            this.MemberCreditShowAllPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMemberReceipts)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCsbkId
            // 
            this.txtCsbkId.Font = new System.Drawing.Font("Roboto Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCsbkId.Location = new System.Drawing.Point(446, 46);
            this.txtCsbkId.Name = "txtCsbkId";
            this.txtCsbkId.Size = new System.Drawing.Size(45, 23);
            this.txtCsbkId.TabIndex = 8;
            this.txtCsbkId.Visible = false;
            this.txtCsbkId.WordWrap = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.LightGray;
            this.btnAdd.Location = new System.Drawing.Point(14, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(103, 29);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // MemberCreditAddEditPanel
            // 
            this.MemberCreditAddEditPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemberCreditAddEditPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MemberCreditAddEditPanel.Controls.Add(this.btnDelete);
            this.MemberCreditAddEditPanel.Controls.Add(this.lblMemberName);
            this.MemberCreditAddEditPanel.Controls.Add(this.btnCancel);
            this.MemberCreditAddEditPanel.Controls.Add(this.label5);
            this.MemberCreditAddEditPanel.Controls.Add(this.btnSave);
            this.MemberCreditAddEditPanel.Controls.Add(this.dtpTransDate);
            this.MemberCreditAddEditPanel.Controls.Add(this.label4);
            this.MemberCreditAddEditPanel.Controls.Add(this.TxtTotalAmount);
            this.MemberCreditAddEditPanel.Controls.Add(this.label7);
            this.MemberCreditAddEditPanel.Controls.Add(this.cboPayMode);
            this.MemberCreditAddEditPanel.Controls.Add(this.groupBox1);
            this.MemberCreditAddEditPanel.Location = new System.Drawing.Point(3, 35);
            this.MemberCreditAddEditPanel.Name = "MemberCreditAddEditPanel";
            this.MemberCreditAddEditPanel.Size = new System.Drawing.Size(694, 378);
            this.MemberCreditAddEditPanel.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.LightGray;
            this.btnDelete.Location = new System.Drawing.Point(529, 309);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 26);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblMemberName
            // 
            this.lblMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMemberName.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberName.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lblMemberName.Location = new System.Drawing.Point(65, 7);
            this.lblMemberName.Margin = new System.Windows.Forms.Padding(0);
            this.lblMemberName.Name = "lblMemberName";
            this.lblMemberName.Size = new System.Drawing.Size(555, 25);
            this.lblMemberName.TabIndex = 39;
            this.lblMemberName.Text = "Member Name";
            this.lblMemberName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(447, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 26);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(12, 47);
            this.label5.MaximumSize = new System.Drawing.Size(43, 26);
            this.label5.MinimumSize = new System.Drawing.Size(43, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 26);
            this.label5.TabIndex = 36;
            this.label5.Text = "Date :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.LightGray;
            this.btnSave.Location = new System.Drawing.Point(611, 309);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 26);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransDate.Location = new System.Drawing.Point(61, 46);
            this.dtpTransDate.MaxDate = new System.DateTime(2090, 12, 31, 0, 0, 0, 0);
            this.dtpTransDate.MaximumSize = new System.Drawing.Size(126, 26);
            this.dtpTransDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtpTransDate.MinimumSize = new System.Drawing.Size(126, 26);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(126, 26);
            this.dtpTransDate.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(193, 43);
            this.label4.MaximumSize = new System.Drawing.Size(66, 26);
            this.label4.MinimumSize = new System.Drawing.Size(66, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 26);
            this.label4.TabIndex = 34;
            this.label4.Text = "Amount :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtTotalAmount
            // 
            this.TxtTotalAmount.Font = new System.Drawing.Font("Roboto", 11F);
            this.TxtTotalAmount.Location = new System.Drawing.Point(265, 46);
            this.TxtTotalAmount.MaximumSize = new System.Drawing.Size(178, 25);
            this.TxtTotalAmount.MinimumSize = new System.Drawing.Size(178, 25);
            this.TxtTotalAmount.Name = "TxtTotalAmount";
            this.TxtTotalAmount.Size = new System.Drawing.Size(178, 25);
            this.TxtTotalAmount.TabIndex = 33;
            this.TxtTotalAmount.Text = "0.00";
            this.TxtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTotalAmount.TextChanged += new System.EventHandler(this.TxtTotalAmount_TextChanged);
            this.TxtTotalAmount.Leave += new System.EventHandler(this.TxtTotalAmount_Leave);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.LightGray;
            this.label7.Location = new System.Drawing.Point(449, 43);
            this.label7.MaximumSize = new System.Drawing.Size(86, 26);
            this.label7.MinimumSize = new System.Drawing.Size(86, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 26);
            this.label7.TabIndex = 35;
            this.label7.Text = "Pay Mode :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPayMode
            // 
            this.cboPayMode.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.cboPayMode.FormattingEnabled = true;
            this.cboPayMode.Items.AddRange(new object[] {
            "Bank",
            "Cheque",
            "Cash"});
            this.cboPayMode.Location = new System.Drawing.Point(541, 46);
            this.cboPayMode.MaximumSize = new System.Drawing.Size(134, 0);
            this.cboPayMode.MinimumSize = new System.Drawing.Size(134, 0);
            this.cboPayMode.Name = "cboPayMode";
            this.cboPayMode.Size = new System.Drawing.Size(134, 26);
            this.cboPayMode.TabIndex = 38;
            this.cboPayMode.SelectedIndexChanged += new System.EventHandler(this.cboPayMode_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lbl_AmountAllocated);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lbl_AmountRemaining);
            this.groupBox1.Controls.Add(this.btnRemoveSelectedIIems);
            this.groupBox1.Controls.Add(this.btnRemoveAllIIems);
            this.groupBox1.Controls.Add(this.btnAddIem);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboItemName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TxtItemAmount);
            this.groupBox1.Controls.Add(this.dtgItems);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(8, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 225);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items Allocation";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.LightGray;
            this.label11.Location = new System.Drawing.Point(529, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "Allocated :";
            // 
            // lbl_AmountAllocated
            // 
            this.lbl_AmountAllocated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_AmountAllocated.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_AmountAllocated.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lbl_AmountAllocated.Location = new System.Drawing.Point(451, 30);
            this.lbl_AmountAllocated.Name = "lbl_AmountAllocated";
            this.lbl_AmountAllocated.Size = new System.Drawing.Size(152, 23);
            this.lbl_AmountAllocated.TabIndex = 26;
            this.lbl_AmountAllocated.Text = "1,000,000.00";
            this.lbl_AmountAllocated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.LightGray;
            this.label10.Location = new System.Drawing.Point(266, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Remaining :";
            // 
            // lbl_AmountRemaining
            // 
            this.lbl_AmountRemaining.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_AmountRemaining.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_AmountRemaining.ForeColor = System.Drawing.Color.IndianRed;
            this.lbl_AmountRemaining.Location = new System.Drawing.Point(269, 30);
            this.lbl_AmountRemaining.Name = "lbl_AmountRemaining";
            this.lbl_AmountRemaining.Size = new System.Drawing.Size(152, 23);
            this.lbl_AmountRemaining.TabIndex = 24;
            this.lbl_AmountRemaining.Text = "1,000,000.00";
            this.lbl_AmountRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRemoveSelectedIIems
            // 
            this.btnRemoveSelectedIIems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnRemoveSelectedIIems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelectedIIems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveSelectedIIems.ForeColor = System.Drawing.Color.LightGray;
            this.btnRemoveSelectedIIems.Location = new System.Drawing.Point(468, 186);
            this.btnRemoveSelectedIIems.Name = "btnRemoveSelectedIIems";
            this.btnRemoveSelectedIIems.Size = new System.Drawing.Size(144, 29);
            this.btnRemoveSelectedIIems.TabIndex = 23;
            this.btnRemoveSelectedIIems.Text = "Remove Selected";
            this.btnRemoveSelectedIIems.UseVisualStyleBackColor = false;
            this.btnRemoveSelectedIIems.Click += new System.EventHandler(this.btnRemoveSelectedIIems_Click);
            // 
            // btnRemoveAllIIems
            // 
            this.btnRemoveAllIIems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnRemoveAllIIems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAllIIems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAllIIems.ForeColor = System.Drawing.Color.LightGray;
            this.btnRemoveAllIIems.Location = new System.Drawing.Point(275, 186);
            this.btnRemoveAllIIems.Name = "btnRemoveAllIIems";
            this.btnRemoveAllIIems.Size = new System.Drawing.Size(144, 29);
            this.btnRemoveAllIIems.TabIndex = 22;
            this.btnRemoveAllIIems.Text = "Remove All Items";
            this.btnRemoveAllIIems.UseVisualStyleBackColor = false;
            this.btnRemoveAllIIems.Click += new System.EventHandler(this.btnRemoveAllIIems_Click);
            // 
            // btnAddIem
            // 
            this.btnAddIem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnAddIem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddIem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddIem.ForeColor = System.Drawing.Color.LightGray;
            this.btnAddIem.Location = new System.Drawing.Point(160, 106);
            this.btnAddIem.Name = "btnAddIem";
            this.btnAddIem.Size = new System.Drawing.Size(103, 29);
            this.btnAddIem.TabIndex = 17;
            this.btnAddIem.Text = "Add Item";
            this.btnAddIem.UseVisualStyleBackColor = false;
            this.btnAddIem.Click += new System.EventHandler(this.btnAddIem_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(6, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Item Amount :";
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(108, 39);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(155, 26);
            this.cboItemName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(20, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Item Name :";
            // 
            // TxtItemAmount
            // 
            this.TxtItemAmount.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtItemAmount.Location = new System.Drawing.Point(110, 73);
            this.TxtItemAmount.Name = "TxtItemAmount";
            this.TxtItemAmount.Size = new System.Drawing.Size(153, 27);
            this.TxtItemAmount.TabIndex = 15;
            this.TxtItemAmount.Text = "0.00";
            this.TxtItemAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtgItems
            // 
            this.dtgItems.AllowUserToAddRows = false;
            this.dtgItems.AllowUserToDeleteRows = false;
            this.dtgItems.AllowUserToResizeColumns = false;
            this.dtgItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dtgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Item,
            this.Amount});
            this.dtgItems.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgItems.DefaultCellStyle = dataGridViewCellStyle9;
            this.dtgItems.EnableHeadersVisualStyles = false;
            this.dtgItems.Location = new System.Drawing.Point(269, 56);
            this.dtgItems.Name = "dtgItems";
            this.dtgItems.ReadOnly = true;
            this.dtgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgItems.Size = new System.Drawing.Size(343, 124);
            this.dtgItems.TabIndex = 21;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Item
            // 
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.panel2.Controls.Add(this.MainGBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 424);
            this.panel2.TabIndex = 11;
            // 
            // MainGBox
            // 
            this.MainGBox.Controls.Add(this.MemberCreditAddEditPanel);
            this.MainGBox.Controls.Add(this.MemberCreditShowAllPanel);
            this.MainGBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MainGBox.Location = new System.Drawing.Point(0, 0);
            this.MainGBox.Name = "MainGBox";
            this.MainGBox.Size = new System.Drawing.Size(718, 424);
            this.MainGBox.TabIndex = 3;
            this.MainGBox.TabStop = false;
            // 
            // MemberCreditShowAllPanel
            // 
            this.MemberCreditShowAllPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemberCreditShowAllPanel.Controls.Add(this.dtgMemberReceipts);
            this.MemberCreditShowAllPanel.Controls.Add(this.txtMemberId);
            this.MemberCreditShowAllPanel.Controls.Add(this.cboMemberName);
            this.MemberCreditShowAllPanel.Controls.Add(this.label2);
            this.MemberCreditShowAllPanel.Controls.Add(this.btnAdd);
            this.MemberCreditShowAllPanel.Controls.Add(this.txtCsbkId);
            this.MemberCreditShowAllPanel.Location = new System.Drawing.Point(32, 19);
            this.MemberCreditShowAllPanel.Name = "MemberCreditShowAllPanel";
            this.MemberCreditShowAllPanel.Size = new System.Drawing.Size(653, 322);
            this.MemberCreditShowAllPanel.TabIndex = 2;
            // 
            // dtgMemberReceipts
            // 
            this.dtgMemberReceipts.AllowUserToAddRows = false;
            this.dtgMemberReceipts.AllowUserToDeleteRows = false;
            this.dtgMemberReceipts.AllowUserToResizeColumns = false;
            this.dtgMemberReceipts.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgMemberReceipts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dtgMemberReceipts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgMemberReceipts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgMemberReceipts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dtgMemberReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgMemberReceipts.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgMemberReceipts.DefaultCellStyle = dataGridViewCellStyle12;
            this.dtgMemberReceipts.EnableHeadersVisualStyles = false;
            this.dtgMemberReceipts.Location = new System.Drawing.Point(21, 95);
            this.dtgMemberReceipts.Name = "dtgMemberReceipts";
            this.dtgMemberReceipts.ReadOnly = true;
            this.dtgMemberReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgMemberReceipts.Size = new System.Drawing.Size(617, 160);
            this.dtgMemberReceipts.TabIndex = 9;
            this.dtgMemberReceipts.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgMemberReceipts_RowHeaderMouseDoubleClick);
            // 
            // txtMemberId
            // 
            this.txtMemberId.Location = new System.Drawing.Point(527, 47);
            this.txtMemberId.Name = "txtMemberId";
            this.txtMemberId.Size = new System.Drawing.Size(57, 20);
            this.txtMemberId.TabIndex = 8;
            // 
            // cboMemberName
            // 
            this.cboMemberName.FormattingEnabled = true;
            this.cboMemberName.Location = new System.Drawing.Point(123, 8);
            this.cboMemberName.Name = "cboMemberName";
            this.cboMemberName.Size = new System.Drawing.Size(515, 21);
            this.cboMemberName.TabIndex = 7;
            this.cboMemberName.SelectedIndexChanged += new System.EventHandler(this.cboMemberName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(11, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Member Name :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Hoefler Text Black", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(275, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(443, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Member Credit Transactions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(353, 422);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 36);
            this.button4.TabIndex = 9;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(475, 422);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 36);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(597, 422);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.LightGray;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 45);
            this.panel1.TabIndex = 10;
            // 
            // Frm_ReceiptMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 469);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_ReceiptMembers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_ReceiptMembers";
            this.Load += new System.EventHandler(this.Frm_ReceiptMembers_Load);
            this.MemberCreditAddEditPanel.ResumeLayout(false);
            this.MemberCreditAddEditPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgItems)).EndInit();
            this.panel2.ResumeLayout(false);
            this.MainGBox.ResumeLayout(false);
            this.MemberCreditShowAllPanel.ResumeLayout(false);
            this.MemberCreditShowAllPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMemberReceipts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtCsbkId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel MemberCreditAddEditPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox MainGBox;
        private System.Windows.Forms.Panel MemberCreditShowAllPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboMemberName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtItemAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddIem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_AmountAllocated;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_AmountRemaining;
        private System.Windows.Forms.Button btnRemoveSelectedIIems;
        private System.Windows.Forms.Button btnRemoveAllIIems;
        private System.Windows.Forms.DataGridView dtgItems;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtTotalAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPayMode;
        private System.Windows.Forms.Label lblMemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.TextBox txtMemberId;
        private System.Windows.Forms.DataGridView dtgMemberReceipts;
    }
}