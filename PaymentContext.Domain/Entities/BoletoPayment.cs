using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;


public class BoletoPayment : Payment
{
    public BoletoPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Document document,
        string payer, Address address,Email email, string barCode, string boletoNumber) : base(paidDate, expireDate, total,
        totalPaid, document, payer, address,email)
    {
        BarCode = barCode;
        BoletoNumber = boletoNumber;
    }
    
    public string BarCode { get; private set; }
    public string BoletoNumber { get; private set; }
}