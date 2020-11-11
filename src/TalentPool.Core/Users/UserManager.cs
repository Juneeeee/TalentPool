﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TalentPool.Users
{
    public class UserManager : UserManager<User>
    {
        private readonly IUserStore _userStore;
        private readonly ITokenProvider _tokenProvider;
        public UserManager(IUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            ITokenProvider tokenProvider)
            : base(store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
        {
            _userStore = store;
            _tokenProvider = tokenProvider;
        }
        protected override CancellationToken CancellationToken => _tokenProvider.Token;

        public async Task<IdentityResult> UpdateRolesAsync(User user, List<string> roles)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));
            // 移除旧的角色数据
            var oldRoles = await GetRolesAsync(user);
            if (oldRoles != null)
            {
                foreach (var role in oldRoles)
                {
                    await _userStore.RemoveFromRoleAsync(user, role, CancellationToken);
                }
            }
            // 增加指定的角色数据
            foreach (var role in roles)
            {
                await _userStore.AddToRoleAsync(user, role, CancellationToken);
            }
            return await UpdateUserAsync(user);

        } 
    }
}