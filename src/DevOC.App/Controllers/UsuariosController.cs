using AutoMapper;
using DevOC.App.Filters;
using DevOC.App.ViewModels;
using DevOC.Business.Interfaces;
using DevOC.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DevOC.App.Extensions.CustomAuthorization;

namespace DevOC.App.Controllers
{

    //[Authorize]
    [PaginaRestritaAdmin]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IOrcamentoPessoalRepository _orcamentoPessoalRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository,
                                  IOrcamentoPessoalRepository orcamentoPessoalRepository,
                                  IUsuarioService usuarioService, 
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _orcamentoPessoalRepository = orcamentoPessoalRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        //GET: Fornecedores
        [AllowAnonymous]
        [Route("lista-de-usuarios")]
        public async Task <IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos()));
        }

        // GET: Fornecedores/Details/5/
        [AllowAnonymous]
        [Route("dados-do-usuario/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var usuarioViewModel = await ObterUsuarioEndereco(id);

            if (usuarioViewModel == null)
            {
                return NotFound();
            }

            return View(usuarioViewModel);
        }

        // GET: Fornecedores/Create
        //[ClaimsAuthorize("Usuario", "Adicionar")]
        [Route("novo-usuario")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[ClaimsAuthorize("Usuario", "Adicionar")]
        [Route("novo-usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel usuarioViewModel)
        {
            usuarioViewModel.DateCadastro = DateTime.Now;
            /*if (!ModelState.IsValid)
            {
                return View(usuarioViewModel);

            }*/
            
            var usuario = _mapper.Map<Usuario>(usuarioViewModel);
            await _usuarioService.Adicionar(usuario);

            if (!OperacaoValida()) return View(usuarioViewModel);

            return RedirectToAction("Index");
        }

        // GET: Fornecedores/Edit/5
        //[ClaimsAuthorize("Usuario", "Editar")]
        [Route("editar-usuario/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {


            var usuarioViewModel = await ObterUsuarioOrcamentoEndereco(id);
            if (usuarioViewModel == null)
            {
                return NotFound();
            }
            return View(usuarioViewModel);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[ClaimsAuthorize("Usuario", "Editar")]
        [Route("editar-usuario/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UsuarioSemSenhaViewModel usuarioSemSenhaViewModel)
        {
            if (id != usuarioSemSenhaViewModel.Id)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid) return View(usuarioViewModel);
            var usuarioViewModel = await ObterUsuarioOrcamentoEndereco(id);
            usuarioViewModel.Id = usuarioSemSenhaViewModel.Id;
            usuarioViewModel.Nome = usuarioSemSenhaViewModel.Nome;
            usuarioViewModel.Login = usuarioSemSenhaViewModel.Login;
            usuarioViewModel.Email = usuarioSemSenhaViewModel.Email;
            usuarioViewModel.Celular = usuarioSemSenhaViewModel.Celular;
            usuarioViewModel.Perfil = usuarioSemSenhaViewModel.Perfil;
            

            var usuario = _mapper.Map<Usuario>(usuarioViewModel);
            await _usuarioService.Atualizar(usuario);

            if (!OperacaoValida()) return View(usuarioViewModel);

            return RedirectToAction(nameof(Index));


        }

        // GET: Fornecedores/Delete/5
        //[ClaimsAuthorize("Usuario", "Excluir")]
        [Route("excluir-usuario/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {


            var usuarioViewModel = await ObterUsuarioEndereco(id);
            if (usuarioViewModel == null)
            {
                return NotFound();
            }

            return View(usuarioViewModel);
        }

        // POST: Fornecedores/Delete/5
       // [ClaimsAuthorize("Usuario", "Excluir")]
        [Route("excluir-usuario/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuarioViewModel = await ObterUsuarioEndereco(id);

            if (usuarioViewModel == null)
            {
                return NotFound();
            }

            await _usuarioService.Remover(id);

            if (!OperacaoValida()) return View(usuarioViewModel);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [Route("obter-endereco-usuario/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var usuario = await ObterUsuarioEndereco(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", usuario);
        }

        //[ClaimsAuthorize("Usuario", "Editar")]
        [Route("atualizar-endereco-usuario/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var usuario = await ObterUsuarioEndereco(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new UsuarioViewModel { Endereco = usuario.Endereco });
        }

        //[ClaimsAuthorize("Usuario", "Editar")]
        [Route("atualizar-endereco-usuario/{id:guid}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(UsuarioViewModel usuarioViewModel)
        {
            ModelState.Remove("Nome");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", usuarioViewModel);

            await _usuarioService.AtualizarEndereco(_mapper.Map<Endereco>(usuarioViewModel.Endereco));

            if (!OperacaoValida()) return PartialView("_AtualizarEndereco", usuarioViewModel);


            var url = Url.Action("ObterEndereco", "Usuarios", new { id = usuarioViewModel.Endereco.UsuarioId });
            return Json(new { success = true, url });
        }


        private async Task<UsuarioViewModel> ObterUsuarioEndereco(Guid id)
        {
            return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.ObterUsuarioEndereco(id));
        }

        private async Task<UsuarioViewModel> ObterUsuarioOrcamentoEndereco(Guid id)
        {
            return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.ListarPorId(id));
        }

        public async Task<IActionResult> ListarContatosPorUsuarioId(Guid id)
        {
            var orcamento = _mapper.Map<IEnumerable<OrcamentoPessoalViewModel>>(await _orcamentoPessoalRepository.BuscarTodos(id));
            return PartialView("_OrcamentosUsuario", orcamento);
        }
    }
}
