using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("application")]
    public class Application
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        public required string Description { get; set; } = "";
        [Column("entry_date")]
        public DateTime EntryDate  { get; set; }
        [Column("resolution_date")]
        public DateTime? ResolutionDate { get; set; } 
        [Column("is_solved")]
        public bool IsSolved { get; set; } = false;
    }
}