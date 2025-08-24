using Domain.Interface;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Domain.Specification;
public class BaseSpecification<T> : ISpecification<T>
{
    private readonly Expression<Func<T, bool>> _criteria;
    protected BaseSpecification() : this(null) { }

    public BaseSpecification(Expression<Func<T,bool>> criteria)
    {
        _criteria = criteria;
    }
    public Expression<Func<T, bool>> criteria => _criteria;

    public Expression<Func<T, object>>? orderBy { get; private set; }

    public Expression<Func<T, object>>? orderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    protected void AddOrderBy(Expression<Func<T,object>>? orderBySpecification)
    {
        orderBy = orderBySpecification;
    }

    protected void AddOrderByDescending(Expression<Func<T,object>>? orderByDescSpecification)
    {
        orderByDescending = orderByDescSpecification;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    } 
}


public class  BaseSpecification<T,TResult>(Expression<Func<T, bool>> criteria) : BaseSpecification<T>(criteria), ISpecification<T,TResult>
{
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected BaseSpecification() : this(null) { }

    protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
    {
        Select  = selectExpression;
    }
}

