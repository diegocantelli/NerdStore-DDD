using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Data
{
    //Implementa um repositório de T, que implementa o Idisposable, onde o tipo T seja uma raiz de agregação
    //Com isso mantemos a regra de 1 repositório por agregação
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}