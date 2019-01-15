using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using Microsoft.AspNetCore.Http;

namespace JacobMarshallTafeFinalProject.Services
{
	public static class UrlExtension
	{
		public static string PathQuery(this HttpRequest request) =>
			request.QueryString.HasValue ? $"{request.Path}{request.QueryString}"
			: request.Path.ToString();
	}
}
