namespace FluentBuilder.Tests
{
    using System;
    using System.Linq.Expressions;
    using EnsureThat;

    public class Builder<TBuild> where TBuild : class

    {
        private readonly TBuild _objectToBuild;

        private readonly string _invalidExpressionError =
            "Invalid Expression Type. Fluent Builder only supports properties";


        public Builder()
        {
            var typeToBuild = typeof(TBuild);

            _objectToBuild = (TBuild) Activator.CreateInstance(typeToBuild, true);
        }

        public TBuild Build()
        {
            return _objectToBuild;
        }

        public Builder<TBuild> WithProperty(Expression<Func<TBuild, object>> expression, object value)
        {
            Ensure.That(expression).IsNotNull();
            Ensure.That(value).IsNotNull();



            // Reference type property or field
            var propertyName = _objectToBuild.GetMemberName(expression);

            if (!string.IsNullOrWhiteSpace(propertyName))
                SetPropertyName(propertyName, value);

            return this;
        }

        private void SetPropertyName(string propertyName, object value)
        {
            typeof(TBuild).GetProperty(propertyName)
                .SetValue(_objectToBuild, value, null);
        }


/*        private string GetMemberName(Expression expression)
        {
            Ensure.That(expression).IsNotNull();

            if (!(expression is MemberExpression)) throw new InvalidOperationException(_invalidExpressionError);

            var memberExpression = (MemberExpression) expression;

            return memberExpression.Member.Name;
        }*/
    }
}