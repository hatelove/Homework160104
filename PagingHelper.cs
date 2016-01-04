using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework160104
{
    public class PagingHelper
    {
        private readonly IProductSource _source;

        public PagingHelper(IProductSource source)
        {
            _source = source;
        }

        public List<int> SumByPaging(ColumnType type, int pagingCount)
        {
            List<Product> products = _source.GetSource();

            List<PagingProductSum> productSums = Paging(products, pagingCount);

            switch (type)
            {
                case ColumnType.Cost:
                    return productSums.Select(s => s.Cost).ToList();
                case ColumnType.Revenue:
                    return productSums.Select(s => s.Revenue).ToList();
                case ColumnType.SellPrice:
                    return productSums.Select(s => s.SellPrice).ToList();
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        private List<PagingProductSum> Paging(List<Product> products, int rows)
        {
            List<PagingProductSum> sums = new List<PagingProductSum>();

            int record = Convert.ToInt16(Math.Ceiling((double)products.Count/rows));

            for (int i = 1; i < record + 1 ; i++)
            {
                IEnumerable<Product> enumerable = products.Skip((i - 1)*rows).Take(rows);

                PagingProductSum sum = new PagingProductSum();

                foreach (var product in enumerable)
                {
                    sum.Cost += product.Cost;
                    sum.Revenue += product.Revenue;
                    sum.SellPrice += product.SellPrice;
                }

                sums.Add(sum);
            }

            return sums;
        }
    }

    public class PagingProductSum
    {
        public int Cost { get; set; }
        public int Revenue { get; set; }
        public int SellPrice { get; set; }
    }

    public enum ColumnType
    {
        Cost, Revenue, SellPrice
    }

    public interface IProductSource
    {
        List<Product> GetSource();
    }

    public class ProductSource : IProductSource
    {
        public List<Product> GetSource()
        {
            return new List<Product>();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Revenue { get; set; }
        public int SellPrice { get; set; }
    }
}
