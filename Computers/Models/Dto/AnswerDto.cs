using System.Net;

namespace Computers.Models.Dto
{
    public class AnswerDto
    {
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public AnswerDto(string message)
        {
            Message = message;
            StatusCode = HttpStatusCode.OK;
        }

        public AnswerDto(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
