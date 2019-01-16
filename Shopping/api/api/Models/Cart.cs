using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Cart
    {
        public IList<CartProduct> Product { get; set; }
        public double Discount { get => Product.Sum(it => it.Discount); }
        public double TotalBeforeDiscount { get => Product.Sum(it => it.TotalBeforeDiscount - it.Discount); }
        public double Total { get => TotalBeforeDiscount - Discount; }
    }
}
