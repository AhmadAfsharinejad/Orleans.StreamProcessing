namespace Workflow.Domain.Plugins.RandomGenerator;

[GenerateSerializer]
public enum RandomType
{
    Text,
    Number,
    DateTime,
    TimeSpan,
    Bool,
    Guid,
    Age,
    Name,
    LastName,
    Company,
    Address,
    PhoneNumber,
    Email,
    Gender,
    Ip,
    Account,
    MusicGenre,
    CreditCardNumber
}