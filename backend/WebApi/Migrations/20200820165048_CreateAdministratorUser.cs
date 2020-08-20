using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class CreateAdministratorUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO public.\"Users\"(\"FirstName\", \"LastName\", \"Email\") " +
                "VALUES('Administrator', 'Floiir', 'administrator@floiir.com');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM public.\"Users\" WHERE \"FirstName\" = 'Administrator' AND \"LastName\" = 'Floiir';");
        }
    }
}
