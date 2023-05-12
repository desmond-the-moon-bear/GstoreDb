using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class GameContext : IContext<Game, int>
    {
        private readonly GstoreDbContext dbContext;

        public GameContext(GstoreDbContext dbContext)
            => this.dbContext = dbContext;

        public void Create(Game item)
        {
            try
            {
                List<Genre> genres = new();
                Genre genreFromDb = null;

                foreach(Genre genre in item.Genres)
                {
                    genreFromDb = dbContext.Genres.Find(genre.Id);
                    if (genreFromDb != null) genres.Add(genreFromDb);
                    else genres.Add(genre);
                }
                item.Genres = genres;

                List<User> users = new();
                User userFromDb = null;

                foreach (User user in item.Users)
                {
                    userFromDb = dbContext.Users.Find(user.Id);
                    if (userFromDb != null) users.Add(userFromDb);
                    else users.Add(user);
                }
                item.Users = users;

                dbContext.Games.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public Game Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Game> query = dbContext.Games;

                if (useNavigationalProperties)
                {
                    query = query.Include(g => g.Genres)
                        .Include(p => p.Users);
                }

                return query.FirstOrDefault(g => g.Id == key);
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Game> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Game> query = dbContext.Games;

                if (useNavigationalProperties)
                {
                    query = query.Include(g => g.Genres)
                        .Include(p => p.Users);
                }

                return query.ToList();
            }
            catch (Exception) { throw; }
        }

        public void Update(Game item, bool useNavigationalProperties = false)
        {
            try
            {
                Game gameFromDb = dbContext.Games.Find(item.Id);

                if(gameFromDb == null)
                {
                    Create(item);
                    return;
                }

                gameFromDb.Title = item.Title;

                if(!useNavigationalProperties)
                {
                    dbContext.SaveChanges();
                    return;
                }

                List<Genre> genres = new();
                Genre genreFromDb = null;

                foreach (Genre genre in item.Genres)
                {
                    genreFromDb = dbContext.Genres.Find(genre.Id);
                    if (genreFromDb != null) genres.Add(genreFromDb);
                    else genres.Add(genre);
                }
                gameFromDb.Genres = genres;

                List<User> users = new();
                User userFromDb = null;

                foreach (User user in item.Users)
                {
                    userFromDb = dbContext.Users.Find(user.Id);
                    if (userFromDb != null) users.Add(userFromDb);
                    else users.Add(user);
                }
                gameFromDb.Users = users;

                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public void Delete(int key)
        {
            try
            {
                Game gameFromDb = Read(key) ??
                    throw new InvalidOperationException(
                    "Game with the given key does not exist");

                dbContext.Games.Remove(gameFromDb);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }
    }
}
