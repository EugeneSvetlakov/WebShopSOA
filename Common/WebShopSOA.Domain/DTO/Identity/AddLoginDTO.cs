using Microsoft.AspNetCore.Identity;

namespace WebShopSOA.Domain.DTO.Identity
{
    public class AddLoginDTO : UserDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}
