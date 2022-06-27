namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;

public class UserToken
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UserClaim> Claims { get; set; }
}
