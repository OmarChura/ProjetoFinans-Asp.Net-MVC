using AutoMapper;
using DevOC.App.Helper;
using DevOC.App.ViewModels;
using DevOC.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevOC.App.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;
        private readonly IMapper _mapper;

        public LoginController(IUsuarioRepository usuarioRepository, IMapper mapper, ISessao sessao)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            //se usuario estiver logado rediresionar para  a home
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioViewModel usuario = new UsuarioViewModel();
                    usuario = await BuscarPorlogin(loginViewModel);
                    var usuarioModel = await _usuarioRepository.BuscarPorlogin(loginViewModel.Login);


                    if (usuario != null)
                    {
                        if (usuarioModel.SenhaValida(loginViewModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                return View("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        private async Task<UsuarioViewModel> BuscarPorlogin(LoginViewModel loginViewModel)
        {
            return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.BuscarPorlogin(loginViewModel.Login));
        }
    }
}
