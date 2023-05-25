using DevOC.App.ViewModels;

namespace DevOC.App.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UsuarioViewModel usuario);
        void RemoverSessaoUsuario();
        UsuarioViewModel BuscarSessaoUsuario();
    }
}
