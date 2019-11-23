using System.Collections.Generic;
using System.Security.Claims;

namespace WebShopSOA.Domain.DTO.Identity
{
    public abstract class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
