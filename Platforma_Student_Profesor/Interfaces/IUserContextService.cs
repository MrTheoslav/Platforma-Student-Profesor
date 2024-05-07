using System.Security.Claims;

namespace API.Interfaces
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}