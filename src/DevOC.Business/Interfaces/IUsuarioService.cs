using DevOC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(Usuario usuario);

        Task Atualizar(Usuario usuario);

        Task AtualizarSenha(Usuario usuario, string senhaAtual, string senhaNova);

        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
