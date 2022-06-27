using System.ComponentModel;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Vehicles;

public class VehicleByAgencyModel : List<VehicleByAgencyResponse>
{
    public int StatusFiltered { get; set; }
}

public class VehicleByAgencyResponse
{
    public string Plate { get; set; }
    public string Model { get; set; }
    public string VehicleGroup { get; set; }
    public string VehicleStatus { get; set; }
    public VehicleStatus Status { get; set; }
}
