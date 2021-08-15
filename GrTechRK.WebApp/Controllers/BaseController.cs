using GrTechRK.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;

namespace GrTechRK.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
		protected DtoResponse Response_Ok()
		{
			return new DtoResponse<object>(default!);
		}

		protected DtoResponse<TResult> Response_Ok<TResult>()
		{
			return new DtoResponse<TResult>(default!);
		}

		protected DtoResponse<TResult> Response_Ok<TResult>(TResult result)
		{
			return new DtoResponse<TResult>(result);
		}

		protected DtoResponse Response_Exception(Exception exc)
		{
			return ToDtoResponse<object>(exc);
		}

		protected DtoResponse<TResult> Response_Exception<TResult>(Exception exc)
		{
			return ToDtoResponse<TResult>(exc);
		}

		protected DtoResponse<TResult> Response_Exception<TResult>(IEnumerable<string> errors)
		{
			Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return new DtoResponse<TResult>(default!, errors);
		}

		private DtoResponse<TResult> ToDtoResponse<TResult>(Exception exc)
        {
			switch(exc)
            {
				case InvalidOperationException _:
				case ArgumentException _:
					Response.StatusCode = (int)HttpStatusCode.BadRequest;
					return new DtoResponse<TResult>(default!, ImmutableHashSet.Create(exc.Message));
				default:
					Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					return new DtoResponse<TResult>(default!, ImmutableHashSet.Create("Internal server error"));
			}
        }
	}
}
