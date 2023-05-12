using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Genre
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Genre name must be max 20 symbols.")]
        public string Name { get; set; }

        #endregion

        #region Navigation

        public List<Game> Games { get; set; }

        public List<User> Users { get; set; }

        #endregion

        private Genre()
        {
            Games = new();
            Users = new();
        }

        public Genre(string name)
            : this()
        {
            Name = name;
        }
    }
}
