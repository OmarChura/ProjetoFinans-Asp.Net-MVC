using AutoMapper;
using DevOC.App.ViewModels;
using DevOC.Business.Models;

namespace DevOC.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<OrcamentoPessoal, OrcamentoPessoalViewModel>().ReverseMap();
        }
    }
}
