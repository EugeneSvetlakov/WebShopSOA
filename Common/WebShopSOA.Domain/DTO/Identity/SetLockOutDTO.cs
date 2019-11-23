using System;

namespace WebShopSOA.Domain.DTO.Identity
{
    public class SetLockoutDTO : UserDTO
    {
        public DateTimeOffset? LockOutEnd { get; set; }
    }
}
