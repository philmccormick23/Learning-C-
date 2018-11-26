using System.ComponentModel.DataAnnotations;

namespace DojoSurveyValidation.Models
{
    public class FormData
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Language { get; set; }

        [MaxLength(20)]
        public string Comment { get; set; }
    }
}