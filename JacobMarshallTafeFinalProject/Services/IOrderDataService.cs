using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JacobMarshallTafeFinalProject.Models;

namespace JacobMarshallTafeFinalProject.Services
{
	public interface IOrderDataService
	{
		IQueryable<Order> Orders { get; }
		void SaveOrder(Order order);
	}
}
