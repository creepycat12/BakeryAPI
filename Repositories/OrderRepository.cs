using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;

namespace bakery.api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly IAddressRepository _addressrepo;
    public OrderRepository(DataContext context, IAddressRepository addressrepo)
    {
        _addressrepo = addressrepo;
        _context = context;

    }

    public async Task<bool> Add(OrdersPostViewModel model)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Name.Replace(" ", "").ToLower().Trim() == model.Name.Replace(" ", "").ToLower().Trim());
        if (customer is null)
        {
            var deliveryAddress = await _addressrepo.Add(new AddressPostViewModel
            {
                AddressLine = model.DeliveryAddress,
                PostalCode = model.DeliveryPostalCode,
                City = model.DeliveryCity,
                AddressType = AddressTypeEnum.Delivery
            });

            // Use the Address Repository to check and add Invoice Address
            var invoiceAddress = await _addressrepo.Add(new AddressPostViewModel
            {
                AddressLine = model.InvoiceAddress,
                PostalCode = model.InvoicePostalCode,
                City = model.InvoiceCity,
                AddressType = AddressTypeEnum.Invoice
            });

            var ContactPerson = await _context.Contact
                .FirstOrDefaultAsync(c => c.ContactPerson.ToLower().Trim()
                == model.ContactPerson.ToLower().Trim());

            var Email = await _context.Contact
                .FirstOrDefaultAsync(c => c.Email.ToLower().Trim() == model.Email.ToLower().Trim());

            var Phone = await _context.Contact
                .FirstOrDefaultAsync(c => c.PhoneNumber.Replace(" ", "").Trim()
                == model.Phone.Replace(" ", "").Trim());

            if (ContactPerson is null && Email is null)
            {
                ContactPerson = new Contact
                {
                    ContactPerson = model.ContactPerson.ToLower().Trim(),
                    Email = model.Email,
                    PhoneNumber = model.Phone
                };
                await _context.Contact.AddAsync(ContactPerson);
            }

            customer = new Customer
            {
                Name = model.Name,
            };

            await _context.Customers.AddAsync(customer);

            var CC = new CustomerContact
            {
                Contact = ContactPerson,
                Customer = customer
            };
            await _context.CustomerContacts.AddAsync(CC);
        }

        var newOrder = new Order
        {
            OrderDate = model.OrderDate,
            OrderNumber = model.OrderNumber,
            OrderProducts = new List<OrderProduct>()
        };

        var customerOrder = new CustomerOrder
        {
            Customer = customer,
            Order = newOrder
        };
        await _context.CustomerOrders.AddAsync(customerOrder);

        foreach (var product in model.Products)
        {
            var prod = await _context.Products.SingleOrDefaultAsync(p => p.Id == product.ProductId);
            if (prod is null)
            {
                throw new Exception($"Product Id {product.ProductId} doesn't exist");
            }

            newOrder.OrderProducts.Add(new OrderProduct
            {
                Quantity = product.Quantity,
                ProductId = product.ProductId
            });
        }
        try
        {
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }


    public async Task<OrdersViewModel> Find(string orderNumber)
    {
        try
        {
            var order = await _context.Orders
           .Where(c => c.OrderNumber == orderNumber)
           .Include(c => c.OrderProducts)
           .Include(c => c.CustomerOrder)
           .Select(c => new OrdersViewModel
           {
               OrderNumber = c.OrderNumber,
               OrderDate = c.OrderDate,
               Customer = c.CustomerOrder.Select(co => co.Customer.Name).FirstOrDefault(),
               Products = c.OrderProducts
                   .Select(c => new OrderProductViewModel
                   {
                       Name = c.Products.Name,
                       PackPrice = c.Products.PackPrice,
                       AmountInPack = c.Products.AmountInPack,
                       Weight_kg = c.Products.Weight_kg,
                       PriceEach = c.Products.PackPrice / c.Products.AmountInPack,
                   }).ToList(),
               Total = c.OrderProducts.Sum(op => op.Products.PackPrice * op.Quantity)
           }).SingleOrDefaultAsync();

            return order;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IList<OrdersViewModel>> Find(DateTime orderDate)
    {
        try
        {
            var orders = await _context.Orders
            .Where(c => c.OrderDate == orderDate)
            .Include(c => c.OrderProducts)
            .Include(c => c.CustomerOrder)
            .Select(c => new OrdersViewModel
            {
                OrderNumber = c.OrderNumber,
                OrderDate = c.OrderDate,
                Customer = c.CustomerOrder.Select(co => co.Customer.Name).FirstOrDefault(),
                Products = c.OrderProducts
                    .Select(c => new OrderProductViewModel
                    {
                        Name = c.Products.Name,
                        PackPrice = c.Products.PackPrice,
                        AmountInPack = c.Products.AmountInPack,
                        Weight_kg = c.Products.Weight_kg,
                        PriceEach = c.Products.PackPrice / c.Products.AmountInPack,
                    }).ToList(),
                Total = c.OrderProducts.Sum(op => op.Products.PackPrice * op.Quantity)
            }).ToListAsync();

            return orders;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IList<OrdersViewModel>> List()
    {
        try
        {
            var response = await _context.Orders
            .Include(c => c.OrderProducts)
                .ThenInclude(c => c.Products)
            .Include(c => c.CustomerOrder)
                .ThenInclude(c => c.Customer)
                .ToListAsync();

            var orders = response.Select(c => new OrdersViewModel
            {
                OrderNumber = c.OrderNumber,
                OrderDate = c.OrderDate,
                Customer = c.CustomerOrder.FirstOrDefault().Customer.Name,
                Products = c.OrderProducts.Select(c => new OrderProductViewModel
                {
                    Name = c.Products.Name,
                    PackPrice = c.Products.PackPrice,
                    AmountInPack = c.Products.AmountInPack,
                    Weight_kg = c.Products.Weight_kg,
                    PriceEach = c.Products.PackPrice / c.Products.AmountInPack,
                    Quantity = c.Quantity
                }).ToList(),
                Total = c.OrderProducts.Sum(c => c.Products.PackPrice * c.Quantity)
            }).ToList();

            return orders;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }
}

