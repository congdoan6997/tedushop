namespace TeduShop.Web.Models
{
    public class ProductTagViewModel
    {
        public string TagID { get; set; }

        public int ProductID { get; set; }

        public virtual TagViewModel Tag { get; set; }

        public virtual ProductViewModel Product { get; set; }
    }
}