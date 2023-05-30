using System.Net;

namespace Computers.Models.Dto
{
    public class LoginAnswerDto : AnswerDto
    {
        public Guid Guid { get; set; }

        public LoginAnswerDto(Guid guid, string message) : base(message)
        {
            Guid = guid;
        }

        public LoginAnswerDto(Guid guid, string message, HttpStatusCode statusCode) : this(guid, message)
        {
            StatusCode = statusCode;
        }
    }
}
