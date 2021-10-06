using DiscountStore.Core.DAL;
using DiscountStore.Core.Models;
using DiscountStore.Core.Services;
using Moq;
using NUnit.Framework;

namespace DiscountStore.Services.Tests
{
    public class Tests
    {
        private ICartService _service;

        private readonly Item _vase = new()
        {
            ItemKey = "Vase",
            Price = 1.2
        };
        private readonly Item _mug = new()
        {
            ItemKey = "Big mug",
            Price = 1
        };
        private readonly Item _napkins = new()
        {
            ItemKey = "Napkins pack",
            Price = 0.45
        };

        [SetUp]
        public void Setup()
        {
            var discountDALMock = new Mock<IDiscountDAL>();
            _service = new CartService(discountDALMock.Object);

            discountDALMock.Setup(d => d.GetCurrentDiscounts()).Returns(Core.Mocks.DiscountDALMockedData);
        }

        [Test]
        public void GetPrice_EmptyCart_ShouldReturnZero()
        {
            double price = _service.GetTotal();

            Assert.AreEqual(0, price);
        }

        [Test]
        public void GetPrice_SomeNotDiscountedItems_ShouldReturnValidPrice()
        {
            
            _service.Add(_vase);
            _service.Add(_vase);
            _service.Add(_vase);

            double price = _service.GetTotal();

            Assert.AreEqual(_vase.Price * 3, price);
        }

        [Test]
        public void GetPrice_SomeDiscountedItems_ShouldReturnValidPrice()
        {

            _service.Add(_mug);
            _service.Add(_mug);
            _service.Add(_mug);

            double price = _service.GetTotal();

            Assert.AreEqual(_mug.Price + Core.Mocks.DiscountDALMockedData[_mug.ItemKey].DiscountedPrice, price);
        }

        [Test]
        public void GetPrice_MultipleItems_ShouldReturnValidPrice()
        {

            _service.Add(_mug);
            _service.Add(_mug);
            _service.Add(_mug);
            double mugsPrice = _mug.Price + Core.Mocks.DiscountDALMockedData[_mug.ItemKey].DiscountedPrice;
            _service.Add(_vase);
            _service.Add(_vase);
            double vasesPrice = _vase.Price * 2;
            _service.Add(_napkins);
            _service.Add(_napkins);
            _service.Add(_napkins);
            double napkinsPrice = Core.Mocks.DiscountDALMockedData[_napkins.ItemKey].DiscountedPrice;

            double price = _service.GetTotal();

            Assert.AreEqual(mugsPrice + vasesPrice + napkinsPrice, price);

            _service.Remove(_mug);
            _service.Remove(_mug);
            mugsPrice = _mug.Price;
            price = _service.GetTotal();
            Assert.AreEqual(mugsPrice + vasesPrice + napkinsPrice, price);
        }
    }
}