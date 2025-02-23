using Azure.Identity;

namespace DapperIntro.Models.Ecommerce;

public class Category
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Данный объект нужен для того, чтобы взять родительскую категорию
    // после выпоолнения нужного join запроса
    public Category ParentCategory { get; set; }
}       