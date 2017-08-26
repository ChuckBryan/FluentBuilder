# SimplyBuildIt
SimplyBuildIt is a utility designed to help you quickly building out instances of your entities to use in Tests. Especially, when those Entities have private constructors, private setters, factory methods. If that's your object AND you don't want to go through all of the ceremony you've created to protect your invariants just for a Unit Test, then SimplyBuildIt can Help.

## Download from NuGet
```csharp
Install-Package SimplyBuildIt -Version 1.0.0
```

## Example
The following Employee class is an example of the kinds of classes that SimplyBuildIt was made for:
```csharp
public class Person
{
    private  Person()
    {
        Id = Guid.NewGuid();
    }

    public static Person Create(string firstName, string lastName,  Employer employer)
    {
        Ensure.That(employer).IsNotNull();
        var person = new Person();

        person.ChangeName(firstName, lastName);
        person.Employer = employer;
        person.EmployerId = employer.Id;
        return person;
    }

    public Guid Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid EmployerId { get; private set; }
    public Employer Employer { get; private set; }

    public void ChangeName(string firstName, string lastName)
    {
        Ensure.That(firstName).IsNotNullOrWhiteSpace();
        Ensure.That(lastName).IsNotNullOrWhiteSpace();

        FirstName = firstName;
        LastName = lastName;
    }
}
```
Because the constructor is private and forces construction through a factory method, it can get complicated to build. For example, if you wanted to use a Person in a test, you would need to create the Employer as well, event if Employer doesn't affect the test. Newing up objects creates a lot of ceremony. 

SimplyBuildIt lets you bypass your object creation methods and builds the object.

```csharp
var person = new Builder<Person>()
    .WithProperty(x => x.FirstName, "Bob")
    .WithProperty(x => x.LastName, "Kelso")
    .Build();
```                
If the object that you want to build isn't complex, then go a head and use the standard Object Initializer syntax. But, if it fairly complex to create, use SimplyBuildIt.
