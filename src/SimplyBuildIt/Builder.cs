namespace SimplyBuildIt
{
    using System;
    using System.Linq.Expressions;

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
            typeof(TBuild).GetProperty(propertyName)
                .SetValue(_objectToBuild, value, null);
        }

    }
}