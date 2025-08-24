using System.Linq.Expressions;
namespace Domain.Interface;

public interface ISpecification<T>
{
    Expression<Func<T,bool>> criteria { get; }

    Expression<Func<T,object>>? orderBy { get; }

    Expression<Func<T,object>>? orderByDescending { get; }

    bool IsDistinct { get; }
}

public interface ISpecification<T,TResult>:ISpecification<T>
{
   Expression<Func<T,TResult>>? Select { get; }
}
