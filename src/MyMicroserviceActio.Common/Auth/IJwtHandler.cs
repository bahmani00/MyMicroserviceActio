using System;

namespace MyMicroserviceActio.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);     
    }
}