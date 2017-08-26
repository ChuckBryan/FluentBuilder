# FluentBuilder
This is a simple Fluent Builder used for creating instances of Entities during Testing. Most of my Entities have private Ctor, Private Setters and Factory Methods. This led to a lot of noise when creating objects to return from Mocks. Originally, I created a bunch of builders, and the usage pattern was mostly the same.

# Usage

```csharp
var employer = new Builder<Employer>()
    .WithProperty(m => m.Name, "St. Mercy")
    .Build();

var person = new Builder<Person>()
    .WithProperty(x => x.FirstName, "Bob")
    .WithProperty(x => x.LastName, "Kelso")
    .WithProperty(x => x.EmployerId, employer.Id)
    .WithProperty(x => x.Employer, employer)
    .Build();
```                
