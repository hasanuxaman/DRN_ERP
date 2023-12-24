namespace DRN_WIN_ERP.Modules.Sales
{
    partial class frmSalesReport
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.optDoExecute = new System.Windows.Forms.RadioButton();
            this.optDoPending = new System.Windows.Forms.RadioButton();
            this.optDoCreated = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDoDateTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDoDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrintDo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optSalesPerson = new System.Windows.Forms.RadioButton();
            this.optItemName = new System.Windows.Forms.RadioButton();
            this.optCustomer = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpChlnDateTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpChlnDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtChlnFilter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPrintChln = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox3.Controls.Add(this.optDoExecute);
            this.groupBox3.Controls.Add(this.optDoPending);
            this.groupBox3.Controls.Add(this.optDoCreated);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dtpDoDateTo);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dtpDoDateFrom);
            this.groupBox3.Controls.Add(this.txtCustName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnPrintDo);
            this.groupBox3.Location = new System.Drawing.Point(34, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1297, 55);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "D/O Report";
            // 
            // optDoExecute
            // 
            this.optDoExecute.AutoSize = true;
            this.optDoExecute.Location = new System.Drawing.Point(158, 21);
            this.optDoExecute.Name = "optDoExecute";
            this.optDoExecute.Size = new System.Drawing.Size(114, 17);
            this.optDoExecute.TabIndex = 28;
            this.optDoExecute.Text = "Executed (Challan)";
            this.optDoExecute.UseVisualStyleBackColor = true;
            // 
            // optDoPending
            // 
            this.optDoPending.AutoSize = true;
            this.optDoPending.Location = new System.Drawing.Point(90, 21);
            this.optDoPending.Name = "optDoPending";
            this.optDoPending.Size = new System.Drawing.Size(64, 17);
            this.optDoPending.TabIndex = 27;
            this.optDoPending.Text = "Pending";
            this.optDoPending.UseVisualStyleBackColor = true;
            // 
            // optDoCreated
            // 
            this.optDoCreated.AutoSize = true;
            this.optDoCreated.Checked = true;
            this.optDoCreated.Location = new System.Drawing.Point(22, 21);
            this.optDoCreated.Name = "optDoCreated";
            this.optDoCreated.Size = new System.Drawing.Size(62, 17);
            this.optDoCreated.TabIndex = 26;
            this.optDoCreated.TabStop = true;
            this.optDoCreated.Text = "Created";
            this.optDoCreated.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(450, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "To Date: ";
            // 
            // dtpDoDateTo
            // 
            this.dtpDoDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDoDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoDateTo.Location = new System.Drawing.Point(508, 21);
            this.dtpDoDateTo.Name = "dtpDoDateTo";
            this.dtpDoDateTo.Size = new System.Drawing.Size(87, 20);
            this.dtpDoDateTo.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "From Date: ";
            // 
            // dtpDoDateFrom
            // 
            this.dtpDoDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDoDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoDateFrom.Location = new System.Drawing.Point(356, 21);
            this.dtpDoDateFrom.Name = "dtpDoDateFrom";
            this.dtpDoDateFrom.Size = new System.Drawing.Size(87, 20);
            this.dtpDoDateFrom.TabIndex = 23;
            // 
            // txtCustName
            // 
            this.txtCustName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtCustName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCustName.Location = new System.Drawing.Point(716, 21);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(467, 20);
            this.txtCustName.TabIndex = 1;
            this.txtCustName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCustName_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(624, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Customer Name:";
            // 
            // btnPrintDo
            // 
            this.btnPrintDo.Location = new System.Drawing.Point(1202, 21);
            this.btnPrintDo.Name = "btnPrintDo";
            this.btnPrintDo.Size = new System.Drawing.Size(75, 23);
            this.btnPrintDo.TabIndex = 2;
            this.btnPrintDo.Text = "Print";
            this.btnPrintDo.UseVisualStyleBackColor = true;
            this.btnPrintDo.Click += new System.EventHandler(this.btnPrintDo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.optSalesPerson);
            this.groupBox1.Controls.Add(this.optItemName);
            this.groupBox1.Controls.Add(this.optCustomer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpChlnDateTo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpChlnDateFrom);
            this.groupBox1.Controls.Add(this.txtChlnFilter);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnPrintChln);
            this.groupBox1.Location = new System.Drawing.Point(33, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1297, 55);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Challan Report";
            // 
            // optSalesPerson
            // 
            this.optSalesPerson.AutoSize = true;
            this.optSalesPerson.Location = new System.Drawing.Point(175, 21);
            this.optSalesPerson.Name = "optSalesPerson";
            this.optSalesPerson.Size = new System.Drawing.Size(87, 17);
            this.optSalesPerson.TabIndex = 28;
            this.optSalesPerson.Text = "Sales Person";
            this.optSalesPerson.UseVisualStyleBackColor = true;
            this.optSalesPerson.CheckedChanged += new System.EventHandler(this.optSalesPerson_CheckedChanged);
            // 
            // optItemName
            // 
            this.optItemName.AutoSize = true;
            this.optItemName.Location = new System.Drawing.Point(92, 21);
            this.optItemName.Name = "optItemName";
            this.optItemName.Size = new System.Drawing.Size(76, 17);
            this.optItemName.TabIndex = 27;
            this.optItemName.Text = "Item Name";
            this.optItemName.UseVisualStyleBackColor = true;
            this.optItemName.CheckedChanged += new System.EventHandler(this.optItemName_CheckedChanged);
            // 
            // optCustomer
            // 
            this.optCustomer.AutoSize = true;
            this.optCustomer.Checked = true;
            this.optCustomer.Location = new System.Drawing.Point(22, 21);
            this.optCustomer.Name = "optCustomer";
            this.optCustomer.Size = new System.Drawing.Size(69, 17);
            this.optCustomer.TabIndex = 26;
            this.optCustomer.TabStop = true;
            this.optCustomer.Text = "Customer";
            this.optCustomer.UseVisualStyleBackColor = true;
            this.optCustomer.CheckedChanged += new System.EventHandler(this.optCustomer_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(450, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "To Date: ";
            // 
            // dtpChlnDateTo
            // 
            this.dtpChlnDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpChlnDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChlnDateTo.Location = new System.Drawing.Point(508, 21);
            this.dtpChlnDateTo.Name = "dtpChlnDateTo";
            this.dtpChlnDateTo.Size = new System.Drawing.Size(87, 20);
            this.dtpChlnDateTo.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "From Date: ";
            // 
            // dtpChlnDateFrom
            // 
            this.dtpChlnDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpChlnDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChlnDateFrom.Location = new System.Drawing.Point(356, 21);
            this.dtpChlnDateFrom.Name = "dtpChlnDateFrom";
            this.dtpChlnDateFrom.Size = new System.Drawing.Size(87, 20);
            this.dtpChlnDateFrom.TabIndex = 23;
            // 
            // txtChlnFilter
            // 
            this.txtChlnFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtChlnFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtChlnFilter.Location = new System.Drawing.Point(716, 21);
            this.txtChlnFilter.Name = "txtChlnFilter";
            this.txtChlnFilter.Size = new System.Drawing.Size(467, 20);
            this.txtChlnFilter.TabIndex = 1;
            this.txtChlnFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtChlnFilter_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(631, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Search Filter:";
            // 
            // btnPrintChln
            // 
            this.btnPrintChln.Location = new System.Drawing.Point(1202, 21);
            this.btnPrintChln.Name = "btnPrintChln";
            this.btnPrintChln.Size = new System.Drawing.Size(75, 23);
            this.btnPrintChln.TabIndex = 2;
            this.btnPrintChln.Text = "Print";
            this.btnPrintChln.UseVisualStyleBackColor = true;
            this.btnPrintChln.Click += new System.EventHandler(this.btnPrintChln_Click);
            // 
            // frmSalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1362, 571);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmSalesReport";
            this.Text = "Sales Report";
            this.Load += new System.EventHandler(this.frmSalesReport_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintDo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDoDateTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDoDateFrom;
        private System.Windows.Forms.RadioButton optDoExecute;
        private System.Windows.Forms.RadioButton optDoPending;
        private System.Windows.Forms.RadioButton optDoCreated;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optSalesPerson;
        private System.Windows.Forms.RadioButton optItemName;
        private System.Windows.Forms.RadioButton optCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpChlnDateTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpChlnDateFrom;
        private System.Windows.Forms.TextBox txtChlnFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPrintChln;
    }
}