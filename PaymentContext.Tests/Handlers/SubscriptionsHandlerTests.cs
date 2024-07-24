using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionsHandlerTests
{
    private readonly SubscriptionHandler _handler;
    private readonly CreateBoletoSubscriptionCommand _command;

    public SubscriptionsHandlerTests()
    {
        _handler = new SubscriptionHandler(new FakeStudentRepository(),new FakeEmailService());
        _command = new CreateBoletoSubscriptionCommand();
        _command.FirstName = "Bruce";
        _command.LastName = "Wayne";
        _command.Document = "99999999999";
        _command.Email = "hello@balta.io2";
        _command.BarCode = "123456789";
        _command.BoletoNumber = "1234654987";
        _command.PaymentNumber = "123121";
        _command.PaidDate = DateTime.Now;
        _command.ExpireDate = DateTime.Now.AddMonths(1);
        _command.Total = 60;
        _command.TotalPaid = 60;
        _command.Payer = "WAYNE CORP";
        _command.PayerDocument = "12345678911";
        _command.PayerDocumentType = EDocumentType.CPF;
        _command.PayerEmail = "batman@dc.com";
        _command.Street = "asdas";
        _command.Number = "asdd";
        _command.Neighborhood = "asdasd";
        _command.City = "as";
        _command.State = "as";
        _command.Country = "as";
        _command.ZipCode = "12345678";
    }
    [TestMethod]
    public void ReturnErrorIfDocumentExists()
    {
        _handler.Handle(_command);
        Assert.AreEqual(false, _handler.Valid);
    }

    [TestMethod]
    public void ReturnErrorIfEmailExists()
    {
        _command.Document = "99999999996";
        _command.Email = "hello@balta.io";
        _handler.Handle(_command);
        Assert.AreEqual(false, _handler.Valid);
    }
}