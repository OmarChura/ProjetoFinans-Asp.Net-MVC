using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.App.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual do usúario")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha do usúario")]
        [StringLength(200, ErrorMessage = "O campo {0} Precisa ter mais que {2} caracteres", MinimumLength = 6)]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha do usúario")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        [StringLength(200, ErrorMessage = "O campo {0} Precisa ter mais que {2} caracteres", MinimumLength = 6)]
        public string ConfirmarNovaSenha { get; set; }
    }
}
