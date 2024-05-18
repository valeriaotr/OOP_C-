using Npgsql;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.DbContext;

public class DbRepository
{
    private readonly CasinoDbContext _dbContext;

    public DbRepository()
    {
        _dbContext = new CasinoDbContext();
    }

    public void AddLog(Player player)
    {
        var existingLog = _dbContext.pokeroperationslog.FirstOrDefault(log => log.PlayerName == player.GetPlayerName());

        if (existingLog == null)
        {
            var newLog = new CasinoLog
            {
                PlayerName = player.GetPlayerName(),
                FirstChoice = player.FirstChoice,
                FirstBet = player.FirstBet,
                GameId = player.PlayedGameId,
                SecondChoice = string.Empty,
                SecondBet = 0,
                ThirdChoice = string.Empty,
                ThirdBet = 0
            };

            _dbContext.pokeroperationslog.Add(newLog);
        }
        else
        {
            existingLog.SecondChoice = player.SecondChoice;
            existingLog.SecondBet = player.SecondBet;

            if (!string.IsNullOrEmpty(player.ThirdChoice))
            {
                existingLog.ThirdChoice = player.ThirdChoice;
                existingLog.ThirdBet = player.ThirdBet;
            }
        }

        _dbContext.SaveChanges();
    }


    public void AddPlayerToRating(Player player)
    {
        var casinoPlayer = new CasinoPlayer
        {
            PlayerName = player.GetPlayerName(),
            Winning = player.Winning,
            GameId = player.PlayedGameId
        };
        _dbContext.playersrating.Add(casinoPlayer);
        _dbContext.SaveChanges();
    }
}