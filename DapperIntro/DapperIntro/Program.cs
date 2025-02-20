#region Part1 
/*
// Basic ADO.NET query example 

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


using Dapper;
using DapperIntro;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json");

var config = configBuilder.Build();

var connectionString = config.GetConnectionString("Default");

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