using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.BaseModule
{
    [Table("Log")]
    public class Log : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string LogType { get; set; }
        [Required]
        public string LogContent { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
