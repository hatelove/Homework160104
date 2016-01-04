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
        public void InitialTestSource()
        {
            _products = new List<Product>
                {
                    //跟需求的資料不一樣，請務必練習從需求/spec來撰寫等義的測試案例
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

        [TestCleanup]
        public void ClearupTestSource()
        {
            _products = null;
        }

        // 請依照需求上面的 scenario 設計測試案例
        [TestMethod]
        public void SumByPagingTest_column_cost_paging_3_should_1_2_3()
        {
            IProductSource source = Substitute.For<IProductSource>();
            //enum是一種標準解法, 透過 string + reflection 也是一種, 更漂亮的是透過 generic + lambda, 寫起來最快、彈性最好、風險也最低、易讀性也最高
            ColumnType columnType = ColumnType.Cost;
            int paging = 3;
            source.GetSource().ReturnsForAnyArgs(_products);

            List<int> expected = new List<int> { 1, 2, 3 };

            PagingHelper helper = new PagingHelper(source);
            List<int> actual = helper.SumByPaging(columnType, paging);

            //這個結果順序是重要的，所以應該用 CollectionAssert.AreEqual, 而不是AreEquivalent
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
