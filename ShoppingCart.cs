using Spring2025_Samples.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.eCommerce.Services
{
    public class ShoppingCart
    {
        // Making CartItem public to match the accessibility of the Items property
        public class CartItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal SubTotal => Product.Price * Quantity;
        }

        private List<CartItem> _items = new List<CartItem>();
        
        public IEnumerable<CartItem> Items => _items;

        public void AddItem(Product product, int quantity)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Id == product.Id);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem 
                { 
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void RemoveItem(Product product, int quantity)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Id == product.Id);
            
            if (existingItem == null)
            {
                throw new ArgumentException("Product not found in cart");
            }

            if (quantity >= existingItem.Quantity)
            {
                _items.Remove(existingItem);
            }
            else
            {
                existingItem.Quantity -= quantity;
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public decimal GetTotal()
        {
            return _items.Sum(item => item.SubTotal);
        }
    }
}