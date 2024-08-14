using AutoMapper;
using DesafioTJRJ.Business.Entities;
using DesafioTJRJ.UI.ViewModels;

namespace DesafioTJRJ.UI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Assunto, AssuntoViewModel>().ReverseMap();
            CreateMap<Autor, AutorViewModel>().ReverseMap();
            CreateMap<FormaCompra, FormaCompraViewModel>().ReverseMap();

            CreateMap<LivroAutor, LivroAutorViewModel>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Autor.Nome))
                .ReverseMap()
                .ForMember(dest => dest.Autor, opt => opt.Ignore())
                .ForMember(dest => dest.Livro, opt => opt.Ignore());

            CreateMap<LivroAssunto, LivroAssuntoViewModel>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Assunto.Descricao))
                .ReverseMap()
                .ForMember(dest => dest.Assunto, opt => opt.Ignore())
                .ForMember(dest => dest.Livro, opt => opt.Ignore());

            CreateMap<LivroViewModel, Livro>()
                .ForMember(dest => dest.LivroAssuntos, opt => opt.MapFrom(src => src.Assuntos))
                .ForMember(dest => dest.LivroAutores, opt => opt.MapFrom(src => src.Autores))
                .ForMember(dest => dest.LivroPrecosFormaCompra, opt => opt.MapFrom(src => src.PrecosFormaCompra))
                .ReverseMap()
                .ForMember(dest => dest.Assuntos, opt => opt.MapFrom(src => src.LivroAssuntos))
                .ForMember(dest => dest.Autores, opt => opt.MapFrom(src => src.LivroAutores))
                .ForMember(dest => dest.PrecosFormaCompra, opt => opt.MapFrom(src => src.LivroPrecosFormaCompra));

            CreateMap<LivroPrecoFormaCompra, LivroPrecoFormaCompraViewModel>()
                .ForMember(dest => dest.FormaCompra, opt => opt.MapFrom(src => src.FormaCompra.Descricao))
                .ReverseMap()
                .ForMember(dest => dest.FormaCompra, opt => opt.Ignore())
                .ForMember(dest => dest.Livro, opt => opt.Ignore());
        }
    }
}