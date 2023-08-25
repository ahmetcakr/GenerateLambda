using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateLambda.Conditions
{
    public static class DynamicLambdaFunctions
    {
        public static Func<ExpandoObject, string> CreateGroupByKeySelector(string propertyName)
        {
            return item =>
            {
                var expandoDict = (IDictionary<string, object>)item;
                if (expandoDict.ContainsKey(propertyName))
                {
                    return expandoDict[propertyName]?.ToString();
                }
                return null;
            };
        }
    }
}
