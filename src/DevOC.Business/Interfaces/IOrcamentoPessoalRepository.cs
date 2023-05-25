using DevOC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Interfaces
{
    public interface IOrcamentoPessoalRepository : IRepository<OrcamentoPessoal>
    {
        Task<OrcamentoPessoal> ListarPorId(Guid id);
        Task<IEnumerable<OrcamentoPessoal>> BuscarTodos(Guid usuarioId);
        Task<IEnumerable<OrcamentoPessoal>> BuscarTodosOrcamentosUsuario(Guid usuarioId);
        Task<IEnumerable<OrcamentoPessoal>> ObterOrcamentosUsuarios();
    }
}
