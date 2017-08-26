namespace FluentBuilder.Tests
{
    using Shouldly;
    using Xunit;

    public class When_Setting_Properties_With_Builder
    {
        [Fact]
        public void Then_Builder_Will_Create_Type_With_Set_Properties()
        {

            var employer = new Builder<Employer>()
                .WithProperty(m => m.Name, "St. Mercy")
                .Build();

            var person = new Builder<Person>()
                .WithProperty(x => x.FirstName, "Bob")
                .WithProperty(x => x.LastName, "Kelso")
                .WithProperty(x => x.EmployerId, employer.Id)
                .WithProperty(x => x.Employer, employer)
                .Build();
            

            person.ShouldNotBeNull();
            person.FirstName.ShouldBe("Bob");
            person.LastName.ShouldBe("Kelso");
            person.EmployerId.ShouldBe(employer.Id);
            person.Employer.ShouldBe(employer);
        }
    }
}