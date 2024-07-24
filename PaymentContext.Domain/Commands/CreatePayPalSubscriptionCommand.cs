using PaymentContext.Domain.Enums;

namespace PaymentContext.Domain.Commands;

public class CreatePayPalSubscriptionCommand
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Document { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string TransactionCode { get; set; } = null!;
    public string PaymentNumber { get; set; } = null!;
    public DateTime PaidDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPaid { get; set; }
    public string Payer { get; set; } = null!;
    public string PayerDocument { get; set; } = null!;
    public EDocumentType PayerDocumentType { get; set; }
    public string PayerEmail { get; set; } = null!;

    public string Street { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Neighborhood { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
}