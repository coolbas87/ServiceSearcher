namespace ServiceSearcher
{
    internal class SearchItemsList : List<SearchItem>
    {
        private string sortFieldName = string.Empty;
        private bool isAscendingSort;

        public void Sort(string fieldName, bool isAscending = true)
        {
            sortFieldName = fieldName;
            isAscendingSort = isAscending;

            if (isAscendingSort)
            {
                Sort((x, y) => GetPropertyValue(x, sortFieldName)?.CompareTo(GetPropertyValue(y, sortFieldName)) ?? 0);
            }
            else
            {
                Sort((y, x) => GetPropertyValue(x, sortFieldName)?.CompareTo(GetPropertyValue(y, sortFieldName)) ?? 0);
            }

            static IComparable? GetPropertyValue(SearchItem item, string propertyName)
            {
                return (IComparable?)item.GetType().GetProperty(propertyName)?.GetValue(item, null);
            }
        }
	}
}
