using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.Agencies;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Models.User;
public class UserLogin
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [Required(ErrorMessage = "É obrigatório selecionar uma agência")]
    [Display(Name = "Agência")]
    public string AgencyId { get; set; }
    public AgencyResponse Agencies { get; set; }
}


