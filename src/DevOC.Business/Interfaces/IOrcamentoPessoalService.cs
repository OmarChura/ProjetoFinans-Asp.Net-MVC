using DevOC.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Interfaces
{
    public interface IOrcamentoPessoalService : IDisposable
    {
        Task Adicionar(OrcamentoPessoal orcamentoPessoal);

        Task Atualizar(OrcamentoPessoal orcamentoPessoal);

        Task Remover(Guid id);
    }
}
