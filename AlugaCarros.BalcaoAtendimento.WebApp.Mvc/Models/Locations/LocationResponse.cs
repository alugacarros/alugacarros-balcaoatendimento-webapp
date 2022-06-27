namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Locations;
public class LocationResponse
{
    public string Code { get; set; }
    public string ReservationCode { get; set; }
    public string AgencyCode { get; set; }
    public string ClientDocument { get; set; }
    public string VehicleGroupCode { get; set; }
    public string VehiclePlate { get; set; }
    public DateTime Date { get; set; }
    public DateTime ReturnDate { get; set; }
    private decimal _value;
    public decimal Value { get { return Math.Round(_value, 2); } set { _value = value; } }
    public decimal SecurityDepositValue { get; set; }
}
