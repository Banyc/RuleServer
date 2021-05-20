using System.Reflection;
namespace RuleEngine.Tests.Extensions
{
    public static class TestExtensions
    {
        public static TProperty GetProperty<TSource, TProperty>(this TSource source, string propertyName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            PropertyInfo propertyInfo = typeof(TSource).GetProperty(propertyName, bindFlags);
            return (TProperty)propertyInfo.GetValue(source);
        }

        public static TField GetField<TSource, TField>(this TSource source, string fieldName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo fieldInfo = typeof(TSource).GetField(fieldName, bindFlags);
            return (TField)fieldInfo.GetValue(source);
        }
    }
}
