namespace AbsManagementAPI.Core.Entities
{
    public class LogEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Controller { get; set; }

        public string Method { get; set; }

        public string FunctionName { get; set; }
        public string Status { get; set; }

        public string OleValue { get; set; }

        public string NewValue { get; set; }

        public string Type { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
