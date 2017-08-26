namespace SimplyBuildIt.Tests
{
    using System;
    using EnsureThat;

    public class Person
    {

        private  Person()
        {
            Id = Guid.NewGuid();
        }
        
        public static Person Create(string firstName, string lastName)
        {
            var person = new Person();

            person.ChangeName(firstName, lastName);

            return person;

        }

        public Guid Id { get; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Guid EmployerId { get; set; }

        public Employer Employer { get; set; }

        public void ChangeName(string firstName, string lastName)
        {

            Ensure.That(firstName).IsNotNullOrWhiteSpace();
            Ensure.That(lastName).IsNotNullOrWhiteSpace();

            FirstName = firstName;
            LastName = lastName;
        }


    }
}