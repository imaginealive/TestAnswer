using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class CartProduct : Product
    {
        public int Amount { get; set; }
        public double TotalBeforeDiscount { get => Amount * Price; }
        public double Discount { get => Amount / 4 * Price; }
    }
}
