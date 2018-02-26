namespace ECCalcAidDataBuilder
{
    partial class frm_DataBuilder
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_New = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Compile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOnLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_Data = new System.Windows.Forms.DataGridView();
            this.trv_DataTree = new System.Windows.Forms.TreeView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_LoadCodes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(742, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_New,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnu_Compile,
            this.mnu_Import,
            this.mnu_LoadCodes,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem,
            this.loadOnLineToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.toolStripMenuItem1.Text = "DataBase";
            // 
            // mnu_New
            // 
            this.mnu_New.Name = "mnu_New";
            this.mnu_New.Size = new System.Drawing.Size(153, 22);
            this.mnu_New.Text = "New";
            this.mnu_New.Click += new System.EventHandler(this.mnu_New_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // mnu_Compile
            // 
            this.mnu_Compile.Name = "mnu_Compile";
            this.mnu_Compile.Size = new System.Drawing.Size(153, 22);
            this.mnu_Compile.Text = "Compile";
            this.mnu_Compile.Click += new System.EventHandler(this.mnu_Compile_Click);
            // 
            // mnu_Import
            // 
            this.mnu_Import.Name = "mnu_Import";
            this.mnu_Import.Size = new System.Drawing.Size(153, 22);
            this.mnu_Import.Text = "Import";
            this.mnu_Import.Click += new System.EventHandler(this.mnu_Import_Click);
            // 
            // loadOnLineToolStripMenuItem
            // 
            this.loadOnLineToolStripMenuItem.Name = "loadOnLineToolStripMenuItem";
            this.loadOnLineToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.loadOnLineToolStripMenuItem.Text = "Sync Online";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // dgv_Data
            // 
            this.dgv_Data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Data.Location = new System.Drawing.Point(196, 27);
            this.dgv_Data.Name = "dgv_Data";
            this.dgv_Data.Size = new System.Drawing.Size(546, 393);
            this.dgv_Data.TabIndex = 1;
            // 
            // trv_DataTree
            // 
            this.trv_DataTree.Location = new System.Drawing.Point(0, 27);
            this.trv_DataTree.Name = "trv_DataTree";
            this.trv_DataTree.Size = new System.Drawing.Size(190, 393);
            this.trv_DataTree.TabIndex = 3;
            this.trv_DataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_DataTree_AfterSelect);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // mnu_LoadCodes
            // 
            this.mnu_LoadCodes.Name = "mnu_LoadCodes";
            this.mnu_LoadCodes.Size = new System.Drawing.Size(153, 22);
            this.mnu_LoadCodes.Text = "LoadCodeTables";
            this.mnu_LoadCodes.Click += new System.EventHandler(this.mnu_LoadCodes_Click);
            // 
            // frm_DataBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 423);
            this.Controls.Add(this.trv_DataTree);
            this.Controls.Add(this.dgv_Data);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1000, 650);
            this.MinimumSize = new System.Drawing.Size(750, 450);
            this.Name = "frm_DataBuilder";
            this.Text = "EuroCode Calc Aid - Data Builder";
            this.Load += new System.EventHandler(this.frm_DataBuilder_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnu_New;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_Compile;
        private System.Windows.Forms.ToolStripMenuItem mnu_Import;
        private System.Windows.Forms.ToolStripMenuItem loadOnLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_Data;
        private System.Windows.Forms.TreeView trv_DataTree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnu_LoadCodes;
    }
}

