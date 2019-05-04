namespace JW.Domain.Shared
{
    public class TreeGridModel
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public string text { get; set; }
        public bool isLeaf { get; set; }
        public bool expanded { get; set; }
        public bool loaded { get; set; }
        public string entityJson { get; set; }
    }
}