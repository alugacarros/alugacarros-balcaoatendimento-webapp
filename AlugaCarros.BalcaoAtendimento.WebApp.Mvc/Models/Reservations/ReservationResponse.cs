namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Reservations;

public class ReservationResponse : List<ReservationResponseItem>
{
    public ReservationResponse()
    {

    }
    public ReservationResponse(List<ReservationResponseItem> items)
    {
        AddRange(items);
    }
}

public class ReservationResponseItem
{
    public string ReservationCode { get; set; }
    public string Client { get; set; }
    public string ClientDocument { get; set; }
    public string GroupCode { get; set; }
    public string GroupDescription { get; set; }
    public string VehicleGroup { get { return $"{GroupCode} - {GroupDescription}"; } }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal ReservationValue { get; set; }
    public decimal SecurityDepositValue { get; set; }
    public ReservationStatus Status { get; set; }
    public int Days { get; set; }
}

public class OpenedReservationModel
{
    public OpenedReservationModel(ReservationResponseItem reservationResponse)
    {
        ReservationCode = reservationResponse.ReservationCode;
        Client = reservationResponse.Client;
        VehicleGroup = reservationResponse.VehicleGroup;
        PickupDate = reservationResponse.PickupDate;
        ReturnDate = reservationResponse.ReturnDate;
        ReservationValue = reservationResponse.ReservationValue;
        SecurityDepositValue = reservationResponse.SecurityDepositValue;
        Status = reservationResponse.Status;
        Days = reservationResponse.Days;
    }
    public string ReservationCode { get; set; }
    public string Client { get; set; }
    public string VehicleGroup { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal ReservationValue { get; set; }
    public decimal SecurityDepositValue { get; set; }
    public ReservationStatus Status { get; set; }
    public int Days { get; set; }
}