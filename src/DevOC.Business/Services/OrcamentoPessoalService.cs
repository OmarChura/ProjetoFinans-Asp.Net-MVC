using DevOC.Business.Interfaces;
using DevOC.Business.Models;
using DevOC.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Services
{
    public class OrcamentoPessoalService : BaseService, IOrcamentoPessoalService
    {
        private readonly IOrcamentoPessoalRepository _orcamentoPessoalRepository;

        public OrcamentoPessoalService(IOrcamentoPessoalRepository orcamentoPessoalRepository,
                                        INotificador notificador) : base(notificador)
        {
            _orcamentoPessoalRepository = orcamentoPessoalRepository;
        }

        public async Task Adicionar(OrcamentoPessoal orcamentoPessoal)
        {

            if (!ExecutarValidacao(new OrcamentoPessoalValidation(), orcamentoPessoal)) return;

            await _orcamentoPessoalRepository.Adicionar(orcamentoPessoal);
        }

        public async Task Atualizar(OrcamentoPessoal orcamentoPessoal)
        {
            if (_orcamentoPessoalRepository.ListarPorId(orcamentoPessoal.Id) == null)
            {
                throw new Exception("houve um erro na atualização do usuario");
            }

            if (!ExecutarValidacao(new OrcamentoPessoalValidation(), orcamentoPessoal)) return;

            await _orcamentoPessoalRepository.Atualizar(orcamentoPessoal);
        }


        public async Task Remover(Guid id)
        {
            if (_orcamentoPessoalRepository.ListarPorId(id) == null)
            {
                throw new Exception("houve um erro na deleção do usuario");
            }

            await _orcamentoPessoalRepository.Remover(id);
        }

        public void Dispose()
        {
            _orcamentoPessoalRepository?.Dispose();
        }
    }
}
