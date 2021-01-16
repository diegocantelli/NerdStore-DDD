using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Domain
{
    //A interface IProdutoRespositorio implementa os métodos de IRepository, onde o tipo passado como parâmetro
    //para a classe obrigatoriamente deve ser do tipo agregateRoot
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}