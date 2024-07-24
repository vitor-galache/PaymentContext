using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;
    
    public SubscriptionHandler(IStudentRepository repository,IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    
    // Implementação para criar assinatura via Boleto
    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar o cadastro");
            
        }
        // Verificar se documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document","Este CPF já está em uso");
        // Verificar E-MAIL
        if (_repository.EmailExists(command.Email))
            AddNotification("Email","Este e-mail já está em uso");
        
        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City,command.State,command.Country,command.ZipCode);
        
        // Gerar entidades 
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            document,
            command.Payer,
            address,
            email,
            command.BarCode,
            command.BoletoNumber);
        
        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Validações de Value Objects e Entities 
        AddNotifications(name,document,email,address,student,subscription,payment);
        
        // Checar notifications
        if (Invalid)
            return new CommandResult(false,"Não foi possível concluir a realização da assinatura");
                    
        // Salvar informações
        _repository.CreateSubscription(student);
        
        // Envio de E-mail
        _emailService.Send(student.Name.ToString(),student.Email.Address,"Seja bem vindo, bons estudos","Sua assinatura foi criada");
        
        // Retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso!");
    }
        
    // Implementação para criar assinatura via PayPal
    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar o cadastro");
            
        }
        // Verificar se documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document","Este CPF já está em uso");
        // Verificar E-MAIL
        if (_repository.EmailExists(command.Email))
            AddNotification("Email","Este e-mail já está em uso");
        
        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City,command.State,command.Country,command.ZipCode);
        
        // Gerar entidades 
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(command.TransactionCode,command.PaidDate,command.ExpireDate,command.Total,command.TotalPaid,command.Payer,document,address,email);
        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Validações de Value Objects e Entities 
        AddNotifications(name,document,email,address,student,subscription,payment);
        
        // Salvar informações
        _repository.CreateSubscription(student);
        
        // Envio de E-mail
        _emailService.Send(student.Name.ToString(),student.Email.Address,"Seja bem vindo, bons estudos","Sua assinatura foi criada");
        
        // Retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso!");
    }
}