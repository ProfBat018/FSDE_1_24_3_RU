BitcoinPayment bitcoinPayment = new();
MasterCardPayment masterCardPayment = new();
VisaPayment visaPayment = new();


PaymentDecoratorWithTwoCards pwt = new(new List<IPayment>() { bitcoinPayment, masterCardPayment, visaPayment });

pwt.Pay(100);

// // Decorator
// PaymentDecoratorWithTwoCards paymentDecoratorWithTwoCards = new(new List<IPayment> () {bitcoinPayment, masterCardPayment, visaPayment});
// //paymentDecoratorWithTwoCards.Pay(100);
//
//
// PaymentDecoratorWithTax paymentDecoratorWithTax = new(bitcoinPayment);
//
// paymentDecoratorWithTax.Pay(100);


