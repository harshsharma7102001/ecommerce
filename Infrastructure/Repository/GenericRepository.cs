using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.Repository;

public class GenericRepository<T> : IGenericInterface<T> where T: BaseEntity
{
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }
    public void Add(T entity)
    {
        _context.Set<T>().AddAsync(entity);
        _context.SaveChangesAsync();
    }

    public bool checkExists(int id)
    {
        return _context.Set<T>().Any(res => res.Id == id);
    }

    public bool Delete(T entity)
    {
        if (checkExists(entity.Id))
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync().Result > 0;
        }
        return false;
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    

    public async Task<T> GetById(int id)
    {
        if (checkExists(id))
        {
            return await _context.Set<T>().FirstOrDefaultAsync(res => res.Id == id);
        }
        return null;
    }

    public bool SaveChangesAsync()
    {
        return _context.SaveChangesAsync().Result > 0;
    }

    public bool Update(T entity)
    {
        if (checkExists(entity.Id))
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync().Result > 0;
        }
        return false;
    }

    public async Task<T?> GetBySpecification(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecification(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    // Creating a helper method for specification
    public IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }

    public IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T,TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery<T,TResult>(_context.Set<T>().AsQueryable(), spec);
    }

    public async Task<TResult> GetBySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllWithSpecification<TResult>(ISpecification<T,TResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }
}
