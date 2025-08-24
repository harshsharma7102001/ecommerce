using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Interface;
public interface IGenericInterface<T> where T:BaseEntity
{
    Task<IReadOnlyList<T?>> GetAll();
    Task<T?> GetById(int id);

    Task<T?> GetBySpecification(ISpecification<T> spec);

    Task<IReadOnlyList<T>> GetAllWithSpecification(ISpecification<T> spec);
    void Add(T entity);

    bool Update(T entity);

    bool Delete(T entity);

    bool checkExists(int id);

    bool SaveChangesAsync();
    Task<TResult> GetBySpecification<TResult>(ISpecification<T,TResult> spec);

    Task<IReadOnlyList<TResult>> GetAllWithSpecification<TResult>(ISpecification<T,TResult> spec);
}
