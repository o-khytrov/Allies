using SQLite;

namespace Allies
{
    [Table("Setting")]
    public class Setting
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}