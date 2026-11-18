using Microsoft.EntityFrameworkCore;
using ReadMoreBooks;
using ReadMoreBooks.Models;

using var context = new BookStoreContext();

context.Database.EnsureDeleted();
context.Database.EnsureCreated(); 

if (!context.Authors.Any())
{
    var author = new Author
    {
        FirstName = "Naguib",
        LastName = "Mahfouz",
        Email = "naguib@example.com",
        Biography = "Egyptian writer and Nobel Prize winner.",
        DateOfBirth = new DateTime(1911, 12, 11)
    };

    var category = new Category { Name = "Novels", Description = "Fiction and novels", IsActive = true };

    context.Books.Add(new Book
    {
        Title = "Palace Walk",
        ISBN = "9780385264662",
        Price = 250.50m,
        NumberOfPages = 498,
        YearPublished = 1956,
        IsInStock = true,
        Author = author,
        Category = category
    });

    context.SaveChanges();
}

foreach (var b in context.Books.Include(b => b.Author).Include(b => b.Category))
    Console.WriteLine($"{b.Title} | {b.Author.FullName} | {b.Category.Name} | {b.Price}");

Console.WriteLine("\n=== Schema ===");
var conn = context.Database.GetDbConnection();
conn.Open();
using var cmd = conn.CreateCommand();
cmd.CommandText = @"
    SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE,
           CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME IN ('Books','Authors','BookCategories')
    ORDER BY TABLE_NAME, ORDINAL_POSITION";

using var reader = cmd.ExecuteReader();
Console.WriteLine($"{"Table",-15}{"Column",-18}{"Type",-10}{"Len",-6}{"Prec",-6}{"Scale",-6}Nullable");
while (reader.Read())
    Console.WriteLine($"{reader[0],-15}{reader[1],-18}{reader[2],-10}{reader[3],-6}{reader[4],-6}{reader[5],-6}{reader[6]}");