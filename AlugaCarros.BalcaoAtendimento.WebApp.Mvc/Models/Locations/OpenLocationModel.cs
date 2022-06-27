using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Reservations;
using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Vehicles;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Locations;
public class OpenLocationModel
{
    [Required]
    public string ReservationCode { get; set; }
    [Required(ErrorMessage = "Necessário selecionar um veículo")]
    [Display(Name = "Veículo")]
    public string SelectedVehicle { get; set; }

    [ValidateNever]
    public LocationReservation Reservation { get; set; } = new LocationReservation();
    private List<VehicleByAgencyResponse> _vehicles { get; set; } = new List<VehicleByAgencyResponse>();
    [ValidateNever]
    public List<VehicleByAgencyResponse> Vehicles
    {
        get { return _vehicles?.Where(w => w.VehicleGroup == Reservation.VehicleGroup).ToList(); }
        set { _vehicles = value; }
    }
    
}

public class LocationReservation
{
    [Display(Name = "Reserva")]
    public string ReservationCode { get; set; }
    [Display(Name = "Cliente")]
    public string Client { get; set; }
    [Display(Name = "Documento")]
    public string ClientDocument { get; set; }
    public string GroupCode { get; set; }
    public string GroupDescription { get; set; }
    [Display(Name = "Grupo")]
    public string VehicleGroup { get { return $"{GroupCode} - {GroupDescription}"; } }
    [Display(Name = "Data Reserva")]
    public DateTime PickupDate { get; set; }
    [Display(Name = "Data Devolução")]
    public string FormatedReturnDate => ReturnDate.ToString("dd/MM/yyyy");
    public DateTime ReturnDate { get; set; }
    [Display(Name = "Data Locação")]
    public string FormatedLocationDate => LocationDate.ToString("dd/MM/yyyy");
    public DateTime LocationDate { get; set; }
    [Display(Name = "Valor Reserva")]
    public string FormatedReservationValue => ReservationValue.ToString("C");
    public decimal ReservationValue { get; set; }

    [Display(Name = "Depósito Segurança")]
    public string FormatedSecurityDepositValue => SecurityDepositValue.ToString("C");
    public decimal SecurityDepositValue { get; set; }
    [Display(Name = "Dias")]
    public int Days { get; set; }

    public static implicit operator LocationReservation(ReservationResponseItem reservation)
    {
        return new LocationReservation()
        {
            Client = reservation.Client,
            ClientDocument = reservation.ClientDocument,
            Days = reservation.Days,
            GroupCode = reservation.GroupCode,
            GroupDescription = reservation.GroupDescription,
            LocationDate = DateTime.Now,
            PickupDate = reservation.PickupDate,
            ReservationCode = reservation.ReservationCode,
            ReservationValue = reservation.ReservationValue,
            ReturnDate = reservation.ReturnDate,
            SecurityDepositValue = reservation.SecurityDepositValue
        };
    }
}