using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Logic
{
    public interface IShopManage
    {
        void AddNewProductToShop(Product product);
        void RemoveProductInShop(string id);
        Product GetProductByID(string id);
        IEnumerable<Product> GetAllProductInShop();
        void AddProductToCart(string id, int amount);
        void RemoveProductInCart(string id);
        Cart GetCurrentCart();
        void CleanCart();

    }
}
