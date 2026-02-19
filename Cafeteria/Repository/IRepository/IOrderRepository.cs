using System;
using Cafeteria.Models.Dtos.Order;
using Cafeteria.Models;

namespace Cafeteria.Repository.IRepository;

public interface IOrderRepository
{
    //Obtener una orden
    Order? GetOrder(int id);

    //Obtener todas las ordenes
    ICollection<Order> GetOrders();

    //Saber si una orden existe
    bool OrderExists(int id);

    //Crear una orden
    bool CreateOrder(Order order);

    //Pagar orden
    bool PayOrder(Order order);

    //Cancelar una orden
    bool CancelOrder(Order order);

    //Eliminar una orden
    bool DeleteOrder(Order order);

    //Guardar cambios
    bool Save();
}
