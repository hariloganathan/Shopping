using Moq;
using Shopping.Core.Domain;
using Shopping.Data;
using Shopping.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shopping.Core.Tests
{
    public class ShoppingCartTests
    {
        private readonly List<Product> products;
        private readonly List<PromoCode> promocodes;
        private readonly Mock<IProducts> productsDataMock;
        private readonly Mock<IPromocode> promocodeDataMock;
        private readonly ICart Cart;

        public ShoppingCartTests()
        {
            this.products = new List<Product>()
            {
              new Product{ ProductId=1,ProductName=ProductName.Milk,Price=1.00m},
              new Product{ ProductId=2,ProductName=ProductName.Butter,Price=2.00m},
              new Product{ ProductId=3,ProductName=ProductName.Sugar,Price=2.50m},
              new Product{ ProductId=4,ProductName=ProductName.Egg,Price=1.50m}
            };
            this.promocodes = new List<PromoCode>() {
            new PromoCode{ Promocode="TWENTYFIVE",DicountPercentage=0.25m },
            new PromoCode{ Promocode="FIFTY",DicountPercentage=0.50m },
            new PromoCode{ Promocode="SEVENTYFIVE",DicountPercentage=0.75m }
            };
            this.productsDataMock = new Mock<IProducts>();
            this.productsDataMock.Setup(x => x.GetavailableProducts()).Returns(products);
            this.promocodeDataMock = new Mock<IPromocode>();
            this.promocodeDataMock.Setup(x => x.GetPromocodes()).Returns(promocodes);
            Cart = new InMemoryCart();
        }
        [Fact]
        public void ShouldThrowExceptionIfTryingToAddNullProduct()
        {
            List<CartItem> cartItems = new List<CartItem>();

            var exception = Assert.Throws<ArgumentNullException>(() => Cart.AddToCart(null));

            Assert.Equal("product", exception.ParamName);
        }

        [Fact]
        public void ShouldAbleToAddSingleProductToCart()
        {
            // As a User
            // I Want to be able to add a single product to a basket
            List<CartItem> cartItems = Cart.AddToCart(products.First());

            Assert.NotNull(cartItems);
            Assert.Single(cartItems);
            Assert.Equal(1, cartItems.First().Quantity);
        }
        [Fact]
        public void ShouldAbleToAddMultipleProductToCart()
        {
            // As a User
            // I want to be able to add multiple products to a basket
            Cart.AddToCart(products.First());
            Cart.AddToCart(products.First());
            List<CartItem> cartItems = Cart.AddToCart(products.Last());

            Assert.NotNull(cartItems);
            Assert.Equal(2, cartItems.Count);
            Assert.Equal(2, cartItems.First().Quantity);

        }

        [Fact]
        public void ShouldbeAbleToSeeTotalCostOfTheBasket()
        {
            //As a User
            //I want to be able to see the total cost of my basket
            //Arrange
            List<CartItem> cartItems = new List<CartItem>() {
                                                                new CartItem{ ProductId=1, Quantity=1,Product= products.First(p=>p.ProductId==1)  },
                                                                new CartItem{ ProductId=2, Quantity=2,Product= products.First(p=>p.ProductId==2)  },
                                                            };

            //Act
            decimal totalCost = Cart.CalculateTotal(cartItems);

            Assert.Equal(5.0m, totalCost);

        }
        [Fact]
        public void ShouldReturnAllPromocodes()
        {
            Assert.NotNull(promocodes);
            Assert.Equal(3, promocodes.Count);

        }
        [Fact]
        public void ShouldThrowExceptionIfPromoCodeIsNotValid()
        {
            List<CartItem> cartItems = new List<CartItem>();

            var exception = Assert.Throws<ArgumentNullException>(() => Cart.CalculateDiscount(cartItems, null));

            Assert.Equal("promocode", exception.ParamName);
        }

        [Fact]
        public void ShouldBeAbleToSeePriceBeforeAndAfterDiscount()
        {
            //As a User
            //I want to be able apply a discount code and see the price before and after the discount
            //Arrange
            List<CartItem> cartItems = new List<CartItem>() {
                                                                new CartItem{ ProductId=1, Quantity=1,Product= products.First(p=>p.ProductId==1)  },
                                                                new CartItem{ ProductId=2, Quantity=2,Product= products.First(p=>p.ProductId==2)  },
                                                            };

            //Act
            decimal totalCost = Cart.CalculateTotal(cartItems);
            decimal totalDiscount = Cart.CalculateDiscount(cartItems, promocodes.FirstOrDefault(p => p.Promocode == "TWENTYFIVE"));

            //Assert
            Assert.Equal((decimal)5, totalCost);//price
            Assert.Equal((decimal)1.25, totalDiscount);//discount
            Assert.Equal((decimal)3.75, totalCost - totalDiscount);//after discount
            //Assert.True(totalCost > totalDiscount);


        }
    }
}
