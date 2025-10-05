namespace AuthApi.Core.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class TranslatableAttribute : Attribute
{
    public string TranslationsCollectionSuffix { get; }
    public string? TranslationTextPropertyName { get; }

    public TranslatableAttribute(string translationsCollectionSuffix = "Translations", string? translationTextPropertyName = null)
    {
        TranslationsCollectionSuffix = translationsCollectionSuffix;
        TranslationTextPropertyName = translationTextPropertyName;
    }
}
