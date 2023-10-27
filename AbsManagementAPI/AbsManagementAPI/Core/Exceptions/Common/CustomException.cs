namespace AbsManagementAPI.Core.Exceptions.Common
{
    public class CustomException : Exception
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public object Description { get; set; }

        public CustomException(string message) : base(message)
        {

        }

        public CustomException(string title,string detail, object description) : base(title)
        {
            Title = title;
            Detail = detail;
            Description = description;
        }

        public CustomException(string title, string description) : base(title)
        {
            Title = title;
            Detail = description;
        }
    }
}
