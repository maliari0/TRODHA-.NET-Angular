// TRODHA.Server/Controllers/BaseApiController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace TRODHA.Server.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new ApplicationException("Kullan�c� kimli�i bulunamad�");
                
            return int.Parse(userIdClaim.Value);
        }
    }
}
