using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("orders")]
public class Order
{
    [Column("orderid")]
    public int OrderId { get; set; }

    [Column("createat")]
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    [Column("updateat")]
    public DateTime? UpdateAt {get;set;} = null;

    [Column("cancelat")]
    public DateTime? CancelAt {get;set;} = null;

    [MaxLength(100)]
    [Column("cancelreason")]
    public string? CancelReason {get;set;} = string.Empty;

    [Column("total")]
    public float? Total {get;set;}

    [Column("subtotal")]
    public float? SubTotal {get;set;}

    [Column("iva")]
    public float? Iva {get;set;}

    [Column("discount")]
    public float? Discount {get;set;}

    [Column("paymenttype")]
    public int? PaymentType {get;set;}

    [Column("clientid")]
    public int? ClientId {get;set;}
    [ForeignKey(nameof(ClientId))]
    public Client? Client {get;set;}

    [Column("cancelbyid")]
    public int? CancelById {get;set;}
    [ForeignKey(nameof(CancelById))]
    public User? CancelByUser {get;set;}

    [Column("finishbyid")]
    public int? FinishById {get;set;}
    [ForeignKey(nameof(FinishById))]
    public User? FinishByUser {get;set;}

    [Column("status")]
    public int Status {get;set;}

    [Column("ispaid")]
    public bool IsPai {get;set;}

    [Column("tableid")]
    public int TableId {get;set;}
    [ForeignKey(nameof(TableId))]
    public required Table Table {get;set;}
}
