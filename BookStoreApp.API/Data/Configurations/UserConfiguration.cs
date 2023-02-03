using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreApp.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<APIUser>
    {
        public void Configure(EntityTypeBuilder<APIUser> builder)
        {
            var hasher = new PasswordHasher<APIUser>(); 
            builder.HasData(
                new APIUser
                {
                    Id = "585696FB-C3F4-4219-AF7C-F37588219FF9" ,
                    Email = "admin@bookapp.com",
                    NormalizedEmail = "ADMIN@BOOKAPP.COM",
                    UserName = "admin@bookapp.com",
                    NormalizedUserName = "ADMIN@BOOKAPP.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null,"P@ssw0rd")
                },
                 new APIUser
                 {
                     Id = "C15ADE04-7357-457B-B529-A8851A5A73CD",
                     Email = "user@bookapp.com",
                     NormalizedEmail = "USER@BOOKAPP.COM",
                     UserName = "user@bookapp.com",
                     NormalizedUserName = "USER@BOOKAPP.COM",
                     FirstName = "System",
                     LastName = "User",
                     PasswordHash = hasher.HashPassword(null, "P@ssw0rd")
                 }
                );      
        
        }
    }
}
