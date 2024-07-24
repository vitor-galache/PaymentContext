using System.Diagnostics.Contracts;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using Contract = Flunt.Validations.Contract;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(
            new Contract().Requires()
            .HasMinLen(FirstName, 
                3, "Name.FirstName", 
                "O nome deve conter no minimo 3 caracteres")
            .HasMinLen(LastName,3,"Name.LastName","O sobrenome deve conter no mínimo 3 caracteres")
            .HasMaxLen(FirstName,
                150,
                "Name.FirstName",
                "O nome deve conter no máximo 150 caracteres")
        );
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}