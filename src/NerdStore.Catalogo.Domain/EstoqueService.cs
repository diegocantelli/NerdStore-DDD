using NerdStore.Core.Bus;
using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Events;

namespace NerdStore.Catalogo.Domain
{
    //EstoqueService é um serviço de domínio, utilizado quando é necessário implementar funcionalidades
    //nas quais as entidades não conseguem resolver por si só
    //Os métodos definidos dentro de um serviço de domínio DEVEM REPRESENTAR as ações da LINGUAGEM UBÍQUA
    //A IMPLEMENTAÇÂO assim como sua INTERFACE do serviço de domínio fica dentro da camada DOMAIN
    //Ex: -> Debitar um estoque
    // 1 -> Buscar um produto no BD através do repositório que irá devolver uma instância deste produto
    // 2 -> Com a instância deste produto será feita a operação de debitar estoque, operação esta já definida na 
    //      entidade de produto
    // 3 -> Após feito estes passos, o produto com a nova quantidade será persistido no banco
    //Um serviço de domínio UNE várias operações definidas em classes distintas em um lugar só, para poder
    //executar uma atividade definida
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatrHandler _bus;

        public EstoqueService(IProdutoRepository produtoRepository, 
                              IMediatrHandler bus)
        {
            _produtoRepository = produtoRepository;
            _bus = bus;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            // TODO: Parametrizar a quantidade de estoque baixo
            if (produto.QuantidadeEstoque < 10)
            {
                await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}