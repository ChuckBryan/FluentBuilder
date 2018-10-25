namespace SimplyBuildIt
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Builder<TBuild> where TBuild : class

    {
        private readonly TBuild _objectToBuild;


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
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (value == null) throw new ArgumentNullException(nameof(value));

            // Reference type property or field
            var propertyName = _objectToBuild.GetMemberName(expression);

            if (!string.IsNullOrWhiteSpace(propertyName))
                SetPropertyName(propertyName, value);

            return this;
        }

        private void SetPropertyName(string propertyName, object value)
        {

            typeof(TBuild).GetTypeInfo().DeclaredProperties.First(x => x.Name == propertyName)
                .SetValue(_objectToBuild, value, null);
        }

    }
}