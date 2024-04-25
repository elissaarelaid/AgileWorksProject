using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("application")]
    public class Application
    {
        /// <summary>
        /// Id of the application, automatically added by the entity framework
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Description of the application. Requierd filed which length cannot be longer then 500 characters
        /// </summary>
        [Column("description")]
        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Date and time when application added to the database
        /// </summary>
        [Column("entry_date")]
        public DateTime EntryDate  { get; set; }

        /// <summary>
        /// Application's solving deadline
        /// </summary>
        [Column("resolution_date")]
        public DateTime ResolutionDate { get; set; } 

        /// <summary>
        /// Status of the application. True if application is solved and false if it is not
        /// </summary>
        [Column("is_solved")]
        public bool IsSolved { get; set; } = false;
    }
}