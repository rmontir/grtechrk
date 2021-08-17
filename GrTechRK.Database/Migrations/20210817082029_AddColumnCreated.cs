using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrTechRK.Database.Migrations
{
    public partial class AddColumnCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38e4c6d8-a44b-4b63-829a-d5f307c487a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "531d8189-eb22-45f3-ad61-e3961087b633", "AQAAAAEAACcQAAAAEBVQW+hCYCVb+ApugGO/HVM96/oNjPQEIBEq3segTMzU62ci9nNwcwffEkKZG4x5jQ==", "51ef82f1-5bac-400a-8222-c96fcdbb1492" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e082f561-367e-47f6-8724-371d7ad3d1d7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b715eb4e-9d5f-4453-9104-7bd910b04853", "AQAAAAEAACcQAAAAEEZ9F2gysc0TCLGPo6EqGr9R6uwwspBsJJZzybeO0S2mj5q+arqVzmA3VnAsk9xC/w==", "ec83ff63-cf27-4fa0-9876-0bc50674a6a1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38e4c6d8-a44b-4b63-829a-d5f307c487a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "609c4720-f86b-43b7-88fe-5e833b23ed1b", "AQAAAAEAACcQAAAAEK2+GxFx6dbXNMxxR38E9DlRvqL2MWY0inkoamBnqLLbW8ZJabQP4ioHiCWc/AgX7A==", "9e37fb15-8af3-40b1-88b4-b490288d948a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e082f561-367e-47f6-8724-371d7ad3d1d7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1be3cf5b-dc71-4476-948f-959f15df5ef5", "AQAAAAEAACcQAAAAEOnIY17JhWAiO70K2Xw5hvqBY5dMMdxHRShf4TIV8HNX325efEuyQzUq8wLuAJIPrQ==", "f493b003-67e8-4675-b9fe-403b2d7e5f2a" });
        }
    }
}
