using AbsManagementAPI.Core.Constants;
using FluentValidation.Results;

namespace AbsManagementAPI.Core.Exceptions.Common
{
    public class CustomValidateException : CustomException
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

        public CustomValidateException(string message) : base(message)
        {
        }

        public CustomValidateException(IEnumerable<ValidationFailure> failures) : base(MessageSystem.DATA_INVALID)
        {
            var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                Errors.Add(failureGroup.Key, failureGroup.Distinct().ToArray());
            }
        }
    }
}
