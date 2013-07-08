using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace  WiLinq.LinqProvider.Extensions
{
    public static class WorkItemValueConverterRepository
    {
        private static Dictionary<Type, WorkItemValueConverter> _converterDictionary = new Dictionary<Type, WorkItemValueConverter>();

        static WorkItemValueConverterRepository()
        {
            
            AddConverter(new ProjectToWorkItemValueConverter());
        }

        public static void AddConverter(WorkItemValueConverter converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter", "converter is null.");
            }

            if (_converterDictionary.ContainsKey(converter.RelatedType))
            {
                throw new InvalidOperationException("related type for converter already registered");
            }

            _converterDictionary.Add(converter.RelatedType, converter);
        }

        internal static WorkItemValueConverter GetConverter(Type type)
        {
            WorkItemValueConverter result = null;

            _converterDictionary.TryGetValue(type, out result);

            return result;
        }
    }

}
