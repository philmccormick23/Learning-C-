using System.ComponentModel.DataAnnotations;

namespace DojoSurvey.Models
{
    public class FormData
    {
        public string Name { get; set; }


        public string Location { get; set; }


        public string Language { get; set; }

        public string Comment { get; set; }
    }
}