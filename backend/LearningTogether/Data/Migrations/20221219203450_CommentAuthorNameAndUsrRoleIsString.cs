using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningTogether.Migrations
{
    public partial class CommentAuthorNameAndUsrRoleIsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Course_CourseID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_AuthorID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Category_CategoryID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_User_CreatorID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Course_CourseID",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_User_UserID",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Unit_UnitID",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Course_CourseID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Category_CategoryID",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_User_UserID",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Course_CourseID",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "PhoneNum",
                table: "User",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Unit",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                table: "Unit",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_CourseID",
                table: "Unit",
                newName: "IX_Unit_CourseId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Subscription",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Subscription",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "SubscriptionID",
                table: "Subscription",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_UserID",
                table: "Subscription",
                newName: "IX_Subscription_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_CategoryID",
                table: "Subscription",
                newName: "IX_Subscription_CategoryId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Rating",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Rating",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserID",
                table: "Rating",
                newName: "IX_Rating_UserId");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                table: "Material",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "MaterialID",
                table: "Material",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_UnitID",
                table: "Material",
                newName: "IX_Material_UnitId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Enrollment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Enrollment",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "EnrollmentID",
                table: "Enrollment",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_UserID",
                table: "Enrollment",
                newName: "IX_Enrollment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_CourseID",
                table: "Enrollment",
                newName: "IX_Enrollment_CourseId");

            migrationBuilder.RenameColumn(
                name: "CreatorID",
                table: "Course",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Course",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Course",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CreatorID",
                table: "Course",
                newName: "IX_Course_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CategoryID",
                table: "Course",
                newName: "IX_Course_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Comment",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Comment",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Comment",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CourseID",
                table: "Comment",
                newName: "IX_Comment_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorID",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "CatImg",
                table: "Category",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Course_CourseId",
                table: "Comment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Category_CategoryId",
                table: "Course",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_User_CreatorId",
                table: "Course",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Course_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_User_UserId",
                table: "Enrollment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Unit_UnitId",
                table: "Material",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Course_CourseId",
                table: "Rating",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Category_CategoryId",
                table: "Subscription",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_UserId",
                table: "Subscription",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Course_CourseId",
                table: "Unit",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Course_CourseId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Category_CategoryId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_User_CreatorId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Course_CourseId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_User_UserId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Unit_UnitId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Course_CourseId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Category_CategoryId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_User_UserId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Course_CourseId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CatImg",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "User",
                newName: "PhoneNum");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Unit",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Unit",
                newName: "UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_CourseId",
                table: "Unit",
                newName: "IX_Unit_CourseID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Subscription",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Subscription",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Subscription",
                newName: "SubscriptionID");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                newName: "IX_Subscription_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_CategoryId",
                table: "Subscription",
                newName: "IX_Subscription_CategoryID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rating",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Rating",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserId",
                table: "Rating",
                newName: "IX_Rating_UserID");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Material",
                newName: "UnitID");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "Material",
                newName: "MaterialID");

            migrationBuilder.RenameIndex(
                name: "IX_Material_UnitId",
                table: "Material",
                newName: "IX_Material_UnitID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enrollment",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Enrollment",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollment",
                newName: "EnrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment",
                newName: "IX_Enrollment_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment",
                newName: "IX_Enrollment_CourseID");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Course",
                newName: "CreatorID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Course",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Course",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CreatorId",
                table: "Course",
                newName: "IX_Course_CreatorID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CategoryId",
                table: "Course",
                newName: "IX_Course_CategoryID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Comment",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comment",
                newName: "AuthorID");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comment",
                newName: "CommentID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CourseId",
                table: "Comment",
                newName: "IX_Comment_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                newName: "IX_Comment_AuthorID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "CategoryID");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Course_CourseID",
                table: "Comment",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_AuthorID",
                table: "Comment",
                column: "AuthorID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Category_CategoryID",
                table: "Course",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_User_CreatorID",
                table: "Course",
                column: "CreatorID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Course_CourseID",
                table: "Enrollment",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_User_UserID",
                table: "Enrollment",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Unit_UnitID",
                table: "Material",
                column: "UnitID",
                principalTable: "Unit",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Course_CourseID",
                table: "Rating",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserID",
                table: "Rating",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Category_CategoryID",
                table: "Subscription",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_UserID",
                table: "Subscription",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Course_CourseID",
                table: "Unit",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
