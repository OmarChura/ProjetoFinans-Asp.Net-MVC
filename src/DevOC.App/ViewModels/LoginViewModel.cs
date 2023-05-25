using System.ComponentModel.DataAnnotations;

namespace DevOC.App.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite o Login do usúario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a Senha do usúario")]
        public string Senha { get; set; }
    }
}
