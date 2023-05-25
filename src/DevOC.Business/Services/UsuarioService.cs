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
    public class UsuarioService : BaseService, IUsuarioService
    {
        //framework fluent validation
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEnderecoRepository _enderecoRepository;


        public UsuarioService(IUsuarioRepository usuarioRepository, 
                            INotificador notificador,
                            IEnderecoRepository enderecoRepository) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Usuario usuario) 
        {
            usuario.DateCadastro = DateTime.Now;
            usuario.SetSenhaHash();

            //validar o estado da entidade
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)
                || !ExecutarValidacao(new EnderecoValidation(), usuario.Endereco)) return;


            //validar se nao existe suario com o mesmo login
            if (_usuarioRepository.Buscar(f => f.Login == usuario.Login).Result.Any())
            {
                Notificar("Já existe um usuario com este login infomado.");
                return;
            }

            await _usuarioRepository.Adicionar(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            usuario.DataAtualizacao = DateTime.Now;
            if (_usuarioRepository.ListarPorId(usuario.Id) == null)
            {
                throw new SystemException("houve um erro na atualização do usuario");
            }

            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return;

            if (_usuarioRepository.Buscar(f => f.Login == usuario.Login && f.Id != usuario.Id).Result.Any())
            {
                Notificar("Já existe um usuario com este login infomado.");
                return;
            }

            await _usuarioRepository.Atualizar(usuario);
        }
        public async Task AtualizarSenha(Usuario usuario, string senhaAtual, string senhaNova)
        {
            
            if (_usuarioRepository.ListarPorId(usuario.Id) == null)
            {
                throw new SystemException("houve um erro na atualização do usuario");
            }

            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return;

            if (!usuario.SenhaValida(senhaAtual)) throw new SystemException("Senha atual nao confere");

            if (usuario.SenhaValida(senhaNova)) throw new SystemException("Nova senha deve ser diferente da senha atual");

            usuario.SetNovaSenha(senhaNova);
            usuario.DataAtualizacao = DateTime.Now;

            await _usuarioRepository.Atualizar(usuario);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            /*
            if (_usuarioRepository.ListarPorId(id) == null)
            {
                throw new SystemException("houve um erro na deleção do usuario");
            }
            */
            if (_usuarioRepository.ListarPorId(id).Result.Orcamentos.Any())
            {
                Notificar("O usuario possui Despesas de orçamento cadastrados!");
                return;
            }

            var endereco = await _enderecoRepository.ObterEnderecoPorUsuario(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }


            await _usuarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
