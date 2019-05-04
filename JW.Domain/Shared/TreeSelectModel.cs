namespace JW.Domain.Shared
{
    public class TreeSelectModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public int parentId { get; set; }
        public object data { get; set; }
        public bool disabled { get; set; }
    }
}