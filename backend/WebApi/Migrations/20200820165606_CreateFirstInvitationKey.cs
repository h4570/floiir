using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApi.Migrations
{
    public partial class CreateFirstInvitationKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.Now;
            var createdAt = $"{now.Year}-{now.Month}-{now.Day} {now.Hour}:{now.Minute}:{now.Second}";
            migrationBuilder
                .Sql($"INSERT INTO public.\"InvitationKeys\"(\"Key\", \"InviterId\", \"CreatedAt\") " +
                $"VALUES('1234567890', (SELECT \"Id\" FROM public.\"Users\" WHERE \"FirstName\" = 'Administrator' " +
                $"AND \"LastName\" = 'Floiir' ), '{createdAt}');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM public.\"InvitationKeys\" WHERE \"Key\" = '1234567890';");
        }
    }
}
