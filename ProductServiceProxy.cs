using System;
using System.Collections.Generic;
using System.Linq;
using Spring2025_Samples.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Product>();
        }

        private int LastKey
        {
            get
            {
                return Products.Any() ? Products.Max(p => p.Id) : 0;
            }
        }

        private static ProductServiceProxy? instance;
        private static readonly object instanceLock = new object();
        
        public static ProductServiceProxy Current
        {
            get
            {
                lock(instanceLock) 
                {
                    instance ??= new ProductServiceProxy();
                }
                return instance;
            }
        }

        public List<Product> Products { get; private set; }

        public Product AddOrUpdate(Product product)
        {
            if(product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }
            return product;
        }

        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
            }
            return product;
        }
    }
}