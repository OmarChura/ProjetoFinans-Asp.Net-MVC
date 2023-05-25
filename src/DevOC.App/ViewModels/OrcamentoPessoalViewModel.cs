using DevOC.App.Extensions;
using DevOC.Business.Enums;
using DevOC.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevOC.App.ViewModels
{
    public class OrcamentoPessoalViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TipoMes Mes { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Dia { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Tipo Despesa")]
        public TipoDespesaOrcamento TipoDespesa { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DataAtualizacao { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }


        /* EF Relation*/
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Usuario")]
        public Guid UsuarioId { get; set; }

        public UsuarioViewModel Usuario { get; set; }

        public IEnumerable<UsuarioViewModel> Usuarios { get; set; }

    }
}
