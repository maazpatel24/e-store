﻿using DAL.Models.Api;
using System.Net;

namespace API.Helpers.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (AccessViolationException avEx)
			{
				_logger.LogError($"A new violation exception has been thrown: {avEx}");
				await HandleExceptionAsync(httpContext, avEx);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var message = exception switch
			{
				AccessViolationException => "Access violation error from the custom middleware",
				_ => "Internal Server Error from the custom middleware."
			};

			await context.Response.WriteAsync(
				new ApiResult(false, null, 
					new ErrorResult(context.Response.StatusCode, message)
				).ToString()
			).ConfigureAwait(false);
		}
	}
}
