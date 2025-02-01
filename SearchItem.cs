namespace ServiceSearcher
{
    internal class SearchItem
    {
        public string Name {  get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public ulong Size { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Hits { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
