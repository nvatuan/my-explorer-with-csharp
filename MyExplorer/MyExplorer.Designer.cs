using System.Windows.Forms;

namespace MyExplorer
{
    partial class MyExplorer
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("SAMPLE");
            this.TopBodyPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.WorkingDir = new System.Windows.Forms.TextBox();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.DirTree = new System.Windows.Forms.TreeView();
            this.DirBrowser = new System.Windows.Forms.ListView();
            this.ListViewItemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToParentFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopBodyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.ListViewItemContextMenu.SuspendLayout();
            this.ListViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopBodyPanel
            // 
            this.TopBodyPanel.ColumnCount = 2;
            this.TopBodyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.TopBodyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopBodyPanel.Controls.Add(this.SearchBox, 1, 0);
            this.TopBodyPanel.Controls.Add(this.WorkingDir, 0, 0);
            this.TopBodyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopBodyPanel.Location = new System.Drawing.Point(0, 0);
            this.TopBodyPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TopBodyPanel.Name = "TopBodyPanel";
            this.TopBodyPanel.RowCount = 1;
            this.TopBodyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopBodyPanel.Size = new System.Drawing.Size(804, 27);
            this.TopBodyPanel.TabIndex = 0;
            // 
            // SearchBox
            // 
            this.SearchBox.AcceptsReturn = true;
            this.SearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBox.Location = new System.Drawing.Point(606, 2);
            this.SearchBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(195, 22);
            this.SearchBox.TabIndex = 1;
            this.SearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchBox_KeyPress);
            // 
            // WorkingDir
            // 
            this.WorkingDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkingDir.Location = new System.Drawing.Point(3, 2);
            this.WorkingDir.Margin = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.WorkingDir.Name = "WorkingDir";
            this.WorkingDir.Size = new System.Drawing.Size(600, 22);
            this.WorkingDir.TabIndex = 0;
            this.WorkingDir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorkingDir_KeyPress);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 27);
            this.SplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.DirTree);
            this.SplitContainer.Panel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.SplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.DirBrowser);
            this.SplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.SplitContainer.Size = new System.Drawing.Size(804, 435);
            this.SplitContainer.SplitterDistance = 268;
            this.SplitContainer.TabIndex = 1;
            // 
            // DirTree
            // 
            this.DirTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DirTree.Location = new System.Drawing.Point(3, 3);
            this.DirTree.Name = "DirTree";
            this.DirTree.Size = new System.Drawing.Size(265, 429);
            this.DirTree.TabIndex = 0;
            this.DirTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DirTree_BeforeExpand);
            this.DirTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DirTree_AfterSelect);
            // 
            // DirBrowser
            // 
            this.DirBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DirBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DirBrowser.GridLines = true;
            this.DirBrowser.HideSelection = false;
            this.DirBrowser.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.DirBrowser.Location = new System.Drawing.Point(3, 3);
            this.DirBrowser.Name = "DirBrowser";
            this.DirBrowser.Size = new System.Drawing.Size(526, 429);
            this.DirBrowser.TabIndex = 0;
            this.DirBrowser.UseCompatibleStateImageBehavior = false;
            this.DirBrowser.View = System.Windows.Forms.View.List;
            this.DirBrowser.ItemActivate += new System.EventHandler(this.DirBrowser_ItemActivate);
            this.DirBrowser.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.DirBrowser_ItemMouseHover);
            this.DirBrowser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DirBrowser_MouseUp);
            // 
            // ListViewItemContextMenu
            // 
            this.ListViewItemContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ListViewItemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.delToolStripMenuItem});
            this.ListViewItemContextMenu.Name = "ListViewItemContextMenu";
            this.ListViewItemContextMenu.Size = new System.Drawing.Size(115, 76);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.delToolStripMenuItem.Text = "Del";
            this.delToolStripMenuItem.Click += new System.EventHandler(this.delToolStripMenuItem_Click);
            // 
            // ListViewContextMenu
            // 
            this.ListViewContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToParentFolderToolStripMenuItem,
            this.newToolStripMenuItem});
            this.ListViewContextMenu.Name = "ListViewContextMenu";
            this.ListViewContextMenu.Size = new System.Drawing.Size(207, 52);
            // 
            // goToParentFolderToolStripMenuItem
            // 
            this.goToParentFolderToolStripMenuItem.Name = "goToParentFolderToolStripMenuItem";
            this.goToParentFolderToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.goToParentFolderToolStripMenuItem.Text = "Go to Parent Folder";
            this.goToParentFolderToolStripMenuItem.Click += new System.EventHandler(this.goToParentFolderToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.folderToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.newToolStripMenuItem.Text = "New";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.folderToolStripMenuItem.Text = "Folder";
            this.folderToolStripMenuItem.Click += new System.EventHandler(this.folderToolStripMenuItem_Click);
            // 
            // MyExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 462);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.TopBodyPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(819, 499);
            this.Name = "MyExplorer";
            this.Text = "MyExplorer";
            this.TopBodyPanel.ResumeLayout(false);
            this.TopBodyPanel.PerformLayout();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ListViewItemContextMenu.ResumeLayout(false);
            this.ListViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopBodyPanel;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.TextBox WorkingDir;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.TreeView DirTree;
        private System.Windows.Forms.ListView DirBrowser;
        private ContextMenuStrip ListViewItemContextMenu;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem delToolStripMenuItem;
        private ContextMenuStrip ListViewContextMenu;
        private ToolStripMenuItem goToParentFolderToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem folderToolStripMenuItem;
    }
}

