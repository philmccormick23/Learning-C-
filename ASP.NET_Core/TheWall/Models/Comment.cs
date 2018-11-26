using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int MessageId { get; set; } //one to many with Message
        public Message Message { get; set; }

        public int UserId { get; set; } //one to many with User
        public User User { get; set; }

        }
}