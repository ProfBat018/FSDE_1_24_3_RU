

using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;

public class Sale
{
    public int SaleId { get; set; }
    
    public Salesman SalesmanRef { get; set; }
    public Car CarRef { get; set; }
    
    public string SaleDate { get; set; }
    public int SalePrice { get; set; }

    public int SalesmanBadgeId { get; set; }
    public int CarId { get; set; }
}