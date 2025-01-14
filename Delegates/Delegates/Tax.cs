
class Tax
{
    public string TaxName { get; set; }    
    public float TaxAmount { get; set; }

    public TaxDelegate TaxCountAction { get; init; }
}



