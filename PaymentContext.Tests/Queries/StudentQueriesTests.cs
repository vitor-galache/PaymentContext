using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries;

[TestClass]
public class StudentQueriesTests
{
    private IList<Student> _students;

    public StudentQueriesTests()
    {
        _students = new List<Student>();
        for (var i = 0; i <= 10; i++)
        {
            _students.Add(new Student(
                new Name("Aluno", i.ToString()),
                new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                new Email(i.ToString() + "@balta.io")
            ));
        }
    }
    
    
    [TestMethod]
    public void ReturnErrorIfDocumentNotExists()
    {
        var exp = StudentQueries.GetStudent("12345678911");
        var studn = _students.AsQueryable().Where(exp).FirstOrDefault();
        Assert.AreEqual(null,studn);
    }
    
    [TestMethod]
    public void ReturnSuccessIfDocumentExists()
    {
        var exp = StudentQueries.GetStudent("11111111111");
        var studn = _students.AsQueryable().Where(exp).FirstOrDefault();
        Assert.AreNotEqual(null,studn);
    }
}