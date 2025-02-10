using Microsoft.Search.Interop;
using System.Data.OleDb;
using System.Text;

namespace ServiceSearcher
{
    internal class SearchItemsDAO
    {
        private List<SearchItem> searchItems = new List<SearchItem>();
        private CSearchManager manager;
        private ISearchCatalogManager catalogManager;
        private ISearchQueryHelper queryHelper;
        private string userQuery = " ";

        public SearchItemsDAO() : this("*.*") { }

        public SearchItemsDAO(string filePatterns = "*.*", string userQuery = " ", int maxResultsCount = 10) 
        {
            this.userQuery = string.IsNullOrEmpty(userQuery) ? " " : userQuery;
            manager = new CSearchManager();
            catalogManager = manager.GetCatalog("SystemIndex");
            queryHelper = catalogManager.GetQueryHelper();

            queryHelper.QueryMaxResults = maxResultsCount;
            queryHelper.QuerySelectColumns = "System.ItemNameDisplay, System.ItemFolderPathDisplay, " +
                "System.ItemTypeText, System.Size, System.Search.AutoSummary, System.Search.HitCount, " +
                "System.DateCreated, System.DateModified, System.ItemUrl";
            queryHelper.QueryWhereRestrictions = "AND scope='file:'";
            queryHelper.QuerySorting = "System.DateModified DESC";
            

            if (filePatterns != "*")
            {
                var patterns = filePatterns.Split(',');
                StringBuilder patternQuery = new StringBuilder();

                foreach (var pattern in patterns)
                {
                    string patternValue = pattern.Trim().Replace("*", "%").Replace("?", "_");
                    patternQuery.Append(patternQuery.Length > 0 ? " OR " : string.Empty);

                    if (patternValue.Contains("%") || patternValue.Contains("_"))
                    {
                        patternQuery.Append($"System.FileName LIKE '{patternValue}'");
                    }
                    else
                    {
                        patternQuery.Append($"Contains(System.FileName, '{patternValue}')");
                    }
                }

                queryHelper.QueryWhereRestrictions += $" AND ({patternQuery}) ";
            }
        }

        public List<SearchItem> GetAllSearchItems()
        {
            using (OleDbConnection conn = new OleDbConnection(queryHelper.ConnectionString))
            {
                conn.Open();

                try
                {
                    var query = queryHelper.GenerateSQLFromUserQuery(userQuery);
                    
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchItems.Add(new SearchItem()
                                {
                                    Name = reader.GetString(0),
                                    Path = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Size = (ulong)reader.GetDecimal(3),
                                    Content = reader.GetString(4),
                                    Hits = reader.GetInt32(5),
                                    DateCreated = reader.GetDateTime(6),
                                    DateModified = reader.GetDateTime(7),
                                    Url = reader.GetString(8)
                                });
                            }
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            
            return searchItems;
        }
    }
}
