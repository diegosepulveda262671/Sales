using System;
using System.Net;

namespace Sales.WEB.repositories
{
	public class HttpResponseWrapper<T>
	{
		public HttpResponseWrapper(T? response,bool error, HttpResponseMessage httpResponseMessage)
		{
			Error = error;
			Response = response;
			HttpResponseMessage = httpResponseMessage;
		}

		public bool Error { get; set; }

		public T? Response { get; set; }

		public HttpResponseMessage HttpResponseMessage { get; set; }

		public async Task<string?> GetErrorMessageAsync()
		{
			if (!Error)
			{
				return null;
			}

		
			switch (HttpResponseMessage.StatusCode)
			{
				case HttpStatusCode.NotFound :
					return "Recurso no encontrado";
					
				case HttpStatusCode.BadRequest:
					return await HttpResponseMessage.Content.ReadAsStringAsync();

				case HttpStatusCode.Unauthorized:
					return "Tienes que iniciar sesión para hacer esta operación";

				case HttpStatusCode.Forbidden:
					return "No tienes permisos para hacer esta operación";

				default:
					return "ha ocurrido un error inesperado";

			}
		}

	}
}

