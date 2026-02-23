using System;

namespace Cafeteria.Models.Dtos.Order;

public class CreateOrderDto
{
    public int? ClientId {get;set;}
    public int Status {get;set;}
    public bool IsPai {get;set;}
    public int TableId {get;set;}

    //Definir los enums de Status
    //1 -> Creado
    //2 -> Cancelado
    //3 -> Pagado
}
