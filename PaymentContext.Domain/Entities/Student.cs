using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    // private IList<string> _notifications;
    private IList<Subscription> _subscriptions { get; set; }
    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();
        AddNotifications(name,document,email);
    }

    public Name Name { get; set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address? Address { get; private set; } 

    public IReadOnlyCollection<Subscription> Subscriptions
    {
        get
        {
            return _subscriptions.ToArray();
        }
    }

    public void AddSubscription(Subscription subscription)
    {
        var hasSubscriptionActive = false;
        foreach (var sub in _subscriptions)
        {
            if (sub.Active)
                hasSubscriptionActive = true;
        }
        AddNotifications(new Contract<Student>().Requires()
            .IsFalse(hasSubscriptionActive,
                "StudentSubscriptions",
                "Você já tem uma assinatura ativa")
            .AreNotEquals(0,
                subscription.Payments.Count,
                "Student.Subscription.Payments",
                "Assinatura não possui pagamento"));
        if (IsValid)
            _subscriptions.Add(subscription);
        
        // if (hasSubscriptionActive)
        //     AddNotification("Student.Subscriptions","Você já possui uma assinatura ativa");
    }
}