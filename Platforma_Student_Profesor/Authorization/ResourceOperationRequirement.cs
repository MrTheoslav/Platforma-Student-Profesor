using Microsoft.AspNetCore.Authorization;

namespace API.Authorization
{
        public enum ResourceOperation
    {
        Create,
        Read, 
        Update, 
        Delete
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }

        public ResourceOperation ResourceOperation { get; }
    }
}
