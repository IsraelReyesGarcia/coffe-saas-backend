using System;

namespace Cafeteria.Models.Dtos.Order;

public class PayOrderDto
{
    public float Total {get;set;}
    public float? SubTotal {get;set;}
    public float? Iva {get;set;}
    public float? Discount {get;set;}
    public int? PaymentType {get;set;}
    //Enum de PaymentType
    // 1 -> Efectivo
    // 2 -> Transferencia
    public int? ClientId {get;set;}
    public int? FinishById {get;set;}
    public bool IsPai {get;set;}
}
