[System.Serializable]

public class Weapon
{

    
    public string name;
    public bool isRanged;
    public float rng;
    public int pow;
    public int correspondingWorkingPart;



    public Weapon(string name, bool isRanged, float rng, int pow, int correspondingWorkingPart)
    {
        this.name = name;
        this.isRanged = isRanged;
        this.rng = rng;
        this.pow = pow;
        this.correspondingWorkingPart = correspondingWorkingPart;
    }
}