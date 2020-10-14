﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Van.TalentPool.EntityFrameworkCore.EntityTypeConfigurations
{
    class UserTokenEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
        {
            builder.ToTable("UserTokens");
        }
    }
}
