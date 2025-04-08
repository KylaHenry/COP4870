# COP4870
# eCommerce Platform
## Project Overview

This project is a console-based **eCommerce platform** created for our semester-long course project. The goal is to simulate a shopping experience, allowing users to manage both an inventory of products and their own shopping cart through a menu-driven interface.

The system implements full **CRUD operations** for both the inventory and the shopping cart, while enforcing inventory rules and providing a final receipt with tax upon checkout.

---
##Features

- **Inventory Management**  
  - Create a product and add it to the inventory  
  - Read/view all inventory items  
  - Update product details  
  - Delete a product from inventory  

- **Shopping Cart Management**  
  - Add an item from inventory to the shopping cart  
  - View all items in the cart  
  - Update quantity of items in the cart  
  - Remove items from the cart and return them to inventory  

- **Business Rules**  
  - Inventory count decreases when items are added to cart  
  - Users cannot add more items to their cart than what’s available in inventory  
  - Accurate synchronization between inventory and cart quantities  

- **Checkout Process**  
  - Generates an itemized receipt  
  - Applies a 7% sales tax  
  - Displays total amount due  

- ** Video Walkthrough
  - For a detailed explanation of how the code works and a live demo of the application, check out the YouTube video below:
     -https://youtu.be/fwCC8o0ejh8
---
##How to Run

1. Ensure you have the [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed.
2. Clone this repository.
3. Navigate to the project directory.
4. Build and run the application:

```bash
dotnet build
dotnet run


