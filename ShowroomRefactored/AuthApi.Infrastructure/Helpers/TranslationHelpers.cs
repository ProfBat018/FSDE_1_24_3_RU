using AuthApi.Core.Attributes;
using AuthApi.Core.Models;

namespace AuthApi.Infrastructure.Helpers;

using System.Globalization;
using System.Reflection;

public static class TranslationHelper
{
    public static bool IsTranslatable(PropertyInfo prop)
        => prop.GetCustomAttribute<TranslatableAttribute>() != null;


    public static string? GetTranslationForProperty(object entity, PropertyInfo property, string languageCode)
    {
        var attribute = property.GetCustomAttribute<TranslatableAttribute>();
        if (attribute == null) return null;

        var translationsCollectionName = property.Name + attribute.TranslationsCollectionSuffix;
        var collProp = entity.GetType().GetProperty(translationsCollectionName, BindingFlags.Public | BindingFlags.Instance);
        if (collProp == null) return null;

        var collValue = collProp.GetValue(entity) as System.Collections.IEnumerable;
        if (collValue == null) return null;

        foreach (var item in collValue)
        {
            if (item == null) continue;
            var itemType = item.GetType();

            var languageProp = itemType.GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, "LanguageCode", StringComparison.OrdinalIgnoreCase) 
                                     && p.PropertyType == typeof(string));
            if (languageProp == null) continue;

            var langVal = languageProp.GetValue(item) as string;
            if (string.IsNullOrEmpty(langVal)) continue;

            if (!IsLanguageMatch(langVal, languageCode)) continue;

            PropertyInfo? textProp = null;
            if (!string.IsNullOrEmpty(attribute.TranslationTextPropertyName))
            {
                textProp = itemType.GetProperty(attribute.TranslationTextPropertyName, BindingFlags.Public | BindingFlags.Instance);
            }
            else
            {
                textProp = itemType.GetProperties()
                    .Where(p => p.PropertyType == typeof(string))
                    .Where(p => !string.Equals(p.Name, languageProp.Name, StringComparison.OrdinalIgnoreCase))
                    .Where(p => !p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();
            }

            if (textProp == null) continue;

            var translated = textProp.GetValue(item) as string;
            if (!string.IsNullOrEmpty(translated))
                return translated;
        }

        return null;
    }

    static bool IsLanguageMatch(string foundLang, string desiredLang)
    {
        if (string.IsNullOrEmpty(foundLang) || string.IsNullOrEmpty(desiredLang)) return false;

        var f = foundLang.Split('-', '_')[0].ToLowerInvariant();
        var d = desiredLang.Split('-', '_')[0].ToLowerInvariant();
        return f == d;
    }

    public static void ApplyTranslations(object entity, string languageCode)
    {
        if (entity == null) return;

        var type = entity.GetType();
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(IsTranslatable);

        foreach (var prop in props)
        {
            if (prop.PropertyType != typeof(string) || !prop.CanWrite) continue;

            var translated = GetTranslationForProperty(entity, prop, languageCode);
            if (!string.IsNullOrEmpty(translated))
            {
                prop.SetValue(entity, translated);
            }
        }
    }

    public static void ApplyTranslationsToResult(object? resultObj, string languageCode)
    {
        if (resultObj == null) return;

        if (resultObj is System.Collections.IEnumerable enumerable && !(resultObj is string))
        {
            foreach (var item in enumerable)
            {
                if (item == null) continue;
                ApplyTranslations(item, languageCode);
            }
        }
        else
        {
            ApplyTranslations(resultObj, languageCode);
        }
    }
}
