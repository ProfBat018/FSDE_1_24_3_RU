class Program
{
    public static void Main(string[] args)
    {
        var taxCalculator = new TaxCalculator();
        
        var taxes = new List<Tax>
        {
            new Tax { TaxName = "DVX", TaxAmount = 0.20f, TaxCountAction = (salary) => salary * 0.20f },
            new Tax { TaxName = "DSMF", TaxAmount = 84f, TaxCountAction = (salary) => 84f },
            new Tax { TaxName = "ITSDA", TaxAmount = 14f, TaxCountAction = (salary) => 14f }
        };

        var res = taxCalculator.CalculateTax(1000, taxes);
        
    }
}

