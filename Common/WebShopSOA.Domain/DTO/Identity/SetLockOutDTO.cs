using System;

namespace WebShopSOA.Domain.DTO.Identity
{
    public class SetLockOutDTO : UserDTO
    {
        public DateTimeOffset? LockOutEnd { get; set; }
    }
}
