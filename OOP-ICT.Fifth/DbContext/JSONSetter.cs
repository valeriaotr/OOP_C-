using Newtonsoft.Json;
using OOP_ICT.Fifth.LastTurnUI;

namespace OOP_ICT.Fifth.DbContext;

public class JSONSetter
{
    public void UpdateId()
    {
        string filePath = "/Users/valeriaotrosenko/Desktop/oop/valeriaotr/OOP-ICT.Fifth/DbContext/Game.json";
        string json = File.ReadAllText(filePath);
        var gameData = JsonConvert.DeserializeObject<GameId>(json);
        
        gameData.Id += 1;
        string updatedJson = JsonConvert.SerializeObject(gameData);
        
        File.WriteAllText(filePath, updatedJson);
    }
}