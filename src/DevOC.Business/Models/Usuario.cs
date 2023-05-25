using DevOC.Business.Enums;
using DevOC.Business.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Models
{
    public class Usuario : Entity
    {
        
        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public string Celular { get; set; }

        public TiposUsuarios Perfil { get; set; }

        public DateTime DateCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public virtual IEnumerable<OrcamentoPessoal> Orcamentos { get; set; }

        public Endereco Endereco { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            {
                Senha = novaSenha.GerarHash();
                return Senha;
            }
        }


    }
}
