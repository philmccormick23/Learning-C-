using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models
{
    public class LoginUser
    {
        
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [EmailAddress]
        public string loginemail { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string loginpw { get; set; }


        }
}