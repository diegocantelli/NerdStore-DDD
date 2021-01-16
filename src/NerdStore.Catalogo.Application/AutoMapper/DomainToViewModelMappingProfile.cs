using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Application.AutoMapper
{
   //Configurando o automapper
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //irá copiar os membros de produto para produtoViewModel
            CreateMap<Produto, ProdutoViewModel>()
                //pelo fato da classe produto possuir a classe ValueObject Dimensao e a produtoViewModel não
                //possuir esta classe, pois implementa os campos de dimensão diretamente nela mesma
                //é necessário criar esse mapeamento adicional, que segue a ordem:
                //destino(ProdutoViewModel) -> fonte(produto)
                .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));

            CreateMap<Categoria, CategoriaViewModel>();
        }
    }
}
