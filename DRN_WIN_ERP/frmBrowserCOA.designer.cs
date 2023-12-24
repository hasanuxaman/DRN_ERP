namespace DRN_WIN_ERP
{
    partial class frmBrowserCOA
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnok = new System.Windows.Forms.Button();
            this.BrowserGrid = new System.Windows.Forms.DataGridView();
            this.lblSearchString = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BrowserGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(878, 482);
            this.webBrowser1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(12, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(752, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnok
            // 
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Location = new System.Drawing.Point(770, 21);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(87, 23);
            this.btnok.TabIndex = 2;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // BrowserGrid
            // 
            this.BrowserGrid.AllowUserToAddRows = false;
            this.BrowserGrid.AllowUserToDeleteRows = false;
            this.BrowserGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.BrowserGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BrowserGrid.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.BrowserGrid.Location = new System.Drawing.Point(12, 50);
            this.BrowserGrid.Name = "BrowserGrid";
            this.BrowserGrid.ReadOnly = true;
            this.BrowserGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BrowserGrid.Size = new System.Drawing.Size(846, 420);
            this.BrowserGrid.TabIndex = 3;
            this.BrowserGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BrowserGrid_CellDoubleClick);
            this.BrowserGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BrowserGrid_ColumnHeaderMouseClick);
            this.BrowserGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BrowserGrid_CellClick);
            this.BrowserGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BrowserGrid_KeyDown);
            this.BrowserGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BrowserGrid_KeyPress);
            this.BrowserGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BrowserGrid_CellContentClick);
            // 
            // lblSearchString
            // 
            this.lblSearchString.AutoSize = true;
            this.lblSearchString.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchString.Location = new System.Drawing.Point(767, 4);
            this.lblSearchString.Name = "lblSearchString";
            this.lblSearchString.Size = new System.Drawing.Size(71, 13);
            this.lblSearchString.TabIndex = 4;
            this.lblSearchString.Text = "Search String";
            this.lblSearchString.Visible = false;
            // 
            // frmBrowserCOA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 482);
            this.Controls.Add(this.lblSearchString);
            this.Controls.Add(this.BrowserGrid);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBrowserCOA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.frmBrowserCOA_Load);
            this.Activated += new System.EventHandler(this.frmBrowser_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.BrowserGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.DataGridView BrowserGrid;
        private System.Windows.Forms.Label lblSearchString;
    }
}