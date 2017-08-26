namespace FluentBuilder.Tests
{
    using System;
    using EnsureThat;

    public class Employer
    {
        private Employer()
        {
            Id = Guid.NewGuid();
        }

        public static Employer Create(string name)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();

            return new Employer {Name = name};
        }
    
        public Guid Id { get; private set; }

        public string Name { get; private set; }

    }
}