using System;
using System.Collections.Generic;

namespace  WiLinq.LinqProvider.Extensions
{
    public static class WorkItemValueConverterRepository
    {
        private static readonly Dictionary<Type, WorkItemValueConverter> _converterDictionary = new Dictionary<Type, WorkItemValueConverter>();

        static WorkItemValueConverterRepository()
        {
            
            AddConverter(new ProjectToWorkItemValueConverter());
        }

        public static void AddConverter(WorkItemValueConverter converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter), "converter is null.");
            }

            if (_converterDictionary.ContainsKey(converter.RelatedType))
            {
                throw new InvalidOperationException("related type for converter already registered");
            }

            _converterDictionary.Add(converter.RelatedType, converter);
        }

        internal static WorkItemValueConverter GetConverter(Type type)
        {
            WorkItemValueConverter result;

            _converterDictionary.TryGetValue(type, out result);

            return result;
        }
    }

}
