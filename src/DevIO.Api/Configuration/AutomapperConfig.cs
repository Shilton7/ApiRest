using AutoMapper;
using DevIO.Api.ViewsModels;
using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();

            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, 
                           opt => opt.MapFrom(src => src.Fornecedor.Nome));

            CreateMap<ProdutoImagemViewModel, Produto>().ReverseMap();

        }
    }
}
