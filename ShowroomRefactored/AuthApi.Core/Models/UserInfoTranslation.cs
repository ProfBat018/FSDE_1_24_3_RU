namespace AuthApi.Core.Models;

public class UserInfoTranslations
{
    public string TranslationId { get; set; }
    public string UserId { get; set; }
    public string LanguageCode { get; set; }
    public string TranslatedInfo { get; set; }
    public User User { get; set; }
    
}