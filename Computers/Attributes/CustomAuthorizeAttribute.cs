using Computers.Models;

namespace Computers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : Attribute
    {
        public Role Role { get; private set; }

        public CustomAuthorizeAttribute(){}

        public CustomAuthorizeAttribute(Role role)
        {
            Role = role;
        }
    }
}
