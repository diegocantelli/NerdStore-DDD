using System;

namespace NerdStore.Core.DomainObjects
{
    //Trata-se de uma classe para lidar com erros de estado inválido nas entidades de domínio
    //Exemplo: não deixar que um produto seja criado sem ter informado o seu nome
    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message) : base(message)
        { }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}