using DevOC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> BuscarPorLoginEmail(string login, string email);

        Task<Usuario> BuscarPorlogin(string login);
        Task<Usuario> ListarPorId(Guid id);
        Task<IEnumerable<Usuario>> ObterUsuarioOrcamentoEndereco();
        Task<Usuario> ObterUsuarioEndereco(Guid id);

    }
}
