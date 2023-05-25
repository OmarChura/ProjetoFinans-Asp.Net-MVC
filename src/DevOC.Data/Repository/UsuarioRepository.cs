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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Usuario> BuscarPorlogin(string login)
        {
            return await Db.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Login.ToUpper() == login.ToUpper());
        }

        public async Task<Usuario> BuscarPorLoginEmail(string login, string email)
        {
            return await Db.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email.ToUpper() == email.ToUpper() && e.Login.ToUpper() == login.ToUpper());
        }

        public async Task<IEnumerable<Usuario>> ObterUsuarioOrcamentoEndereco()
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(x => x.Orcamentos)
                .Include(x => x.Endereco)
                .OrderBy(x => x.Login)
                .ToListAsync();
        }

        public async Task<Usuario> ListarPorId(Guid id)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(e => e.Orcamentos)
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Usuario> ObterUsuarioEndereco(Guid id)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
