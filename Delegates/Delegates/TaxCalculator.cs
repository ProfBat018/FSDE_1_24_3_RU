class TaxCalculator
{
    public float CalculateTax(float salary, IEnumerable<Tax> taxes)
    {
        Console.WriteLine("Calculating tax...");
        float totalTax = 0;
        foreach (var tax in taxes)
        {
            Console.WriteLine($"Calculating tax for {tax.TaxName}...");
            totalTax += tax.TaxCountAction(salary);
            Console.WriteLine($"Tax for {tax.TaxName} is {tax.TaxCountAction(salary)}");
        }
        
        Console.WriteLine($"Total tax is {totalTax}");
        return totalTax;
    }
}