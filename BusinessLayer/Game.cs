using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Game
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Game title must be max 20 symbols.")]
        public string Title { get; set; }

        #endregion

        #region Navigation

        public List<User> Users { get; set; }

        public List<Genre> Genres { get; set; }

        #endregion

        private Game()
        {
            Users = new();
            Genres = new();
        }

        public Game(string title)
            : this()
        {
            Title = title;
        }
    }
}
