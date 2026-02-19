using System;

namespace Cafeteria.Models.Dtos.Order;

public class CancelOrderDto
{
    public DateTime? CancelAt {get;set;} = DateTime.UtcNow;
    public string? CancelReason {get;set;} = string.Empty;
    public int? ClientId {get;set;}
    public int? CancelById {get;set;}
    public int? Status {get;set;}

    //Definir los enums de Status
    //1 -> Creado
    //2 -> Cancelado
    //3 -> Pagado

}
