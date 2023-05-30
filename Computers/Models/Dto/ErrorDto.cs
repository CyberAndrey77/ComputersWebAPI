using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;

namespace Computers.Models.Dto
{
    public class ErrorDto : AnswerDto
    {
        public ErrorDto(string message) : base(message, HttpStatusCode.BadRequest) { }
        public ErrorDto(string message, HttpStatusCode statusCode) : base (message, statusCode) { }
    }
}
