using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JacobMarshallTafeFinalProject.Models
{
	public class Cart
	{
		private List<CartLine> list = new List<CartLine>();
		

		public virtual void AddItem(Product product, int quantity)
		{
			CartLine line = list.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();

			if (line == null)
			{
				list.Add(new CartLine {
					Product = product,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public virtual void RemoveLine(Product product)
		{
			list.RemoveAll(p => p.Product.ProductId == product.ProductId);
		}

		public virtual decimal TotalValue() =>
			list.Sum(p => p.Product.Price * p.Quantity);

		public virtual void Clear() => list.Clear();

		public virtual IEnumerable<CartLine> Lines => list;
	}

	public class CartLine
	{
		public int CartLineId { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
