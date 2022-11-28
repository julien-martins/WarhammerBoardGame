using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class DamageGrid
{
    public string v;
    private string path;
    private string jsonString;
    public char[][] grid;

    public DamageGrid(string v)
    {
        this.v = v;
        
        // Given JSON input:
        // {"name":"Dr Charles","lives":3,"health":0.8}
        // this example will return a PlayerInfo object with
        // name == "Dr Charles", lives == 3, and health == 0.8f.
    }

    public char[][] CreateFromJSON(string jsonString)
    {

        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }

}

