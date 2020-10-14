﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Van.TalentPool.Users
{
    public class UserActiveConfirmation : DefaultUserConfirmation<User>
    {
        public override Task<bool> IsConfirmedAsync(UserManager<User> manager, User user)
        {
            return Task.FromResult(user.Confirmed);
        }
    }
}
