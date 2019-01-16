using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Logic;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopManageController : ControllerBase
    {
        public static IShopManage svc = new ShopManage();
        // GET: api/ShopManage
        [HttpGet(Name = "Get Product in Shop")]
        public IEnumerable<Product> GetAllProduct()
        {
            return svc.GetAllProductInShop();
        }

        // GET: api/ShopManage/add/iphone/49999
        [HttpGet("add/{name}/{price}", Name = "Add Product to Shop")]
        public void AddNewProduct(string name, double price)
        {
            var product = new Product
            {
                Name = name,
                Price = price
            };
            svc.AddNewProductToShop(product);
        }

        // GET: api/ShopManage/remove/53TQ8
        [HttpGet("remove/{id}", Name = "Remove Product in Shop")]
        public void RemoveProduct(string id)
        {
            svc.RemoveProductInShop(id);
        }

        // GET: api/ShopManage/product/48YV7
        [HttpGet("product/{id}", Name = "Get Product by Id")]
        public Product GetProduct(string id)
        {
            return svc.GetProductByID(id);
        }

        // GET: api/ShopManage/cart
        [HttpGet("cart", Name = "Get Cart")]
        public Cart GetCurrentCart()
        {
            return svc.GetCurrentCart();
        }

        // GET: api/ShopManage/cart/add/48YV7/2
        [HttpGet("cart/add/{id}/{amount}", Name = "Add Product to Cart")]
        public void AddProductToCart(string id, int amount)
        {
            svc.AddProductToCart(id, amount);
        }

        // GET: api/ShopManage/cart/remove/48YV7
        [HttpGet("cart/remove/{id}", Name = "Remove Product to Cart")]
        public void RemoveProductToCart(string id)
        {
            svc.RemoveProductInCart(id);
        }

        // GET: api/ShopManage/cart/clean
        [HttpGet("cart/clean", Name = "Clear Cart")]
        public void CleanCurrentCart()
        {
            svc.CleanCart();
        }
    }
}
