using Mastermind.DataAccessLayer;
using Mastermind.GameModels;
using Mastermind.Models;
using Mastermind.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mastermind.Controllers
{
    [Authorize(Roles = Member.ROLE_STANDARD + "," + Member.ROLE_ADMIN)]
    public class GameController : Controller
    {
        private const string SESSION_GAME_NAME = "CurrentGame";

        private Game CreateOrGetGame()
        {
            Game? game = null;

            string? currentJsonGame = HttpContext.Session.GetString(SESSION_GAME_NAME);

            if (currentJsonGame != null)
                game = JsonSerializer.Deserialize<Game>(currentJsonGame);

            if (game == null)
            {
                Dictionary<string, Config> configByKey = new DAL().ConfigFact.GetAll();

                int.TryParse(configByKey[Config.NB_COLORS].Value, out int nbColors);
                int.TryParse(configByKey[Config.NB_POSITIONS].Value, out int nbPositions);
                int.TryParse(configByKey[Config.NB_ATTEMPTS].Value, out int nbAttempts);

                game = new(nbColors, nbPositions, nbAttempts);

                HttpContext.Session.SetString(
                    SESSION_GAME_NAME,
                    JsonSerializer.Serialize(game)
                );
            }

            return game;
        }

        public IActionResult Index()
        {
            Game game = CreateOrGetGame();

            DAL dal = new();

            string? username = User.Identity?.Name;

            MemberStats? stats = !string.IsNullOrWhiteSpace(username)
                ? dal.MemberFact.GetByUsername(username) is Member member
                    ? dal.MemberStatsFact.GetByMemberId(member.Id)
                    : null
                : null;

            return View(new GameVM(game, stats));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Validate(IFormCollection collection)
        {
            Game? game = null;

            string? currentJsonGame =
                HttpContext.Session.GetString(SESSION_GAME_NAME);

            if (currentJsonGame != null)
            {
                game = JsonSerializer.Deserialize<Game>(currentJsonGame);

                if (game != null)
                {
                    PlayerRow playerRow = new();

                    // Build player's row from submitted form values
                    for (int position = 1; position <= game.NbPositions; position++)
                    {
                        int.TryParse(
                            collection[$"color-index-{position}"],
                            out int color
                        );

                        playerRow.Pawns.Add(new Pawn { Color = color });
                    }

                    game.Validate(playerRow);

                    // Save stats only once when game ends
                    if (game.State != Game.GameState.Running && !game.StatisticsSaved)
                    {
                        string? username = User.Identity?.Name;

                        if (!string.IsNullOrWhiteSpace(username))
                        {
                            DAL dal = new();

                            Member? member =
                                dal.MemberFact.GetByUsername(username);

                            if (member != null)
                            {
                                if (game.State == Game.GameState.PlayerWin)
                                    dal.MemberStatsFact.RegisterWin(
                                        member.Id,
                                        game.PlayerRows.Count
                                    );
                                else if (game.State == Game.GameState.ComputerWin)
                                    dal.MemberStatsFact.RegisterLoss(member.Id);

                                game.StatisticsSaved = true;
                            }
                        }
                    }

                    HttpContext.Session.SetString(
                        SESSION_GAME_NAME,
                        JsonSerializer.Serialize(game)
                    );
                }
            }

            game ??= CreateOrGetGame();

            return PartialView("PartialGame", game);
        }

        public IActionResult Replay()
        {
            HttpContext.Session.Remove(SESSION_GAME_NAME);

            return RedirectToAction("Index");
        }
    }
}