using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceSearcher
{
    public partial class frmMain : Form
    {
        MatchCollection searchQueryItems;
        CommandManager commandManager = new CommandManager();
        SearchItemsList searchItems = new SearchItemsList();

        public frmMain()
        {
            InitializeComponent();

            DataGridCommand command = new CopyFileNameCommand(dgvSearchResults);
            tsmiCopyFileName.Command = command;
            commandManager.AddCommand(command);
            command = new CopyFilePathCommand(dgvSearchResults);
            tsmiCopyFilePath.Command = command;
            commandManager.AddCommand(command);
            command = new CopyFullFilePathCommand(dgvSearchResults);
            tsmiCopyFullFilePath.Command = command;
            commandManager.AddCommand(command);
            command = new CopyRowsToClipboardCommand(dgvSearchResults);
            tsmiCopyRowsToClipboardCommand.Command = command;
            commandManager.AddCommand(command);
            command = new CopyCheckedRowsToClipboardCommand(dgvSearchResults, searchItems);
            tsmiCopyCheckedRowsToClipboard.Command = command;
            commandManager.AddCommand(command);
        }

        private void bFind_Click(object sender, EventArgs e)
        {
            try
            {
                bFind.Enabled = false;
                searchQueryItems = Regex.Matches(tbQuery.Text, "\"(.*?)\"");
                SearchItemsDAO searchItemsDAO = new SearchItemsDAO(txtFileTypes.Text, tbQuery.Text, -1);
                searchItems.Clear();
                searchItems.AddRange(searchItemsDAO.GetAllSearchItems());
                bindingSource1.DataSource = searchItems;
                dgvSearchResults.DataSource = bindingSource1;
                bindingSource1.ResetBindings(false);
                tbSearch_TextChanged(tbSearch, e);
                rtbContent.DataBindings.Clear();
                rtbContent.DataBindings.Add(new Binding("Text", bindingSource1, "Content"));
            }
            finally
            {
                bFind.Enabled = true;
            }
        }

        private void dgvSearchResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SearchItem? searchItem = dgvSearchResults.SelectedCells.Count > 0 ?
                dgvSearchResults.SelectedCells[0].OwningRow.DataBoundItem as SearchItem : null;

            if (searchItem != null)
            {
                Uri uri = new Uri(searchItem.Url);
                string filename = uri.LocalPath;

                if (File.Exists(filename))
                {
                    if (dgvSearchResults.Columns[e.ColumnIndex].Name == clnName.Name)
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = filename;
                        psi.UseShellExecute = true;
                        Process.Start(psi);
                    }
                    else if (dgvSearchResults.Columns[e.ColumnIndex].Name == clnPath.Name)
                    {
                        Process.Start("explorer", $"/select, \"{filename}\"");
                    }
                }
            }
        }

        private void rtbContent_TextChanged(object sender, EventArgs e)
        {
            if (rtbContent.Text.Length > 0)
            {
                try
                {
                    rtbContent.SuspendLayout();

                    foreach (Match searchItem in searchQueryItems)
                    {
                        MatchCollection mColl = Regex.Matches(rtbContent.Text, searchItem.Value.Replace("\"", ""),
                            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                        foreach (Match g in mColl)
                        {
                            rtbContent.Select(g.Index, g.Length);
                        }
                    }
                }
                finally
                {
                    rtbContent.ResumeLayout();
                }
            }
        }

        private void rtbContent_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbContent.Text.Length > 0)
            {
                try
                {
                    rtbContent.SuspendLayout();
                    rtbContent.SelectionColor = Color.Black;
                    rtbContent.SelectionBackColor = Color.Yellow;
                }
                finally
                {
                    rtbContent.ResumeLayout();
                }
            }
        }

        private void cmsMain_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            commandManager.RaiseCanExecuteChanged();
        }

        private void dgvSearchResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = dgvSearchResults.Columns[e.ColumnIndex];
            SortOrder sortOrder = column.HeaderCell.SortGlyphDirection;

            if (sortOrder == SortOrder.None || sortOrder == SortOrder.Descending)
            {
                sortOrder = SortOrder.Ascending;
                searchItems.Sort(column.DataPropertyName);
            }
            else
            {
                sortOrder = SortOrder.Descending;
                searchItems.Sort(column.DataPropertyName, false);
            }

            dgvSearchResults.Refresh();

            foreach (DataGridViewColumn col in dgvSearchResults.Columns)
            {
                if (col != column)
                {
                    col.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }

            column.HeaderCell.SortGlyphDirection = sortOrder;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            var text = ((TextBox)sender).Text;

            bindingSource1.SuspendBinding();
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    bindingSource1.DataSource = searchItems;
                }
                else
                {
                    bindingSource1.DataSource = searchItems.Where(x => x.Url.Contains(text)).ToList();
                }
            }
            finally
            {
                bindingSource1.ResumeBinding();
            }
        }

        private void dgvSearchResults_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (tcContent.SelectedTab == tpPreview)
            {
                tPreview.Start();
            }
        }

        private void LoadPreview()
        {
            SearchItem? searchItem = dgvSearchResults.SelectedCells.Count > 0 ?
                dgvSearchResults.SelectedCells[0].OwningRow.DataBoundItem as SearchItem : null;
            string filePath = searchItem != null ? Path.Combine(searchItem.Path, searchItem.Name) : string.Empty;

            if (File.Exists(filePath))
            {
                previewHandlerHost1.Open(filePath);
            }
        }

        private void tPreview_Tick(object sender, EventArgs e)
        {
            LoadPreview();
            tPreview.Stop();
        }

        private void tcContent_Selected(object sender, TabControlEventArgs e)
        {
            if (tcContent.SelectedTab == tpPreview)
            {
                LoadPreview();
            }
        }

        private void bindingSource1_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            tsslTotalFound.Text = $"Всього знайдено файлів: {bindingSource1.Count}";
        }
    }

    internal abstract class DataGridCommand : ManagedCommand
    {
        protected DataGridView dataGridView;

        public DataGridCommand(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        public override bool CanExecute(object? parameter)
        {
            SearchItem? searchItem = dataGridView.SelectedCells.Count > 0 ?
                dataGridView.SelectedCells[0].OwningRow.DataBoundItem as SearchItem : null;

            return searchItem != null;
        }
    }

    internal class CopyFileNameCommand : DataGridCommand
    {
        public CopyFileNameCommand(DataGridView dataGridView) : base(dataGridView) { }

        public override void Execute(object? parameter)
        {
            SearchItem? searchItem = dataGridView.SelectedCells.Count > 0 ?
                dataGridView.SelectedCells[0].OwningRow.DataBoundItem as SearchItem : null;

            if (searchItem != null)
            {
                Clipboard.SetText(searchItem.Name);
            }
        }
    }

    internal class CopyFilePathCommand : DataGridCommand
    {
        public CopyFilePathCommand(DataGridView dataGridView) : base(dataGridView) { }

        public override void Execute(object? parameter)
        {
            SearchItem? searchItem = dataGridView.SelectedCells.Count > 0 ?
                dataGridView.SelectedCells[0].OwningRow.DataBoundItem as SearchItem : null;

            if (searchItem != null)
            {
                Clipboard.SetText(searchItem.Path);
            }
        }
    }

    internal class CopyFullFilePathCommand : DataGridCommand
    {
        public CopyFullFilePathCommand(DataGridView dataGridView) : base(dataGridView) { }

        public override void Execute(object? parameter)
        {
            SearchItem? searchItem = dataGridView.SelectedCells.Count > 0 ?
                dataGridView.SelectedCells[0].OwningRow.DataBoundItem as SearchItem : null;

            if (searchItem != null)
            {
                Clipboard.SetText(Path.Combine(searchItem.Path, searchItem.Name));
            }
        }
    }

    internal class CopyRowsToClipboardCommand : DataGridCommand
    {
        public CopyRowsToClipboardCommand(DataGridView dataGridView) : base(dataGridView) { }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) && dataGridView.SelectedRows.Count > 0;
        }

        public override void Execute(object? parameter)
        {
            DataGridViewSelectedRowCollection rows = dataGridView.SelectedRows;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{dataGridView.Columns["clnName"].HeaderText}\t{dataGridView.Columns["clnPath"].HeaderText}\t" +
                $"{dataGridView.Columns["clnType"].HeaderText}\t{dataGridView.Columns["clnSize"].HeaderText}\t{dataGridView.Columns["clnDateCreated"].HeaderText}\t" +
                $"{dataGridView.Columns["clnDateModified"].HeaderText}");

            foreach (DataGridViewRow row in rows) 
            {
                SearchItem searchItem = ((SearchItem)row.DataBoundItem);
                stringBuilder.AppendLine($"{searchItem.Name}\t{searchItem.Path}\t{searchItem.Type}\t{searchItem.Size}\t" +
                    $"{searchItem.DateCreated}\t{searchItem.DateModified}");
            }

            if (stringBuilder.Length > 0)
            {
                Clipboard.SetText(stringBuilder.ToString());
            }
        }
    }

    internal class CopyCheckedRowsToClipboardCommand : DataGridCommand
    {
        SearchItemsList searchItems = new SearchItemsList();

        public CopyCheckedRowsToClipboardCommand(DataGridView dataGridView, SearchItemsList searchItems) : base(dataGridView) 
        {
            this.searchItems = searchItems;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) && dataGridView.SelectedRows.Count > 0;
        }

        public override void Execute(object? parameter)
        {
            DataGridViewSelectedRowCollection rows = dataGridView.SelectedRows;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{dataGridView.Columns["clnName"].HeaderText}\t{dataGridView.Columns["clnPath"].HeaderText}\t" +
                $"{dataGridView.Columns["clnType"].HeaderText}\t{dataGridView.Columns["clnSize"].HeaderText}\t{dataGridView.Columns["clnDateCreated"].HeaderText}\t" +
                $"{dataGridView.Columns["clnDateModified"].HeaderText}");

            foreach (SearchItem searchItem in searchItems.Where(x => x.IsChecked))
            {
                stringBuilder.AppendLine($"{searchItem.Name}\t{searchItem.Path}\t{searchItem.Type}\t{searchItem.Size}\t" +
                    $"{searchItem.DateCreated}\t{searchItem.DateModified}");
            }

            if (stringBuilder.Length > 0)
            {
                Clipboard.SetText(stringBuilder.ToString());
            }
        }
    }
}
