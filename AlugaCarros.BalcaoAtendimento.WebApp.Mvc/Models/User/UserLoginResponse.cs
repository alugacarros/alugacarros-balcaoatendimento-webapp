namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;

public class UserLoginResponse
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
    public string SelectedAgency { get; set; }
}
