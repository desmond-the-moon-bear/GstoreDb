using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using DataLayer;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace TestingLayer
{
    [TestFixture]
    class GameContextTest
    {
        private GameContext context = new(SetupFixture.dbContext);
        private Game game;
        private Genre genre1, genre2;
        private User user;

        [SetUp]
        public void SetUp()
        {
            game = new("Hollow Knight");
            genre1 = new("Metroidvania");
            genre2 = new("Souls-like");
            user = new("Plant", "Dude", 79, "willow44",
                "theskyispurplE", "oxygenproducer37@trees.org");
            
            game.Genres.Add(genre1);
            game.Genres.Add(genre2);
            game.Users.Add(user);

            context.Create(game);
        }

        [TearDown]
        public void TearDown()
        {
            foreach(Game game in SetupFixture.dbContext.Games.ToList())
            {
                SetupFixture.dbContext.Games.Remove(game);
            }
            SetupFixture.dbContext.SaveChanges();
        }

        [Test]
        public void CreateTest()
        {
            Game newGame = new("Blasphemous");

            int gamesBefore = SetupFixture.dbContext.Games.Count();
            context.Create(newGame);

            int gamesAfter = SetupFixture.dbContext.Games.Count();
            Assert.That(gamesAfter == gamesBefore + 1, "Create method does not work!");
        }

        [Test]
        public void Read()
        {
            Game readGame = context.Read(game.Id);

            Assert.AreEqual(readGame, game, "Read does not work!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            Game readGame = context.Read(game.Id, true);

            Assert.That(
                readGame.Genres.Contains(genre1) 
                && readGame.Genres.Contains(genre2)
                && readGame.Users.Contains(user),
                "ReadWithNavigationalProperties does not work!");
        }

        [Test]
        public void ReadAll()
        {
            List<Game> games = (List<Game>)context.ReadAll();

            Assert.That(games.Count != 0, "ReadAll method does not return games!");
        }

        [Test]
        public void Update()
        {
            Game changedGame = context.Read(game.Id);

            const string newTitle = "Hollow Knight: Silksong";
            changedGame.Title = newTitle;

            context.Update(changedGame, true);
            Assert.AreEqual(game, changedGame,
                "Update method does not work!");
        }

        [Test]
        public void UpdateWithNavigationalProperties()
        {
            Game changedGame = context.Read(game.Id);

            const string newTitle = "Hollow Knight: Silksong";
            changedGame.Title = newTitle;
            changedGame.Users.Clear();
            changedGame.Genres.Clear();

            context.Update(changedGame, true);
            Assert.That(
                game.Title == newTitle
                && game.Users.Count == 0
                && game.Genres.Count == 0
                && game == changedGame,
                "UpdateWithNavigationalProperties method does not work!");
        }

        [Test]
        public void Delete()
        {
            int gamesBefore = SetupFixture.dbContext.Games.Count();
            context.Delete(game.Id);

            int gamesAfter = SetupFixture.dbContext.Games.Count();
            Assert.That(gamesAfter == gamesBefore - 1, "Delete method does not work!");
        }
    }
}
