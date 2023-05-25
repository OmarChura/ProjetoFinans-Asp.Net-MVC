using AutoMapper;
using DevOC.App.Filters;
using DevOC.App.Helper;
using DevOC.App.ViewModels;
using DevOC.Business.Interfaces;
using DevOC.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevOC.App.Controllers
{
    [PaginaUsuarioLogado]
    public class AlterarSenhaController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepository usuarioRepository, 
                                        IUsuarioService usuarioService, 
                                        IMapper mapper,
                                        ISessao sessao,
                                        INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar(AlterarSenhaViewModel alterarSenhaViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioViewModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    var usuarioViewModel = await ObterUsuarioOrcamentoEndereco(usuarioLogado.Id);

                    var usuario = _mapper.Map<Usuario>(usuarioViewModel);

                    await _usuarioService.AtualizarSenha(usuario, alterarSenhaViewModel.SenhaAtual, alterarSenhaViewModel.NovaSenha);

                    return View("Index", alterarSenhaViewModel);
                }
                return View("Index", alterarSenhaViewModel);
            }
            catch(Exception e)
            {
                return View("Index", alterarSenhaViewModel);
            }
        }

        private async Task<UsuarioViewModel> ObterUsuarioOrcamentoEndereco(Guid id)
        {
            return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.ListarPorId(id));
        }
    }
}
