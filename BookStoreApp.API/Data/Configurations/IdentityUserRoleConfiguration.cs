using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreApp.API.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "D2591BF1-96EC-4D9D-BD41-5BB9F6D3EC1A",
                    UserId = "C15ADE04-7357-457B-B529-A8851A5A73CD"
                }
                ,
                new IdentityUserRole<string>
                {
                    RoleId = "06094DFD-7EEB-4D1E-9950-D7D3E6422C0C",
                    UserId = "585696FB-C3F4-4219-AF7C-F37588219FF9"
                }
                );           
                
        }
    }
}
