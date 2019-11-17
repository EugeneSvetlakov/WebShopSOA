using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.Interfaces.Services
{
    public interface IUsersClient :
        IUserRoleStore<User>,
        IUserClaimStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>,
        IUserLoginStore<User>,
        IUserLockoutStore<User>
    {
    }
}
