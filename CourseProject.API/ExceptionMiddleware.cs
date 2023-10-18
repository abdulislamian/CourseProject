using CourseProject.Buisness.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CourseProject.API
{
    public class ExceptionMiddleware
    {
        private RequestDelegate Next { get; }
        public ExceptionMiddleware(RequestDelegate _next)
        {
            Next = _next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (TeamNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Team for id {ex.id} not found",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch (EmployeeNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Employee for id {ex.id} not found",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch (JobNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Job for id {ex.id} not found",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch (AddressNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Address for id {ex.id} not found",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch (DependentEmployeesExistException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Dependent Employee {JsonSerializer.Serialize(ex.Employees.Select(u => u.Id))} exist.",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch (EmployeesNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Employee {JsonSerializer.Serialize(ex.ints)} not Found.",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonSerializer.Serialize(ex.Errors),
                    Instance = "",
                    Title = "Validation Error",
                    Type = "Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
            catch(Exception ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance="",
                    Title = "Somewent Wrong - Internal Server Error",
                    Type   ="Error"
                };

                var problemDetailsJSON = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJSON);
            }
        }
    }
}
