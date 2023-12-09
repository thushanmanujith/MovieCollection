using System.ComponentModel;
using System.Net;

namespace MovieCollection.Core.Extensions
{
    public static class ErrorCodesExtensions
    {
        public static string ToDescription(this Enum enumeration)
        {
            var attribute = GetAttribute<DescriptionAttribute>(enumeration);
            return attribute.Description;
        }

        public static T GetAttribute<T>(Enum enumeration) where T : Attribute
        {
            var type = enumeration.GetType();

            var memberInfo = type.GetMember(enumeration.ToString());

            if (!memberInfo.Any())
                throw new ArgumentException($"No public members for the argument '{enumeration}'.");

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (attributes == null || attributes.Length != 1 || attributes.Single() is not T attribute)
                throw new ArgumentException($"Can't find an attribute matching '{typeof(T).Name}' for the argument '{enumeration}'");

            return attribute;
        }

        public static HttpStatusCode ToHttpStatusCode(this Enum enumeration)
        {
            var attribute = GetAttribute<HttpStatusCodeAttribute>(enumeration);
            return attribute.HttpStatusCode;
        }
    }
}
