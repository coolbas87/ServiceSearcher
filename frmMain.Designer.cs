namespace ServiceSearcher
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            bindingSource1 = new BindingSource(components);
            splitContainer1 = new SplitContainer();
            label2 = new Label();
            label1 = new Label();
            bFind = new Button();
            tbQuery = new TextBox();
            txtFileTypes = new TextBox();
            splitContainer2 = new SplitContainer();
            dgvSearchResults = new DataGridView();
            clnName = new DataGridViewLinkColumn();
            clnPath = new DataGridViewLinkColumn();
            clnType = new DataGridViewTextBoxColumn();
            clnSize = new DataGridViewTextBoxColumn();
            clnHits = new DataGridViewTextBoxColumn();
            clnDateCreated = new DataGridViewTextBoxColumn();
            clnDateModified = new DataGridViewTextBoxColumn();
            clnUrl = new DataGridViewTextBoxColumn();
            clnContent = new DataGridViewTextBoxColumn();
            cmsMain = new ContextMenuStrip(components);
            tsmiCopyFileName = new ToolStripMenuItem();
            tsmiCopyFilePath = new ToolStripMenuItem();
            tsmiCopyFullFilePath = new ToolStripMenuItem();
            tsmiCopyRowsToClipboardCommand = new ToolStripMenuItem();
            rtbContent = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSearchResults).BeginInit();
            cmsMain.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(6, 6);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(bFind);
            splitContainer1.Panel1.Controls.Add(tbQuery);
            splitContainer1.Panel1.Controls.Add(txtFileTypes);
            splitContainer1.Panel1.Padding = new Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(472, 349);
            splitContainer1.SplitterDistance = 95;
            splitContainer1.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 47);
            label2.Name = "label2";
            label2.Size = new Size(121, 15);
            label2.TabIndex = 11;
            label2.Text = "Запит вмісту у файлі";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(168, 15);
            label1.TabIndex = 10;
            label1.Text = "Ім’я файла, або його частина";
            // 
            // bFind
            // 
            bFind.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bFind.Location = new Point(365, 21);
            bFind.Name = "bFind";
            bFind.Size = new Size(101, 23);
            bFind.TabIndex = 9;
            bFind.Text = "Пошук";
            bFind.UseVisualStyleBackColor = true;
            bFind.Click += button2_Click;
            // 
            // tbQuery
            // 
            tbQuery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbQuery.Location = new Point(6, 65);
            tbQuery.Name = "tbQuery";
            tbQuery.Size = new Size(353, 23);
            tbQuery.TabIndex = 8;
            tbQuery.Text = "\"таємно\" OR \"цілком таємно\" OR \"для службового користування\"";
            // 
            // txtFileTypes
            // 
            txtFileTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFileTypes.Location = new Point(6, 21);
            txtFileTypes.Name = "txtFileTypes";
            txtFileTypes.PlaceholderText = "*.docx,*.doc";
            txtFileTypes.Size = new Size(353, 23);
            txtFileTypes.TabIndex = 7;
            txtFileTypes.Text = "*.docx,*.doc,*.docm,*.xls,*.xlsx,*.xlsm,*.ppt,*.pptx, *.pdf, *дск.*";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(dgvSearchResults);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(rtbContent);
            splitContainer2.Size = new Size(472, 250);
            splitContainer2.SplitterDistance = 300;
            splitContainer2.TabIndex = 0;
            // 
            // dgvSearchResults
            // 
            dgvSearchResults.AllowUserToAddRows = false;
            dgvSearchResults.AllowUserToDeleteRows = false;
            dgvSearchResults.AllowUserToOrderColumns = true;
            dgvSearchResults.AllowUserToResizeRows = false;
            dgvSearchResults.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSearchResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSearchResults.Columns.AddRange(new DataGridViewColumn[] { clnName, clnPath, clnType, clnSize, clnHits, clnDateCreated, clnDateModified, clnUrl, clnContent });
            dgvSearchResults.ContextMenuStrip = cmsMain;
            dgvSearchResults.Dock = DockStyle.Fill;
            dgvSearchResults.Location = new Point(0, 0);
            dgvSearchResults.Name = "dgvSearchResults";
            dgvSearchResults.ReadOnly = true;
            dgvSearchResults.Size = new Size(300, 250);
            dgvSearchResults.TabIndex = 15;
            dgvSearchResults.CellContentDoubleClick += dgvSearchResults_CellContentDoubleClick;
            // 
            // clnName
            // 
            clnName.DataPropertyName = "Name";
            clnName.HeaderText = "Ім'я файла";
            clnName.Name = "clnName";
            clnName.ReadOnly = true;
            clnName.Resizable = DataGridViewTriState.True;
            clnName.SortMode = DataGridViewColumnSortMode.Automatic;
            clnName.Width = 150;
            // 
            // clnPath
            // 
            clnPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clnPath.DataPropertyName = "Path";
            clnPath.HeaderText = "Шлях";
            clnPath.MinimumWidth = 150;
            clnPath.Name = "clnPath";
            clnPath.ReadOnly = true;
            clnPath.Resizable = DataGridViewTriState.True;
            clnPath.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // clnType
            // 
            clnType.DataPropertyName = "Type";
            clnType.HeaderText = "Тип";
            clnType.Name = "clnType";
            clnType.ReadOnly = true;
            clnType.Width = 125;
            // 
            // clnSize
            // 
            clnSize.DataPropertyName = "Size";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            clnSize.DefaultCellStyle = dataGridViewCellStyle2;
            clnSize.HeaderText = "Розмір, б";
            clnSize.Name = "clnSize";
            clnSize.ReadOnly = true;
            clnSize.Width = 80;
            // 
            // clnHits
            // 
            clnHits.DataPropertyName = "Hits";
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            clnHits.DefaultCellStyle = dataGridViewCellStyle3;
            clnHits.HeaderText = "Кількість знайдено";
            clnHits.Name = "clnHits";
            clnHits.ReadOnly = true;
            clnHits.Width = 80;
            // 
            // clnDateCreated
            // 
            clnDateCreated.DataPropertyName = "DateCreated";
            dataGridViewCellStyle4.Format = "G";
            dataGridViewCellStyle4.NullValue = null;
            clnDateCreated.DefaultCellStyle = dataGridViewCellStyle4;
            clnDateCreated.HeaderText = "Дата створення";
            clnDateCreated.Name = "clnDateCreated";
            clnDateCreated.ReadOnly = true;
            clnDateCreated.Width = 110;
            // 
            // clnDateModified
            // 
            clnDateModified.DataPropertyName = "DateModified";
            dataGridViewCellStyle5.Format = "G";
            dataGridViewCellStyle5.NullValue = null;
            clnDateModified.DefaultCellStyle = dataGridViewCellStyle5;
            clnDateModified.HeaderText = "Дата редагування";
            clnDateModified.Name = "clnDateModified";
            clnDateModified.ReadOnly = true;
            clnDateModified.Width = 110;
            // 
            // clnUrl
            // 
            clnUrl.DataPropertyName = "Url";
            clnUrl.HeaderText = "URL";
            clnUrl.Name = "clnUrl";
            clnUrl.ReadOnly = true;
            clnUrl.Visible = false;
            // 
            // clnContent
            // 
            clnContent.DataPropertyName = "Content";
            clnContent.HeaderText = "Вміст";
            clnContent.Name = "clnContent";
            clnContent.ReadOnly = true;
            clnContent.Visible = false;
            // 
            // cmsMain
            // 
            cmsMain.Items.AddRange(new ToolStripItem[] { tsmiCopyFileName, tsmiCopyFilePath, tsmiCopyFullFilePath, tsmiCopyRowsToClipboardCommand });
            cmsMain.Name = "cmsMain";
            cmsMain.Size = new Size(376, 92);
            cmsMain.Opening += cmsMain_Opening;
            // 
            // tsmiCopyFileName
            // 
            tsmiCopyFileName.Name = "tsmiCopyFileName";
            tsmiCopyFileName.ShortcutKeys = Keys.Control | Keys.D1;
            tsmiCopyFileName.Size = new Size(375, 22);
            tsmiCopyFileName.Text = "Копіювати назву файла";
            // 
            // tsmiCopyFilePath
            // 
            tsmiCopyFilePath.Name = "tsmiCopyFilePath";
            tsmiCopyFilePath.ShortcutKeyDisplayString = "";
            tsmiCopyFilePath.ShortcutKeys = Keys.Control | Keys.D2;
            tsmiCopyFilePath.Size = new Size(375, 22);
            tsmiCopyFilePath.Text = "Копіювати шлях";
            // 
            // tsmiCopyFullFilePath
            // 
            tsmiCopyFullFilePath.Name = "tsmiCopyFullFilePath";
            tsmiCopyFullFilePath.ShortcutKeys = Keys.Control | Keys.D3;
            tsmiCopyFullFilePath.Size = new Size(375, 22);
            tsmiCopyFullFilePath.Text = "Копіювати повний шлях файла";
            // 
            // tsmiCopyRowsToClipboardCommand
            // 
            tsmiCopyRowsToClipboardCommand.Name = "tsmiCopyRowsToClipboardCommand";
            tsmiCopyRowsToClipboardCommand.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;
            tsmiCopyRowsToClipboardCommand.Size = new Size(375, 22);
            tsmiCopyRowsToClipboardCommand.Text = "Експорт виділених рядків в буфер обміну";
            // 
            // rtbContent
            // 
            rtbContent.Dock = DockStyle.Fill;
            rtbContent.Location = new Point(0, 0);
            rtbContent.Name = "rtbContent";
            rtbContent.ReadOnly = true;
            rtbContent.ShowSelectionMargin = true;
            rtbContent.Size = new Size(168, 250);
            rtbContent.TabIndex = 15;
            rtbContent.Text = "";
            rtbContent.SelectionChanged += rtbContent_SelectionChanged;
            rtbContent.TextChanged += rtbContent_TextChanged;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 361);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(400, 350);
            Name = "frmMain";
            Padding = new Padding(6);
            Text = "ASearch";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSearchResults).EndInit();
            cmsMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private BindingSource bindingSource1;
        private SplitContainer splitContainer1;
        private Button bFind;
        private TextBox tbQuery;
        private TextBox txtFileTypes;
        private Label label2;
        private Label label1;
        private SplitContainer splitContainer2;
        private DataGridView dgvSearchResults;
        private RichTextBox rtbContent;
        private ContextMenuStrip cmsMain;
        private ToolStripMenuItem tsmiCopyFileName;
        private ToolStripMenuItem tsmiCopyFilePath;
        private ToolStripMenuItem tsmiCopyFullFilePath;
        private ToolStripMenuItem tsmiCopyRowsToClipboardCommand;
        private DataGridViewLinkColumn clnName;
        private DataGridViewLinkColumn clnPath;
        private DataGridViewTextBoxColumn clnType;
        private DataGridViewTextBoxColumn clnSize;
        private DataGridViewTextBoxColumn clnHits;
        private DataGridViewTextBoxColumn clnDateCreated;
        private DataGridViewTextBoxColumn clnDateModified;
        private DataGridViewTextBoxColumn clnUrl;
        private DataGridViewTextBoxColumn clnContent;
    }
}
