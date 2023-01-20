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
    
    void Start()
    {
        StreamReader reader = new StreamReader("Assets/Grids.json");
        var str = reader.ReadToEnd();
        reader.Close();
        
        gridUnits = JsonConvert.DeserializeObject<GridUnits>(str);
    }

    public GridUnits GetGridUnits() => gridUnits;
}
