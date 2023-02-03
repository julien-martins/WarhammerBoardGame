using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GridUnits
{
    public int[,] Crusader;
    public int[,] Vanquisher;
    public int[,] IronClad;
    public int[,] FireFly;
    public int[,] Lancer;
    public int[,] Repenter;
    public int[,] Slayer;
    public int[,] Reaper;
    public int[,] Deathripper;
}

public class GridReader : MonoBehaviour
{
    private GridUnits gridUnits;
    
    public void Initialize(){
        StreamReader reader = new StreamReader("Assets/Grids.json");
        var str = reader.ReadToEnd();
        reader.Close();
        
        gridUnits = JsonConvert.DeserializeObject<GridUnits>(str);
    }

    public int[,] GetGridUnits(string name)
    {
        switch (name) {
            case "Crusader":
                return gridUnits.Crusader;
            case "Vanquisher":
                return gridUnits.Vanquisher;
            case "Ironclad":
                return gridUnits.IronClad;
            case "FireFly":
                return gridUnits.FireFly;
            case "Lancer":
                return gridUnits.Lancer;
            case "Repenter":
                return gridUnits.Repenter;
            case "Slayer":
                return gridUnits.Slayer;
            case "Reaper":
                return gridUnits.Reaper;
            case "Deathripper":
                return gridUnits.Deathripper;
            default:
                return null;
        }


}
    }


