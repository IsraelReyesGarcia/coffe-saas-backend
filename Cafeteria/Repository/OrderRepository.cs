using System;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CancelOrder(Order order)
    {
        if(order == null)
        {
            return false;
        }

        _db.Orders.Update(order);
        return Save();
    }

    public bool CreateOrder(Order order)
    {
        if(order == null){
            return false;
        }

        order.CreateAt = DateTime.UtcNow;
        _db.Orders.Add(order);

        return Save();
    }

    public bool PayOrder(Order order)
    {
        if(order == null)
        {
            return false;
        }

        _db.Orders.Update(order);
        return Save();
    }

    public bool DeleteOrder(Order order)
    {
        if(order == null){
            return false;
        }

        _db.Orders.Remove(order);
        return Save();
    }

    public Order? GetOrder(int id)
    {
        if(id <= 1)
        {
            return null;
        }

        return _db.Orders.FirstOrDefault(o => o.OrderId == id);
    }

    public ICollection<Order> GetOrders()
    {
        return _db.Orders.OrderBy(o => o.OrderId).ToList();
    }

    public bool OrderExists(int id)
    {
        if(id <= 0){
            return false;
        }
        return _db.Orders.Any(o => o.OrderId == id);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 1 ? true : false;
    }
}
