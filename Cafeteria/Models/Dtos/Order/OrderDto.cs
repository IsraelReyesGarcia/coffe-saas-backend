using System;

namespace Cafeteria.Models.Dtos.Order;

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt {get;set;} = null;
    public DateTime? CancelAt {get;set;} = null;
    public string? CancelReason {get;set;} = string.Empty;
    public float? Total {get;set;}
    public float? SubTotal {get;set;}
    public float? Iva {get;set;}
    public float? Discount {get;set;}
    public int? PaymentType {get;set;}
    public int? ClientId {get;set;}
    public int? CancelById {get;set;}
    public int? FinishById {get;set;}
    public int Status {get;set;}
    public bool IsPai {get;set;}
    public int TableId {get;set;}
}
