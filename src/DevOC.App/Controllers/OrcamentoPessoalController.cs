using AutoMapper;
using DevOC.App.Filters;
using DevOC.App.Helper;
using DevOC.App.ViewModels;
using DevOC.Business.Interfaces;
using DevOC.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DevOC.App.Extensions.CustomAuthorization;

namespace DevOC.App.Controllers
{
    //[Authorize]
    [PaginaUsuarioLogado]
    public class OrcamentoPessoalController : BaseController
    {
        
            private readonly IOrcamentoPessoalRepository _orcamentoPessoalRepository;
            private readonly IUsuarioRepository _usuarioRepository;
            private readonly IOrcamentoPessoalService _orcamentoPessoalService;
            private readonly IMapper _mapper;
            private readonly ISessao _sessao;

            public OrcamentoPessoalController(IOrcamentoPessoalRepository orcamentoPessoalRepository,
                                      IUsuarioRepository usuarioRepository,
                                      IOrcamentoPessoalService orcamentoPessoalService,
                                      IMapper mapper,
                                      ISessao sessao,
                                      INotificador notificador) : base(notificador)
            {
                _orcamentoPessoalRepository = orcamentoPessoalRepository;
                _usuarioRepository = usuarioRepository;
                _orcamentoPessoalService = orcamentoPessoalService;
                _mapper = mapper;
                _sessao = sessao;
            }



            // GET: Produtos
            [AllowAnonymous]
            [Route("lista-de-orçamento")]
            public async Task<IActionResult> Index()
            {
                UsuarioViewModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                if(usuarioLogado == null)
                {
                    return View();
                }
                var orcamento = _mapper.Map<IEnumerable<OrcamentoPessoalViewModel>>(await _orcamentoPessoalRepository.BuscarTodosOrcamentosUsuario(usuarioLogado.Id));
                return View(orcamento);
            }

            // GET: Produtos/Details/5
            [AllowAnonymous]
            [Route("dados-do-orçamento/{id:guid}")]
            public async Task<IActionResult> Details(Guid id)
            {
                var orcamentoViewModel = await ObterOrcamento(id);
                if (orcamentoViewModel == null)
                {
                    return NotFound();
                }

                return View(orcamentoViewModel);
            }

            // GET: Produtos/Create
            //[ClaimsAuthorize("Orcamento", "Adicionar")]
            [Route("novo-orcamento")]
            public async Task<IActionResult> Create()
            {
                var orcamentoViewModel = await PopularUsuarios(new OrcamentoPessoalViewModel());
                return View(orcamentoViewModel);
            }

            // POST: Produtos/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[ClaimsAuthorize("Orcamento", "Adicionar")]
            [Route("novo-orcamento")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(OrcamentoPessoalViewModel orcamentoPessoalViewModel)
            {
                orcamentoPessoalViewModel = await PopularUsuarios(orcamentoPessoalViewModel);

                //if (!ModelState.IsValid) return View(orcamentoPessoalViewModel);

                await _orcamentoPessoalService.Adicionar(_mapper.Map<OrcamentoPessoal>(orcamentoPessoalViewModel));

                if (!OperacaoValida()) return View(orcamentoPessoalViewModel);

                return RedirectToAction("Index");
            }

            // GET: Produtos/Edit/5
            //[ClaimsAuthorize("Orcamento", "Editar")]
            [Route("editar-orçamento/{id:guid}")]
            public async Task<IActionResult> Edit(Guid id)
            {


                var orcamentoViewModel = await ObterOrcamento(id);
                if (orcamentoViewModel == null)
                {
                    return NotFound();
                }
                return View(orcamentoViewModel);
            }

            // POST: Produtos/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[ClaimsAuthorize("Orcamento", "Editar")]
            [Route("editar-orçamento/{id:guid}")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Guid id, OrcamentoPessoalViewModel orcamentoPessoalViewModel)
            {
                if (id != orcamentoPessoalViewModel.Id)
                {
                    return NotFound();
                }

                var orcamentoAtualizacao = await ObterOrcamento(id);
                orcamentoPessoalViewModel.Usuario = orcamentoAtualizacao.Usuario;
            //if (!ModelState.IsValid) return View(orcamentoPessoalViewModel);
                orcamentoAtualizacao.Ano = orcamentoPessoalViewModel.Ano;
                orcamentoAtualizacao.Mes = orcamentoPessoalViewModel.Mes;
                orcamentoAtualizacao.Dia = orcamentoPessoalViewModel.Dia;
                orcamentoAtualizacao.TipoDespesa = orcamentoPessoalViewModel.TipoDespesa;
                orcamentoAtualizacao.Descricao = orcamentoPessoalViewModel.Descricao;
                orcamentoAtualizacao.Valor = orcamentoPessoalViewModel.Valor;
                orcamentoAtualizacao.DataAtualizacao = DateTime.Now;
                orcamentoAtualizacao.Ativo = orcamentoPessoalViewModel.Ativo;


            await _orcamentoPessoalService.Atualizar(_mapper.Map<OrcamentoPessoal>(orcamentoAtualizacao));

                if (!OperacaoValida()) return View(orcamentoPessoalViewModel);

                return RedirectToAction(nameof(Index));

            }

            // GET: Produtos/Delete/5
            //[ClaimsAuthorize("Orcamento", "Excluir")]
            [Route("excluir-orçamento/{id:guid}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var orcamento = await ObterOrcamento(id);

                if (orcamento == null)
                {
                    return NotFound();
                }

                return View(orcamento);
            }

            // POST: Produtos/Delete/5
           // [ClaimsAuthorize("Orcamento", "Excluir")]
            [Route("excluir-orçamento/{id:guid}")]
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                var orcamento = await ObterOrcamento(id);

                if (orcamento == null)
                {
                    return NotFound();
                }

                await _orcamentoPessoalService.Remover(id);

                if (!OperacaoValida()) return View(orcamento);

                TempData["Sucesso"] = "Produto excluido com sucesso!";

                return RedirectToAction("index");
            }

            private async Task<OrcamentoPessoalViewModel> ObterOrcamento(Guid id)
            {
                var orcamento = _mapper.Map<OrcamentoPessoalViewModel>(await _orcamentoPessoalRepository.ListarPorId(id));
                orcamento.Usuarios = _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos());
                return orcamento;
            }

            private async Task<OrcamentoPessoalViewModel> PopularUsuarios(OrcamentoPessoalViewModel orcamento)
            {

                orcamento.Usuarios = _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos());
                return orcamento;
            }

        }
    }
