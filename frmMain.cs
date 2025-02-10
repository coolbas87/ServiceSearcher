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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            searchQueryItems = Regex.Matches(tbQuery.Text, "\"(.*?)\"");
            SearchItemsDAO searchItemsDAO = new SearchItemsDAO(txtFileTypes.Text, tbQuery.Text, -1);
            searchItems.Clear();
            searchItems.AddRange(searchItemsDAO.GetAllSearchItems());
            bindingSource1.DataSource = searchItems;
            dgvSearchResults.DataSource = bindingSource1;
            rtbContent.DataBindings.Clear();
            rtbContent.DataBindings.Add(new Binding("Text", bindingSource1, "Content"));
            tsslTotalFound.Text = $"Всього знайдено файлів: {searchItems.Count}";
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
                $"{dataGridView.Columns["clnDateModified"].HeaderText}\t{dataGridView.Columns["clnContent"].HeaderText}\t");

            foreach (DataGridViewRow row in rows) 
            {
                SearchItem searchItem = ((SearchItem)row.DataBoundItem);
                stringBuilder.AppendLine($"{searchItem.Name}\t{searchItem.Path}\t{searchItem.Type}\t{searchItem.Size}\t" +
                    $"{searchItem.DateCreated}\t{searchItem.DateModified}\t{searchItem.Content}");
            }

            if (stringBuilder.Length > 0)
            {
                Clipboard.SetText(stringBuilder.ToString());
            }
        }
    }
}
