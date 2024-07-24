using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    public void ReturnErrorCnpjIsInvalid()
    {
        var doc = new Document("123",EDocumentType.CNPJ);
        Assert.IsFalse(doc.IsValid);
    }
    
    
    [TestMethod]
    public void ReturnSucessCnpjIsValid()
    {
        var doc = new Document("84175237000101",EDocumentType.CNPJ);
        Assert.IsTrue(doc.IsValid);
    }
    
    [TestMethod]
    public void ReturnErrorCpfIsInvalid()
    {
        var doc = new Document("123",EDocumentType.CPF);
        Assert.IsFalse(doc.IsValid);
    }
    [TestMethod]
    [DataTestMethod]
    [DataRow("05849011005")]
    [DataRow("49466735047")]
    [DataRow("12668494087")]
    public void ReturnSucessCpfIsValid(string cpf)
    {
        var doc = new Document(cpf,EDocumentType.CPF);
        Assert.IsTrue(doc.IsValid);
    }
}