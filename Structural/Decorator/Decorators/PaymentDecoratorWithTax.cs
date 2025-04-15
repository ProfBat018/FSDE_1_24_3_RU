class PaymentDecoratorWithTax : PaymentDecorator
{
    private readonly IPayment _payment;

    public PaymentDecoratorWithTax(IPayment payment) : base(payment)
    {
        _payment = payment;
    }

    public override void Pay(int amout)
    {
        Console.WriteLine($"Paying {amout * 1.1} using tax");
        _payment.Pay(amout);
    }
}