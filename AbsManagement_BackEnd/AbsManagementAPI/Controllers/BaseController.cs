using MediatR;

namespace AbsManagementAPI.Controllers
{
    public class BaseController
    {
        public readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }
}
