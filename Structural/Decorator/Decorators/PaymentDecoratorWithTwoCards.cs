class PaymentDecoratorWithTwoCards : PaymentDecorator
{
    private List<IPayment> _paymentTypes = new();
    public PaymentDecoratorWithTwoCards(IPayment payment) : base(payment)
    {
       
    }

    public PaymentDecoratorWithTwoCards(IEnumerable<IPayment>? payments = null)
    {
        if (payments != null)
        {
            _paymentTypes.AddRange(payments);
        }
    }

    public override void Pay(int amout)
    {
        foreach (var payment in _paymentTypes)
        {
            payment.Pay(amout);
        }
    }
}