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
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context)
        {

        }

        public async Task<Endereco> ObterEnderecoPorUsuario(Guid usuarioId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(e => e.UsuarioId == usuarioId);
        }
    }
}
