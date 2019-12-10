using System.ComponentModel.DataAnnotations;

namespace ShoppingV2.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}