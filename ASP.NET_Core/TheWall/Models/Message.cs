using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string MessageText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; } //one to many with user
        public User User { get; set; }

        public List<Comment> Comments { get; set; } //many to many with user

        }
}