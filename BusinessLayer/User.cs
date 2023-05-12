using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class User
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "First name must be max 20 symbols.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Last name must be max 20 symbols.")]
        public string LastName { get; set; }

        [Range(10, 80)]
        public int Age { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Username must be max 20 symbols.")]
        public string Username { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Password must be max 70 symbols.")]
        public string Password { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Email must be max 20 symbols.")]
        public string Email { get; set; }

        #endregion

        #region Navigation

        public List<User> Friends { get; set; }
        
        public List<Game> Games { get; set; }

        public List<Genre> Genres { get; set; }

        #endregion

        private User()
        {
            Friends = new();
            Games = new();
        }

        public User(
            string firstName,
            string lastName,
            int age,
            string username,
            string password,
            string email)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
