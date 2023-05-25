using DevOC.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Models
{
    public class OrcamentoPessoal : Entity
    {
        public int Ano { get; set; }

        public TipoMes Mes { get; set; }

        public int Dia { get; set; }

        public TipoDespesaOrcamento TipoDespesa { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public bool Ativo { get; set; }

        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }


    }
}
