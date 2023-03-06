using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coreNotify.Migrations
{
    public partial class UserRoles : Migration
    {
        private string ManagerRoleId = Guid.NewGuid().ToString();
        private string UserRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();
        private string AdminUserId = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleId}', 'Administrator', 'ADMINISTRATOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{ManagerRoleId}', 'Manager', 'MANAGER', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{UserRoleId}', 'User', 'USER', null);");

            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], 
                [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
                VALUES 
                (N'{AdminUserId}', N'First', N'Last', N'test@a.tld2', N'TEST@A.TLD2', 
                N'test@a.tld2', N'TEST@A.TLD2', 0, 
                N'AQAAAAIAAYagAAAAEIaUs9lFkf3+a6hvRjeKGoPdWGn/g+n23omRmOIPm9FGXq/k7ja7E2P4JhFRH+ptbQ==', 
                N'YUPAFWNGZI2UC5FOITC7PX5J7XZTAZAA', N'8e150555-a20d-4610-93ff-49c5af44f749', N'6191fb25-99d1-4c47-837f-04d0ada85be7', 0, 0, NULL, 1, 0)");
            migrationBuilder.Sql(@$"
                INSERT INTO [dbo].[AspNetUserRoles]
                   ([UserId]
                   ,[RoleId])
                VALUES
                   ('{AdminUserId}', '{AdminRoleId}');
                INSERT INTO [dbo].[AspNetUserRoles]
                   ([UserId]
                   ,[RoleId])
                VALUES
                   ('{AdminUserId}', '{UserRoleId}');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
