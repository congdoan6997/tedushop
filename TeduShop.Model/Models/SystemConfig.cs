using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("Systemconfigs")]
    public class SystemConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Column(TypeName ="varchar")]
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string ValueString { get; set; }
        public int? ValueInt { get; set; }
    }
}