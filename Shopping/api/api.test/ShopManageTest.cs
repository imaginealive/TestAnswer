using System;
using Xunit;
using FluentAssertions;
using api.Logic;
using api.Models;
using System.Collections.Generic;
using System.Linq;

namespace api.test
{
    public class ShopManageTest
    {
        private IShopManage svc;

        [Theory]
        [MemberData(nameof(ProductToShopCase))]
        public void AddNewProductToShopShouldWork(Product[] products)
        {
            svc = new ShopManage();
            foreach (var product in products)
            {
                svc.AddNewProductToShop(product);
            }
            var result = svc.GetAllProductInShop();
            foreach (var product in products)
            {
                result.Any(it => (it.Name == product.Name && it.Price == product.Price) || it.Name == product.Name).Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(ProductToShopCase))]
        public void RemoveProductInShopShouldWork(Product[] products)
        {
            svc = new ShopManage();
            foreach (var product in products)
            {
                svc.AddNewProductToShop(product);
            }
            var result = svc.GetAllProductInShop();
            var removeProduct = result.First();
            svc.RemoveProductInShop(removeProduct.ID);
            result = svc.GetAllProductInShop();
            result.Any(it => it == removeProduct).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(AddProductToCartCase))]
        public void AddProductToCartShouldWork(Product product, int amount, double totalPriceBeforeDiscount, double Discount)
        {
            svc = new ShopManage();
            svc.AddNewProductToShop(product);
            var shopProduct = svc.GetAllProductInShop();
            var productPicked = shopProduct.First();

            svc.CleanCart();
            var cart = svc.GetCurrentCart();
            cart = svc.GetCurrentCart();
            cart.Product.Count().Should().Be(0);

            svc.AddProductToCart(productPicked.ID, amount);
            cart = svc.GetCurrentCart();
            var result = cart.Product.FirstOrDefault(it => it.ID == productPicked.ID);
            result.Amount.Should().Be(amount);
            result.TotalBeforeDiscount.Should().Be(totalPriceBeforeDiscount);
            result.Discount.Should().Be(Discount);


        }

        [Theory]
        [MemberData(nameof(ProductToShopCase))]
        public void RemoveProductInCartShouldWork(Product[] products)
        {
            svc = new ShopManage();
            foreach (var product in products)
            {
                svc.AddNewProductToShop(product);
            }
            var shopProduct = svc.GetAllProductInShop();
            var removeProduct = shopProduct.First();
            svc.RemoveProductInCart(removeProduct.ID);
            var result = svc.GetCurrentCart();
            result.Product.Any(it => it == removeProduct).Should().BeFalse();
        }

        public static IEnumerable<object[]> ProductToShopCase => new List<object[]>
        {
            new object[]{
                new Product[]{
                    new Product{ Name = "iPhone", Price = 50000},
                    new Product{ Name = "TV LG50", Price = 29999},
                    new Product{ Name = "iPad", Price = 16999},
                    new Product{ Name = "PS4 Game", Price = 1599},
                    new Product{ Name = "PS4 Game", Price = 1800}
                }
            }
        };

        public static IEnumerable<object[]> AddProductToCartCase => new List<object[]>
        {
            new object[]{
                new Product{ Name = "iPhone", Price = 50000},
                5,
                250000,
                50000
            },
            new object[]{
                new Product{ Name = "TV LG50", Price = 29999},
                2,
                59998,
                0
            },
            new object[]{
                new Product{ Name = "iPad", Price = 16999},
                3,
                50997,
                0
            },
            new object[]{
                new Product{ Name = "iPad", Price = 16999},
                4,
                67996,
                16999
            },
            new object[]{
                new Product{ Name = "TV LG50", Price = 29999},
                7,
                209993,
                29999
            },
            new object[]{
                new Product{ Name = "TV LG50", Price = 29999},
                8,
                239992,
                59998
            },
        };
    }
}
