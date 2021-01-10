using System;

namespace NerdStore.Core.DomainObjects
{
    //Todas as classes que forem uma entidade irão herdar desta classe
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        //É feito o override do método equals para verificar se uma entidade é igual a outra
        //Essa comparação será feita através do Id de cada objeto
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        //Foi feito o override do operador "==" para que as entidades que derivem de Entity possam ser comparadas
        // através do operador "=="
        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        //Pelo fato do operador "==" ter sido sobrescrito, é necessário sobrescrever o operador "!=" também
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        //Garante que dois objetos diferentes NÂO tenham o mesmo HashCode
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}