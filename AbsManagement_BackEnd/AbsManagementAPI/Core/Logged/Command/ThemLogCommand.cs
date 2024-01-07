using AbsManagementAPI.Core.CQRS.Logged;
using MediatR;

namespace AbsManagementAPI.Core.Logged.Command
{
    public class ThemLogCommand : IRequest<string>
    {
        public ThemLogModel ThemLogModel { get; set; }
    }
}
