using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> LastesProducts { get; set; }

        public IEnumerable<ProductViewModel> HotProducts { get; set; }

        public int IndexLastest { get; set; } = 1;
        public int IndexHot { get; set; } = 1;
    }
}