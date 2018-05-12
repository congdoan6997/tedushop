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
        [Column(TypeName = "varchar",Order =1)]
        [MaxLength(50)]
        [Key]
        public string TagID { get; set; }
        [Key]
        [Column(Order =2)]
        public int ProductID { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

    }
}
