using Newtonsoft.Json;
using OOP_ICT.Fifth.DbContext;

namespace OOP_ICT.Fifth.LastTurnUI;

public class JSONGetter
{
    public static int GetCurrentGameId()
    {
        string filePath = "/Users/valeriaotrosenko/Desktop/oop/valeriaotr/OOP-ICT.Fifth/DbContext/Game.json";
        string json = File.ReadAllText(filePath);
        var gameData = JsonConvert.DeserializeObject<GameId>(json);

        int currentId = gameData.Id;
        return currentId;
    }

    public static string GetConnectionString()
    {
        string filePath = "/Users/valeriaotrosenko/Desktop/oop/valeriaotr/OOP-ICT.Fifth/DbContext/Connection.json";
        string json = File.ReadAllText(filePath);
        var connectionData = JsonConvert.DeserializeObject<ConnectionData>(json);
    
        string connection = connectionData.Connection;
        return connection;
    }
}