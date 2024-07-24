using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;


public class CreditCardPayment : Payment
{
    public CreditCardPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Document document,
        string payer, Address address,Email email, string cardHolderName, string cardNumber, string lastTransactionNumber) : base(
        paidDate, expireDate, total, totalPaid, document, payer, address,email)
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        LastTransactionNumber = lastTransactionNumber;
    }

    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string LastTransactionNumber { get; set; }
}