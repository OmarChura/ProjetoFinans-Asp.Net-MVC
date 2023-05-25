using DevOC.Business.Interfaces;
using DevOC.Business.Models;
using DevOC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Data.Repository
{
    public class OrcamentoPessoalRepository : Repository<OrcamentoPessoal>, IOrcamentoPessoalRepository
    {
        public OrcamentoPessoalRepository(MeuDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<OrcamentoPessoal>> BuscarTodos(Guid usuarioId)
        {
            return await Buscar(p => p.UsuarioId == usuarioId);

        }
        public async Task<IEnumerable<OrcamentoPessoal>> BuscarTodosOrcamentosUsuario(Guid usuarioId)
        {
            //return await Buscar(p => p.UsuarioId == usuarioId);
            return await Db.Orcamentos.AsNoTracking().Where(o => o.UsuarioId == usuarioId)
                .Include(f => f.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrcamentoPessoal>> ObterOrcamentosUsuarios()
        {
            return await Db.Orcamentos.AsNoTracking().Include(f => f.Usuario)
                .OrderBy(p => p.TipoDespesa).ToListAsync();
        }

        public async Task<OrcamentoPessoal> ListarPorId(Guid id)
        {
            return await Db.Orcamentos.AsNoTracking()
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
