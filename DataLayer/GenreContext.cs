using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class GenreContext : IContext<Genre, int>
    {
        private readonly GstoreDbContext dbContext;

        public GenreContext(GstoreDbContext dbContext)
            => this.dbContext = dbContext;

        public void Create(Genre item)
        {
            try
            {
                List<Game> games = new();
                Game gameFromDb = null;

                foreach (Game game in item.Games)
                {
                    gameFromDb = dbContext.Games.Find(game.Id);
                    if (gameFromDb != null) games.Add(gameFromDb);
                    else games.Add(game);
                }
                item.Games = games;

                List<User> users = new();
                User userFromDb = null;

                foreach (User user in item.Users)
                {
                    userFromDb = dbContext.Users.Find(user.Id);
                    if (userFromDb != null) users.Add(userFromDb);
                    else users.Add(user);
                }
                item.Users = users;

                dbContext.Genres.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public Genre Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Genre> query = dbContext.Genres;

                if (useNavigationalProperties)
                {
                    query = query.Include(g => g.Games)
                        .Include(p => p.Users);
                }

                return query.FirstOrDefault(g => g.Id == key);
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Genre> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Genre> query = dbContext.Genres;

                if (useNavigationalProperties)
                {
                    query = query.Include(g => g.Games)
                        .Include(p => p.Users);
                }

                return query.ToList();
            }
            catch (Exception) { throw; }
        }

        public void Update(Genre item, bool useNavigationalProperties = false)
        {
            try
            {
                Genre genreFromDb = dbContext.Genres.Find(item.Id);

                if (genreFromDb == null)
                {
                    Create(item);
                    return;
                }

                genreFromDb.Name = item.Name;

                if (!useNavigationalProperties)
                {
                    dbContext.SaveChanges();
                    return;
                }

                List<Game> games = new();
                Game gameFromDb = null;

                foreach (Game game in item.Games)
                {
                    gameFromDb = dbContext.Games.Find(game.Id);
                    if (gameFromDb != null) games.Add(gameFromDb);
                    else games.Add(game);
                }
                genreFromDb.Games = games;

                List<User> users = new();
                User userFromDb = null;

                foreach (User user in item.Users)
                {
                    userFromDb = dbContext.Users.Find(user.Id);
                    if (userFromDb != null) users.Add(userFromDb);
                    else users.Add(user);
                }
                genreFromDb.Users = users;

                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public void Delete(int key)
        {
            try
            {
                Genre genreFromDb = Read(key) ??
                    throw new InvalidOperationException(
                    "Game with the given key does not exist");

                dbContext.Genres.Remove(genreFromDb);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }
    }
}
