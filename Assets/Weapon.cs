public class Weapon
{


    string name;
    bool isRanged;
    float rng;
    int pow;
    int correspondingWorkingPart;



    public Weapon(string name, bool isRanged, float rng, int pow, int correspondingWorkingPart)
    {
        this.name = name;
        this.isRanged = isRanged;
        this.rng = rng;
        this.pow = pow;
        this.correspondingWorkingPart = correspondingWorkingPart;
    }
}