using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.Commons.Utils
{
    public static class LinQUtils
    {
        public static IQueryable<TEntity> DynamicFilter<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (entity.GetType().GetProperty(item.Name) == null) continue;
                var propertyVal = entity.GetType().GetProperty(item.Name).GetValue(entity, null);
                if (propertyVal == null) continue;
                if (item.CustomAttributes.Any(a => a.AttributeType == typeof(SkipAttribute))) continue;
                bool isDateTime = item.PropertyType == typeof(DateTime);
                if (isDateTime)
                {
                    DateTime dt = (DateTime)propertyVal;
                    source = source.Where($"{item.Name} >= @0 && {item.Name} < @1", dt.Date, dt.Date.AddDays(1));
                }
                else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ContainAttribute)))
                {
                    var array = (IList)propertyVal;
                    source = source.Where($"{item.Name}.Any(a=> @0.Contains(a))", array);
                    //source = source.Where($"{item.Name}.Intersect({array}).Any()",);
                }
                else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ChildAttribute)))
                {
                    var childProperties = item.PropertyType.GetProperties();
                    foreach (var childProperty in childProperties)
                    {
                        var childPropertyVal = propertyVal.GetType().GetProperty(childProperty.Name)
                            .GetValue(propertyVal, null);
                        if (childPropertyVal != null && childProperty.PropertyType.Name.ToLower() == "string")
                            source = source.Where($"{item.Name}.{childProperty.Name} = \"{childPropertyVal}\"");
                    }
                }
                else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ExcludeAttribute)))
                {
                    var childProperties = item.PropertyType.GetProperties();
                    var field = item.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ExcludeAttribute))
                        .NamedArguments.FirstOrDefault().TypedValue.Value;
                    var array = ((List<int>)propertyVal).Cast<int?>();
                    source = source.Where($"!@0.Contains(it.{field})", array);

                }
                else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(StringAttribute)))
                {
                    source = source.Where($"{item.Name}.ToLower().Contains(@0)", propertyVal.ToString().ToLower());
                }
                else if (item.PropertyType == typeof(string))
                {
                    source = source.Where($"{item.Name} = \"{((string)propertyVal).Trim()}\""); ;
                }
                else
                {
                    source = source.Where($"{item.Name} = {propertyVal}");
                    //source = source.Where();
                }
            }
            return source;
        }
        public static (int, IQueryable<TResult>) PagingIQueryable<TResult>(this IQueryable<TResult> source, int page, int size,
           int limitPaging, int defaultPaging)
        {
            if (size > limitPaging)
            {
                size = limitPaging;
            }
            if (size < 1)
            {
                size = defaultPaging;
            }
            if (page < 1)
            {
                page = 1;
            }
            int total = source.Count();
            IQueryable<TResult> results = source
                .Skip((page - 1) * size)
                .Take(size);
            return (total, results);
        }
        public static string ToDynamicSelector<TEntity>(this string[] selectorArray)
        {
            var selectors = selectorArray.Where(a => !string.IsNullOrEmpty(a)).Select(s => s.SnackCaseToLower()).ToList();
            var entityProperties = typeof(TEntity).GetProperties().Select(s => s.Name.SnackCaseToLower()).ToArray();
            entityProperties = entityProperties.Where(w => selectors.Contains(w)).ToArray();
            return @"new {" + string.Join(',', entityProperties) + "}";
        }

        public static string ToDynamicSelector(this string[] selectorArray)
        {
            return @"new {" + string.Join(',', selectorArray) + "}";
        }
        public static string SnackCaseToLower(this string o)
          => o.Contains("-") ? string.Join(string.Empty, o.Split("-")).Trim().ToLower() : string.Join(string.Empty, o.Split("_")).Trim().ToLower();

    }
}
