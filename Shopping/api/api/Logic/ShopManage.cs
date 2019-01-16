using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Logic
{
    public class ShopManage : IShopManage
    {
        private Cart CurrentCart;
        private IList<Product> Products;
        public ShopManage() {
            CurrentCart = new Cart { Product = new List<CartProduct>() };
            Products = new List<Product>();
        }
        public void AddNewProductToShop(Product product)
        {
            if (Products.Any(it => it.Name == product.Name))
                return;

            product.ID = Guid.NewGuid().ToString();
            Products.Add(product);
        }

        public void AddProductToCart(string id, int amount)
        {
            var product = GetProductByID(id);
            if(CurrentCart.Product.Any(it => it.ID == product.ID))
            {
                CurrentCart.Product.First(it => it.ID == product.ID).Amount += amount;
                return;
            }
            var cartProduct = new CartProduct
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Amount = amount
            };
            CurrentCart.Product.Add(cartProduct);
        }

        public void CleanCart()
        {
            CurrentCart.Product = new List<CartProduct>();
        }

        public IEnumerable<Product> GetAllProductInShop()
        {
            return Products;
        }

        public Cart GetCurrentCart()
        {
            return CurrentCart;
        }

        public Product GetProductByID(string id)
        {
            return Products.FirstOrDefault(it => it.ID == id);
        }

        public void RemoveProductInCart(string id)
        {
            var removeProduct = CurrentCart.Product.FirstOrDefault(it => it.ID == id);
            if (removeProduct == null)
                return;


            CurrentCart.Product.Remove(removeProduct);
        }

        public void RemoveProductInShop(string id)
        {
            var removeProduct = Products.FirstOrDefault(it => it.ID == id);
            if (removeProduct == null)
                return;

            Products.Remove(removeProduct);
            RemoveProductInCart(id);
        }
    }
}
