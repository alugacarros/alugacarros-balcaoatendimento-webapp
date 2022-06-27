using System.ComponentModel;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Reservations;

public enum ReservationStatus
{
    [Description("Aberta")]
    Opened = 1,
    [Description("Fechada")]
    Closed = 2,
    [Description("Cancelada")]
    Canceled = 3
}