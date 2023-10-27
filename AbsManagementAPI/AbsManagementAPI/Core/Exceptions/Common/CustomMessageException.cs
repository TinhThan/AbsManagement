namespace AbsManagementAPI.Core.Exceptions.Common
{
    public class CustomMessageException : CustomException
    {
        public CustomMessageException(string message) : base(message)
        {
        }

        public CustomMessageException(string title, string detail, object description) : base(title, detail, description)
        {
        }

        public CustomMessageException(string title, string detail) : base(title, detail)
        {
        }
    }
}
