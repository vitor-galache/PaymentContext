using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentsTests
{
  private readonly Name _name;
  private readonly Document _document;
  private readonly Address _address;
  private readonly Email _email;
  private readonly Student _student;

  public StudentsTests()
  {
    _name = new Name("Bruce", "Wayne");
    _document = new Document("35111507795", EDocumentType.CPF);
    _email = new Email("batman@dc.com");
    _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "13400000");
    _student = new Student(_name, _document, _email);            
  }
  
  [TestMethod]
  public void ReturnErrorSubscriptionNoHavePayment()
  {
    var subscription = new Subscription(null);
    _student.AddSubscription(subscription);
    Assert.IsFalse(_student.IsValid);
  }
  
  [TestMethod]
  public void ReturnErrorSubscriptionActive()
  {
    var subscription = new Subscription(null);
    var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5),10,10,_document,"WAYNE CORP",_address,"code_transaction",_email );
    subscription.AddPayment(payment);
    _student.AddSubscription(subscription);
    _student.AddSubscription(subscription);
    Assert.IsFalse(_student.IsValid);
  }
  
  [TestMethod]
  public void ReturnSuccessIfSubscriptionInactive()
  {
    var subscription = new Subscription(null);
    var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5),10,10,_document,"WAYNE CORP",_address,"code_transaction",_email );
    subscription.AddPayment(payment);
    _student.AddSubscription(subscription);
    Assert.IsTrue(_student.IsValid);
  }
}