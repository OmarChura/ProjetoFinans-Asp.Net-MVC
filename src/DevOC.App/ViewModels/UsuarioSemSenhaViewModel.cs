using DevOC.Business.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevOC.App.ViewModels
{
    public class UsuarioSemSenhaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Digite o Nome do usúario")]
        [StringLength(200, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do usúario")]
        [StringLength(100, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o E-mail do usúario")]
        [EmailAddress(ErrorMessage = "E-mail invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "Celular invalido")]
        public string Celular { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Selecione o Perfil do usúario")]
        public TiposUsuarios Perfil { get; set; }
    }
}
