using DecentReads.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace DecentReads.Api.Middleware
{
    public class ErrorHandlingMiddleware 
    {
        
            private readonly RequestDelegate requestDelegate;

            public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
            {
                this.requestDelegate = requestDelegate;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await requestDelegate(context);
                }
                catch (ForbidException forbidException)
                {
                    context.Response.StatusCode = 403;
                }
                //catch (DomainException domainException)
                //{
                //    context.Response.StatusCode = 400;
                //    await context.Response.WriteAsync(domainException.Message);
                //}
                catch (BadRequestException badRequestException)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(badRequestException.Message);
                }

            catch (NotFoundException notFoundException)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(notFoundException.Message);
                }

                //catch (Exception ex)
                //{
                //    await HandleExceptionAsync(context, ex);
                //}
                catch (Exception e)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Something went wrong.");
                }
            }

            public static Task HandleExceptionAsync(HttpContext context, Exception ex)
            {
                var code = HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new { error = ex.Message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
            }


        }


    }
