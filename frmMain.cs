using Microsoft.Search.Interop;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ServiceSearcher
{
    public partial class frmMain : Form
    {
        MatchCollection searchItems;
        CommandManager commandManager = new CommandManager();

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
            searchItems = Regex.Matches(tbQuery.Text, "\"(.*?)\"");
            SearchItemsDAO searchItemsDAO = new SearchItemsDAO(txtFileTypes.Text, tbQuery.Text, -1);
            bindingSource1.DataSource = searchItemsDAO.GetAllSearchItems();
            dgvSearchResults.DataSource = bindingSource1;
            rtbContent.DataBindings.Clear();
            rtbContent.DataBindings.Add(new Binding("Text", bindingSource1, "Content"));
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

                    foreach (Match searchItem in searchItems)
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
