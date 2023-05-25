using DevOC.App.Extensions;
using DevOC.App.ViewModels;
using DevOC.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevOC.App.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Sessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UsuarioViewModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonSerializer.Deserialize<UsuarioViewModel>(sessaoUsuario);
        }

        public void CriarSessaoUsuario(UsuarioViewModel usuario)
        {
            string valor = JsonSerializer.Serialize(usuario);
            _httpContextAccessor.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
