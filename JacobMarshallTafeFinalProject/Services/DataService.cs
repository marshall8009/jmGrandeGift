using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JacobMarshallTafeFinalProject.Models;
//...
using Microsoft.EntityFrameworkCore;

namespace JacobMarshallTafeFinalProject.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
		//fields
		private MyDbContext _context;
		private DbSet<T> _dbSet;

		//constructor
		public DataService(MyDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		//methods
		public IEnumerable<T> GetAll()
		{
			return _dbSet.ToList();
		}

		public void Create(T entity)
		{
			_dbSet.Add(entity);
			_context.SaveChanges();
		}

		public T GetSingle(Func<T, bool> predicate)
		{
			return _context.Set<T>().FirstOrDefault(predicate);
		}

		public IEnumerable<T> Query(Func<T, bool> predicate)
		{
			return _context.Set<T>().Where(predicate);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
			_context.SaveChanges();
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
			_context.SaveChanges();
		}

		public void Find(T entity)
		{
			_dbSet.Find(entity);
			_context.SaveChanges();
		}

		public IQueryable<T> GetQuery()
		{
			return _dbSet;
		}
    }

	
}
