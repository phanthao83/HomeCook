using Microsoft.EntityFrameworkCore.Migrations;

namespace HC.DataAccess.Migrations
{
    public partial class SP_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_SelectActiveProduct = @"
                CREATE PROCEDURE [dbo].[SelectActiveProduct] 
                @CategoryId int
                AS
                BEGIN
                Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                Category.Id as CategoryId, Category.Name as CategoryName, 
                  Unit.Id as UnitId , Unit.Name as UnitName, COALESCE( FileName, 'HomeCook.jpg') as FileName ,  COALESCE( Sum( OrderDetail.Quantity),0) as PQuantity
                from  Product 
                inner join Category on Product.CategoryId = Category.Id and Category.Id = @CategoryId
                inner join Unit on Product.UnitId = Unit.Id
                left join OrderDetail on OrderDetail.ProductId = Product.Id
                left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                where Product.Status = 'A' 
                Group by Product.ID ,  Product.Name , Product.Price , Category.Id, Category.Name, 
                  Unit.Id , Unit.Name, FileName
                END    ";
            migrationBuilder.Sql(sp_SelectActiveProduct);


            var sp_SelectNewProduct = @"
                CREATE PROCEDURE [dbo].[SelectNewProduct] 
                AS
                BEGIN
                Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                Category.Id as CategoryId, Category.Name as CategoryName, 
                  Unit.Id as UnitId , Unit.Name as UnitName, COALESCE( FileName, 'HomeCook.jpg') as FileName ,  0 as PQuantity
                from  Product 
                inner join Category on Product.CategoryId = Category.Id
                inner join Unit on Product.UnitId = Unit.Id
                left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                where Product.Status = 'A' 
                Order by Product.CreateDate DESC

                END
            
                ";
            migrationBuilder.Sql(sp_SelectNewProduct);

            var sp_SelectTop4NewProduct = @"CREATE PROCEDURE [dbo].[SelectTop4NewProduct] 
                    AS
                    BEGIN
                    Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                    Category.Id as CategoryId, Category.Name as CategoryName, 
                      Unit.Id as UnitId , Unit.Name as UnitName, COALESCE( FileName, 'HomeCook.jpg') as FileName ,  0 as PQuantity
                    from  Product 
                    inner join Category on Product.CategoryId = Category.Id
                    inner join Unit on Product.UnitId = Unit.Id
                    left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                    where Product.Status = 'A' 
                    Order by Product.CreateDate DESC

                    END";
             migrationBuilder.Sql(sp_SelectTop4NewProduct);


            var sp_SelectTopProduct = @"
                CREATE PROCEDURE [dbo].[SelectTopProduct] 
                AS
                BEGIN
                Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                Category.Id as CategoryId, Category.Name as CategoryName, 
                  Unit.Id as UnitId , Unit.Name as UnitName, COALESCE( FileName, 'HomeCook.jpg') as FileName ,  COALESCE( Sum( OrderDetail.Quantity),0) as PQuantity
                from  Product 
                inner join Category on Product.CategoryId = Category.Id
                inner join Unit on Product.UnitId = Unit.Id
                left join OrderDetail on OrderDetail.ProductId = Product.Id
                left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                where Product.Status = 'A'
                Group by Product.ID ,  Product.Name , Product.Price , Category.Id, Category.Name, 
                  Unit.Id , Unit.Name, FileName
                END";
            migrationBuilder.Sql(sp_SelectTopProduct);


            var sp_SelectTopSeller = @"
                CREATE PROCEDURE [dbo].[SelectTopSeller] 
                AS
                BEGIN
                SELECT Top 10 [AspNetUsers].ID , PhoneNumber
                      ,[AvartarUrl], [City] , [AspNetUsers].[Name] ,[State],
	                   count(Product.Id) as ProductQuanity
	  
                FROM [AspNetUsers]  left join Product  on Product.UserId = [AspNetUsers].Id  and Product.Status = 'A'
                where [AspNetUsers].Id  in (Select AspNetUserRoles.UserId
                                 from AspNetRoles, AspNetUserRoles
				                 where AspNetUserRoles.RoleId = AspNetRoles.Id and Name ='S')
                group by [AspNetUsers].ID , PhoneNumber
                      ,[AvartarUrl], [City] , [AspNetUsers].Name ,[State]
                END
                ";
            migrationBuilder.Sql(sp_SelectTopSeller);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
