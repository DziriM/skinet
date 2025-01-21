using System.Linq.Expressions;

namespace Core.Specifications;

public interface ISpecification<T>
{
    // To speciify like the Where clause
    Expression<Func<T, bool>> Criteria { get; }
    
    // To specify like the Include clause
    List<Expression<Func<T, object>>> Includes { get; }
}