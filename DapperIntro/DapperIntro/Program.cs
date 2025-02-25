#region Part1 

// Basic ADO.NET query example 
/*
using System.Data;
using Microsoft.Data.SqlClient;

var connectionString =
    "Data Source=localhost; Initial Catalog=Auth_3; User Id=sa; Password=Elvin123; Trust Server Certificate=true;";

var sqlQuery = "select * from Users"; // В реальных проектах комманды хранят в .sql файлах

using var connection = new SqlConnection(connectionString);
using var command = new SqlCommand(sqlQuery, connection);

connection.Open();

SqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    // Console.WriteLine(reader["userName"] + "\t" + reader["email"]);
    Console.WriteLine(reader.GetString(0) + reader.GetString(1));
}
*/

#endregion

#region ConnectionString

// ConnectionString - это строка, которая содержит информацию о том, как подключиться к базе данных.

// По хорошему держать строку подключения в коде нельзя, из-за этого я добавлю его в конфигурационный файл
// Все что будет ниже не имеет значения для работы с Dapper,
// давайте установим пакет Microsoft.Extensions.Configuration.Json


// using Dapper;
// using DapperIntro;
// using Microsoft.Data.SqlClient;
// using Microsoft.Extensions.Configuration;
//
// var configBuilder = new ConfigurationBuilder();
// configBuilder.AddJsonFile("appsettings.json");
//
// var config = configBuilder.Build();
//
// var connectionString = config.GetConnectionString("Default");

#endregion


#region Part2

// // Basic Dapper singleColumn example 
//
// using var connection = new SqlConnection(connectionString);
// var sqlQuery = "select userName from Users";
//
// connection.Open();
//
// var users = connection.Query<string>(sqlQuery);
//
// foreach (var username in users)
// {
//     Console.WriteLine(username);
// }

#endregion

#region Part3

/*
// Basic Dapper multiColumn example 

using var connection = new SqlConnection(connectionString);
var sqlQuery = "select * from Users";

connection.Open();

var users = connection.Query<User>(sqlQuery);

foreach (var user in users)
{
    Console.WriteLine(user);
}
*/
#endregion

#region Part4

// Dapper multiquery example
/*
using var connection = new SqlConnection(connectionString);

var sqlQuery = "select * from Users where userName = 'alice_smith';" +
    "select * from UserRoles where userNameRef = 'alice_smith';";

connection.Open();

using var multiQuery = connection.QueryMultiple(sqlQuery);

var user = multiQuery.ReadSingle<User>();

var userRoles = multiQuery.Read<UserRole>();

Console.WriteLine($"Here are the roles for {user.UserName}");

foreach (var role in userRoles)
{
    Console.WriteLine(role.roleNameRef);
}
*/
#endregion

#region Part5

// Query parametrization and select projection 
// Правило №1. Если тебе нужна одна колонка, бери только ее. * - это зло
// Правило №2. Не передавай переменные в команду напрямую, используй параметры

// using var connection = new SqlConnection(connectionString);
//
//
// var sqlQuery = $"select userName from Users where userName = @userName;" +
//                "select roleNameRef from UserRoles where userNameRef = @userName;";
//
// connection.Open();
//
// using var multiQuery = connection.QueryMultiple(sqlQuery, new {userName = "alice_smith"});
//
// var user = multiQuery.ReadSingle<User>();
//
// var userRoles = multiQuery.Read<UserRole>();
//
// Console.WriteLine($"Here are the roles for {user.UserName}");
//
// foreach (var role in userRoles)
// {
//     Console.WriteLine(role.roleNameRef);
// }
//
#endregion

#region Part6

// Getting scalar value

// using var connection = new SqlConnection(connectionString);
//
// var sqlQuery = "select count(*) from Users";
//
// connection.Open();
//
// int count = connection.QuerySingle<int>(sqlQuery);
//
// Console.WriteLine($"There are {count} users in the database");
//

#endregion

#region Part7

// ExecuteNonQuery 
//
// using var connection = new SqlConnection(connectionString);
//
// connection.Open();
//
// var sqlQuery = "insert into Roles (roleName) values (N'Editor')";
//
// var sqlCommand = new SqlCommand(sqlQuery, connection);
//
// var rowsAffected = sqlCommand.ExecuteNonQuery();
//
// Console.WriteLine($"{rowsAffected} rows affected...");


#endregion

#region Part8

// Execute Non-Query with Dapper

// using var connection = new SqlConnection(connectionString);
//
// connection.Open();
//
// var sqlQuery = "insert into Roles (roleName) values (N'Moderator')";
//
// var rowsAffected = connection.Execute(sqlQuery);
//
// Console.WriteLine($"{rowsAffected} rows affected...");
//
#endregion

// Все примеры ниже этого участка работают с базой данных Ecommerce_3 

#region ConnectionString

using Dapper;
using DapperIntro.Models.Ecommerce;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var connectionString = config.GetConnectionString("Ecommerce");


#endregion

#region Part9

// Relationships in Dapper 
// Recursive relationships
//
// using var connection = new SqlConnection(connectionString);
//
// connection.Open();
//
// var sqlQuery = """
//                SELECT C.Name, PC.Name
//                FROM Categories AS C
//                INNER JOIN Categories AS PC ON C.ParentCategoryID = PC.CategoryID
//                WHERE C.ParentCategoryID IS NOT NULL
//                """;
//
//
//
// var categories = connection.Query<Category, Category, Category>(sqlQuery, (category, parentcategory) =>
// {
//     category.ParentCategory = parentcategory;
//     return category;
// }, splitOn: "Name");
//
// foreach (var category in categories)
// {
//     Console.WriteLine($"{category.Name} - {category.ParentCategory.Name}"); 
// }

#endregion

#region Part10

// One toh many relationships

using var connection = new SqlConnection(connectionString);

var sqlQuery = """
               select p.Name, c.Name from ProductCategories
               inner join dbo.Products P on P.ProductID = ProductCategories.ProductID
               inner join dbo.Categories C on C.CategoryID = ProductCategories.CategoryID; 
               """;

Product foo(Product product, Category category)
{
    product.Categories.Add(category);
    return product;
}

var productCategories = connection.Query<Product, Category, Product>(
    sqlQuery, (product, category) =>
    {
        product.Categories.Add(category);
        return product;
        
    } , splitOn: "Name"
);

foreach (var product in productCategories)
{
    Console.WriteLine($"{product.Name}");
    foreach (var category in product.Categories)
    {
        Console.WriteLine($"\t{category.Name}");
    }
}



#endregion