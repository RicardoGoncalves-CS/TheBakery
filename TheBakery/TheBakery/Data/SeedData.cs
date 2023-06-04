using TheBakery.Models;

namespace TheBakery.Data
{
    public class SeedData
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<TheBakeryContext>();

            // Removes all data from the tables
            if (context.Customer.Any() ||
                context.Address.Any() ||
                context.Product.Any() ||
                context.Order.Any() ||
                context.OrderDetails.Any())
            {
                context.Customer.RemoveRange(context.Customer);
                context.Address.RemoveRange(context.Address);
                context.Product.RemoveRange(context.Product);
                context.Order.RemoveRange(context.Order);
                context.OrderDetails.RemoveRange(context.OrderDetails);

                context.SaveChanges();
            }

            // Add customers
            var customers = new[]
            {
            new Customer { CustomerName = "Etham Thompson", Phone = "0795426124" },
            new Customer { CustomerName = "Olivia Parker", Phone = "0758521361" },
            new Customer { CustomerName = "Anna Adams", Phone = "0754136670" },
            new Customer { CustomerName = "Liam Watson", Phone = "0795622456" },
            new Customer { CustomerName = "Noah Collins", Phone = "0745412360" },
            new Customer { CustomerName = "Isabella Bennet", Phone = "0771030216" },
            new Customer { CustomerName = "Oliver Mitchell", Phone = "0740037841" },
            new Customer { CustomerName = "Sophia Turner", Phone = "0789837852" }
            };
            context.Customer.AddRange(customers);

            // Add addresses
            var addresses = new[]
            {
            new Address { Number = "17", Street = "Park Lane", City = "London", PostCode = "W1K 7AJ" },
            new Address { Number = "42", Street = "High Street", City = "Manchester", PostCode = "M1 4BH" },
            new Address { Number = "9", Street = "Rosemary Avenue", City = "Birmingham", PostCode = "B15 2TS" },
            new Address { Number = "25", Street = "Victoria Road", City = "Glasgow", PostCode = "G2 4TN" },
            new Address { Number = "8", Street = "Church Street", City = "Edinbrugh", PostCode = "EH1 3DG" },
            new Address { Number = "7", Street = "Oxford Road", City = "Manchester", PostCode = "M1 5QA" },
            new Address { Number = "32", Street = "Baker Street", City = "London", PostCode = "W1U 3BW" },
            new Address { Number = "15", Street = "Broad Street", City = "Birmingham", PostCode = "B1 2HJ" }
            };
            context.Address.AddRange(addresses);

            // Associate customers with addresses
            customers[0].Address = addresses[0];
            customers[1].Address = addresses[1];
            customers[2].Address = addresses[2];
            customers[3].Address = addresses[3];
            customers[4].Address = addresses[4];
            customers[5].Address = addresses[5];
            customers[6].Address = addresses[6];
            customers[7].Address = addresses[7];


            // Add products
            var products = new[]
            {
            new Product { ProductName = "Red Velvet Delight", UnitPrice = 32.00m },
            new Product { ProductName = "Chocolate Fudge Dream", UnitPrice = 30.00m },
            new Product { ProductName = "Vanilla Bliss", UnitPrice = 25.00m },
            new Product { ProductName = "Lemon Zest Delight", UnitPrice = 25.00m },
            new Product { ProductName = "Strawberry Swirl", UnitPrice = 30.00m },
            new Product { ProductName = "Caramel Crunch", UnitPrice = 28.00m },
            new Product { ProductName = "Raspberry Ripple", UnitPrice = 30.00m },
            new Product { ProductName = "Cookies and Cream Heaven", UnitPrice = 35.00m },
            new Product { ProductName = "Coconut Paradise", UnitPrice = 30.00m },
            new Product { ProductName = "Carrot Cake Supreme", UnitPrice = 30.00m },
            // Add more products as needed
            };
            context.Product.AddRange(products);

            var orderDetails = new[]
            {
            new OrderDetails { Product = products[0], Quantity = 2 },
            new OrderDetails { Product = products[1], Quantity = 1 },
            new OrderDetails { Product = products[5], Quantity = 2 },
            new OrderDetails { Product = products[0], Quantity = 1 },
            new OrderDetails { Product = products[3], Quantity = 3 },
            new OrderDetails { Product = products[4], Quantity = 1 },
            new OrderDetails { Product = products[2], Quantity = 2 },
            new OrderDetails { Product = products[3], Quantity = 1 },
            new OrderDetails { Product = products[5], Quantity = 1 },
            new OrderDetails { Product = products[8], Quantity = 1 },
            new OrderDetails { Product = products[1], Quantity = 3 },
            new OrderDetails { Product = products[6], Quantity = 3 },
            new OrderDetails { Product = products[2], Quantity = 2 },
            new OrderDetails { Product = products[7], Quantity = 1 },
            new OrderDetails { Product = products[8], Quantity = 1 },
            new OrderDetails { Product = products[9], Quantity = 1 },
            new OrderDetails { Product = products[0], Quantity = 1 },
            new OrderDetails { Product = products[3], Quantity = 2 },
            new OrderDetails { Product = products[4], Quantity = 1 },
            new OrderDetails { Product = products[0], Quantity = 3 },
            new OrderDetails { Product = products[1], Quantity = 2 },
            new OrderDetails { Product = products[5], Quantity = 1 },
            new OrderDetails { Product = products[8], Quantity = 2 },
            new OrderDetails { Product = products[1], Quantity = 1 },
            new OrderDetails { Product = products[2], Quantity = 1 },
            new OrderDetails { Product = products[5], Quantity = 1 },
            new OrderDetails { Product = products[1], Quantity = 1 },
            new OrderDetails { Product = products[2], Quantity = 2 },
            new OrderDetails { Product = products[6], Quantity = 2 },
            new OrderDetails { Product = products[7], Quantity = 1 },
            new OrderDetails { Product = products[3], Quantity = 1 },
            new OrderDetails { Product = products[4], Quantity = 2 },
            new OrderDetails { Product = products[7], Quantity = 3 },
            new OrderDetails { Product = products[4], Quantity = 2 },
            new OrderDetails { Product = products[6], Quantity = 3 }
            // Add more order details as needed
            };
            context.OrderDetails.AddRange(orderDetails);

            // Add orders
            var orders = new[]
            {
            new Order { OrderDate = DateTime.Now, Customer = customers[0],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[0],
                    orderDetails[1],
                    orderDetails[2],
                    orderDetails[3]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[1],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[4],
                    orderDetails[5],
                    orderDetails[6],
                    orderDetails[7]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[2],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[8],
                    orderDetails[9],
                    orderDetails[10],
                    orderDetails[11]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[3],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[12],
                    orderDetails[13],
                    orderDetails[14],
                    orderDetails[15]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[4],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[16],
                    orderDetails[17],
                    orderDetails[18],
                    orderDetails[19]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[5],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[20],
                    orderDetails[21],
                    orderDetails[22],
                    orderDetails[23]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[6],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[24],
                    orderDetails[25],
                    orderDetails[26],
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[7],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[27],
                    orderDetails[28],
                    orderDetails[29]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[2],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[30],
                    orderDetails[31]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[4],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[32],
                    orderDetails[33]
                }},
            new Order { OrderDate = DateTime.Now, Customer = customers[5],
                OrderDetails = new List<OrderDetails>
                {
                    orderDetails[34]
                }}
            // Add more orders as needed
            };
            context.Order.AddRange(orders);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
