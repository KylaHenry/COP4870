using Spring2025_Samples.Models;
using Library.eCommerce.Services;
using System;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Amazon!");
            
            var list = ProductServiceProxy.Current.Products;
            var shoppingCart = new ShoppingCart();
            var cartService = new ShoppingCartService(shoppingCart, ProductServiceProxy.Current);

            char choice;
            do
            {
                Console.WriteLine("\nC. Create new inventory item");
                Console.WriteLine("R. Read all inventory items");
                Console.WriteLine("U. Update an inventory item");
                Console.WriteLine("D. Delete an inventory item");
                Console.WriteLine("A. Add item to cart");
                Console.WriteLine("V. View cart");
                Console.WriteLine("X. Remove item from cart");
                Console.WriteLine("Y. Checkout");
                Console.WriteLine("Q. Quit");

                string? input = Console.ReadLine();
                choice = input?[0] ?? ' ';

                switch (char.ToUpper(choice))
                {
                    case 'C':
                        Console.Write("Enter product name: ");
                        string? name = Console.ReadLine();
                        Console.Write("Enter price: $");
                        if (decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Console.Write("Enter quantity: ");
                            if (int.TryParse(Console.ReadLine(), out int quantity))
                            {
                                ProductServiceProxy.Current.AddOrUpdate(new Product
                                {
                                    Name = name ?? string.Empty,
                                    Price = price,
                                    Quantity = quantity
                                });
                                Console.WriteLine("Product added successfully!");
                            }
                        }
                        break;

                    case 'R':
                        list.ForEach(Console.WriteLine);
                        break;

                    case 'U':
                        Console.WriteLine("Enter the Product ID to update:");
                        if (int.TryParse(Console.ReadLine(), out int selection))
                        {
                            var selectedProd = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
                            if (selectedProd != null)
                            {
                                Console.Write("Enter new name (or press Enter to keep current name): ");
                                string? newName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newName))
                                {
                                    selectedProd.Name = newName;
                                }

                                Console.Write("Enter new price (or press Enter to keep current price): $");
                                string? priceInput = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
                                {
                                    selectedProd.Price = newPrice;
                                }

                                Console.Write("Enter new quantity (or press Enter to keep current quantity): ");
                                string? quantityInput = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(quantityInput) && int.TryParse(quantityInput, out int newQuantity))
                                {
                                    selectedProd.Quantity = newQuantity;
                                }

                                // Ensure the service updates the product
                                ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                                Console.WriteLine("Product updated successfully!");
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
                        break;

                    case 'D':
                        Console.WriteLine("Enter the product ID to delete:");
                        if (int.TryParse(Console.ReadLine(), out selection))
                        {
                            var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
                            if (product != null)
                            {
                                Console.WriteLine($"Current stock for {product.Name}: {product.Quantity}");
                                Console.Write("Enter the quantity to delete: ");
                                
                                if (int.TryParse(Console.ReadLine(), out int deleteQuantity))
                                {
                                    if (deleteQuantity > 0 && deleteQuantity <= product.Quantity)
                                    {
                                        product.Quantity -= deleteQuantity;

                                        // If the quantity reaches zero, remove it from inventory
                                        if (product.Quantity == 0)
                                        {
                                            ProductServiceProxy.Current.Delete(selection);
                                            Console.WriteLine("Product deleted completely from inventory.");
                                        }
                                        else
                                        {
                                            ProductServiceProxy.Current.AddOrUpdate(product);
                                            Console.WriteLine($"{deleteQuantity} items removed from inventory.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid quantity. Cannot delete more than available stock.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid number.");
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
                        break;

                    case 'A':
                        cartService.AddItemToCart();
                        break;

                    case 'V':
                        cartService.ReadItemInCart();
                        break;

                    case 'X':
                        cartService.RemoveItemFromCart();
                        break;

                    case 'Y':
                        cartService.Checkout();
                        break;

                    case 'Q':
                        Console.WriteLine("Thank you for shopping with us!");
                        break;

                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');
        }
    }
}