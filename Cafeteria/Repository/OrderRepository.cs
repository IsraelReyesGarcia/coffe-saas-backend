using System;
using Cafeteria.Models;
using Cafeteria.Models.Enums;
using Cafeteria.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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

        order.Status = (int)OrderStatus.Cancelada;

        _db.Orders.Update(order);
        return Save();
    }

    public bool CreateOrder(Order order)
    {
        if(order == null){
            return false;
        }

        order.Total = (float)order.OrderProducts.Sum(d => d.Quantity * d.Price);
        order.Status = (int)OrderStatus.Creada;
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

        order.Status = (int)OrderStatus.Pagada;

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

        return _db.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefault(o => o.OrderId == id);
    }

    public ICollection<Order> GetOrders()
    {
        return _db.Orders
            .Include(o => o.OrderProducts)
            .OrderBy(o => o.OrderId)
            .ToList();
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

    public bool MarkInPreparation(Order order)
    {
        if(order == null)
        {
            return false;
        }
        
        order.Status = (int)OrderStatus.EnPreparacion;

        _db.Orders.Update(order);

        return Save();
    }

    public bool MarkReady(Order order)
    {
        if(order == null) return false;

        order.Status = (int)OrderStatus.Lista;

        _db.Orders.Update(order);

        return Save();
    }

    public bool MarkDelivered(Order order)
    {
        if(order == null) return false;

        order.Status = (int)OrderStatus.Entregada;

        _db.Orders.Update(order);

        return Save();
    }
}
