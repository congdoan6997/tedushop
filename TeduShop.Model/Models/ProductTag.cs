using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("ProductTags")]
   public  class ProductTag
    {
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        [Key]
        public string TagID { get; set; }
        [Key]
        public int ProductID { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

    }
}
