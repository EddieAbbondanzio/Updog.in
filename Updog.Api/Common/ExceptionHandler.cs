using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

/// <summary>
/// Middleware to automatically wrap an exception that occured but was not caught,
/// and to send back a message to the client.
/// </summary>
public class ExceptionHandler {
    private readonly RequestDelegate next;

    public ExceptionHandler(RequestDelegate next) {
        this.next = next;
    }

    public async Task Invoke(HttpContext context) {
        try {
            await next.Invoke(context);
        } catch (ValidationException ex) {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Message));
        } catch (Exception ex) {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject("Something went horribly wrong."));

            Console.WriteLine(ex.Message);
        }
    }
}