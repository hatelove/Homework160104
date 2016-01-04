using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework160104;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Homework160104.Tests
{
    [TestClass]
    public class PagingHelperTests
    {
        private List<Product> _products;

        [TestInitialize]
        public void GetTestSource()
        {
            _products = new List<Product>
                {
                    new Product {Id = 1, Cost = 1, Revenue = 0, SellPrice = 0},
                    new Product {Id = 2, Cost = 0, Revenue = 2, SellPrice = 0},
                    new Product {Id = 3, Cost = 0, Revenue = 0, SellPrice = 3},
                    new Product {Id = 4, Cost = 1, Revenue = 0, SellPrice = 3},
                    new Product {Id = 5, Cost = 1, Revenue = 2, SellPrice = 0},
                    new Product {Id = 6, Cost = 0, Revenue = 2, SellPrice = 3},
                    new Product {Id = 7, Cost = 1, Revenue = 2, SellPrice = 3},
                    new Product {Id = 8, Cost = 1, Revenue = 2, SellPrice = 3},
                    new Product {Id = 9, Cost = 1, Revenue = 2, SellPrice = 3}
                };
        }

        [TestMethod]
        public void SumByPagingTest_column_cost_paging_3_should_1_2_3()
        {
            IProductSource source = Substitute.For<IProductSource>();
            ColumnType columnType = ColumnType.Cost;
            int paging = 3;
            source.GetSource().ReturnsForAnyArgs(_products);

            List<int> expected = new List<int> { 1, 2, 3 };

            PagingHelper helper = new PagingHelper(source);
            List<int> actual = helper.SumByPaging(columnType, paging);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SumByPagingTest_column_revenue_paging_3_should_2_4_6()
        {
            IProductSource source = Substitute.For<IProductSource>();
            ColumnType columnType = ColumnType.Revenue;
            int paging = 3;
            source.GetSource().ReturnsForAnyArgs(_products);

            List<int> expected = new List<int> { 2, 4, 6 };

            PagingHelper helper = new PagingHelper(source);
            List<int> actual = helper.SumByPaging(columnType, paging);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SumByPagingTest_column_sell_price_paging_3_should_3_6_9()
        {
            IProductSource source = Substitute.For<IProductSource>();
            ColumnType columnType = ColumnType.SellPrice;
            int paging = 3;
            source.GetSource().ReturnsForAnyArgs(_products);

            List<int> expected = new List<int> { 3, 6, 9 };

            PagingHelper helper = new PagingHelper(source);
            List<int> actual = helper.SumByPaging(columnType, paging);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SumByPagingTest_column_cost_paging_2_should_1_1_1_2_1()
        {
            IProductSource source = Substitute.For<IProductSource>();
            ColumnType columnType = ColumnType.Cost;
            int paging = 2;
            source.GetSource().ReturnsForAnyArgs(_products);

            List<int> expected = new List<int> { 1, 1, 1, 2, 1 };

            PagingHelper helper = new PagingHelper(source);
            List<int> actual = helper.SumByPaging(columnType, paging);

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
