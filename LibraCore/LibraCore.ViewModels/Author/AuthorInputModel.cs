using System.ComponentModel.DataAnnotations;

using static LibraCore.GCommon.EntityValidation.Author;

namespace LibraCore.ViewModels.Author
{
    public class AuthorInputModel
    {
        [Required(ErrorMessage = "Author name is required.")]
        [MinLength(AuthorNameMinLength)]
        [MaxLength(AuthorNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
