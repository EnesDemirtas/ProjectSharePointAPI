using MediatR;
using PSP.Application.Models;

namespace PSP.Application.Identity.Commands; 

public class RemoveAccount : IRequest<OperationResult<bool>> {
    public Guid IdentityUserId { get; set; }
    public Guid RequestorGuid { get; set; }
}