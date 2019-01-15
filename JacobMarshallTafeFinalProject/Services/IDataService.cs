﻿using JacobMarshallTafeFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JacobMarshallTafeFinalProject.Services
{
    public interface IDataService<T>
    {
		IEnumerable<T> GetAll();
		void Create(T entity);
		T GetSingle(Func<T, bool> predicate);
		IEnumerable<T> Query(Func<T, bool> predicate);
		void Update(T entity);
		void Delete(T entity);
		void Find(T entity);
		IQueryable<T> GetQuery();

	}

	
}
