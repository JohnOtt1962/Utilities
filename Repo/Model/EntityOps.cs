namespace Utilities.Repo.Model
{
    public class EntityOps
    {
        public required string ConnectionString { get; set; }
        public required string CommandText { get; set; }
        public bool IsStoredProc { get; set; }
        public List<ParamItem> Params { get; set; } = [];
    }

    public class ParamItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsOutput { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
    }
}