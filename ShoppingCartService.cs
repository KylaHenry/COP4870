using System;
using System.Collections.Generic;
using System.Linq;
using Spring2025_Samples.Models;
using Library.eCommerce.Services;

namespace MyApp
{
    public class ShoppingCartService
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ProductServiceProxy _inventory;

        public ShoppingCartService(ShoppingCart shoppingCart, ProductServiceProxy inventory)
        {
            _shoppingCart = shoppingCart;
            _inventory = inventory;
        }

        public void AddItemToCart()
        {
            Console.Write("Enter product ID to add item to cart: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = _inventory.Products.FirstOrDefault(p => p.Id == id);

                if (product != null)
                {
                    Console.Write($"Enter quantity (max {product.Quantity}): ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && 
                        quantity > 0 && quantity <= product.Quantity)
                    {
                        _shoppingCart.AddItem(product, quantity);
                        product.Quantity -= quantity;
                        Console.WriteLine("Item added to cart!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }

        public void ReadItemInCart()
        {
            if (!_shoppingCart.Items.Any())
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            foreach (var item in _shoppingCart.Items)
            {
                Console.WriteLine($"{item.Product.Name} - {item.Quantity} x ${item.Product.Price:F2} = ${item.Product.Price * item.Quantity:F2}");
            }
        }

                public void RemoveItemFromCart()
        {
            Console.Write("Enter product ID to remove from cart: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = _inventory.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    var cartItem = _shoppingCart.Items.FirstOrDefault(i => i.Product.Id == id);
                    if (cartItem != null)
                    {
                        Console.WriteLine($"You have {cartItem.Quantity} of {cartItem.Product.Name} in your cart.");
                        Console.Write("Enter quantity to remove: ");

                        if (int.TryParse(Console.ReadLine(), out int quantityToRemove) && quantityToRemove > 0)
                        {
                            if (quantityToRemove > cartItem.Quantity)
                            {
                                Console.WriteLine("Error: You cannot remove more than what is in the cart.");
                            }
                            else
                            {
                                _shoppingCart.RemoveItem(product, quantityToRemove);
                                product.Quantity += quantityToRemove; // Return removed quantity to inventory

                                if (cartItem.Quantity - quantityToRemove == 0)
                                {
                                    Console.WriteLine($"{cartItem.Product.Name} completely removed from cart and returned to inventory.");
                                }
                                else
                                {
                                    Console.WriteLine($"{quantityToRemove} of {cartItem.Product.Name} removed from cart. Remaining in cart: {cartItem.Quantity - quantityToRemove}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity entered. Please enter a positive number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Item not found in cart.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }


        public void Checkout()
        {
            if (!_shoppingCart.Items.Any())
            {
                Console.WriteLine("Cart is empty. Nothing to checkout.");
                return;
            }

            decimal subtotal = _shoppingCart.GetTotal();
            decimal tax = subtotal * 0.07m;
            decimal total = subtotal + tax;

            Console.WriteLine("\n=== RECEIPT ===");
            foreach (var item in _shoppingCart.Items)
            {
                Console.WriteLine($"{item.Product.Name} - {item.Quantity} x ${item.Product.Price:F2} = ${item.Product.Price * item.Quantity:F2}");
            }
            Console.WriteLine("\nSubtotal: ${0:F2}", subtotal);
            Console.WriteLine("Tax (7%): ${0:F2}", tax);
            Console.WriteLine("Total: ${0:F2}", total);
            Console.WriteLine("\nThank you for shopping with us!");

            _shoppingCart.Clear();
        }
    }
}