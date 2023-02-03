using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Warjack", menuName = "Warhammer/Warjack")]
public class Warjack : Unit
{

    public int[,] grid;

    public bool isArcNode;




    void Start(){
        grid =  _gridReader.GetGridUnits(name);
    }

    public override bool TakesDamage(int dmg, int collumn)
    {
        if (dmg <= 0)
        {
            Debug.Log("No damages");
            return false;
        }
        else
        {

            Debug.Log(dmg + " in collumn " + collumn + " on the " + name);
            for(int index =0; index <= dmg ; ++index){
                if(DealDotOfDamage(collumn)){
                    Debug.Log(name + " died, remove him from play please " );
                    return true;
                }else{

                }

            }
            return false;
        }
    }

    private bool DealDotOfDamage(int col){
        int colIndex = col;
        while (true){

                for(int indexRow = 0; indexRow <= 5; indexRow ++){
                    if(grid[colIndex, indexRow] != 0){
                        grid[colIndex, indexRow] = 0;
                        return isDead();
                    }
                }
                colIndex ++;
                if(colIndex > 5){
                colIndex = 0;

                }
    }

    }





    private bool isDead(){



        bool[] arr = {false, false, false, false, false,false};
        bool isDead = true;

        for(int indexc = 0; indexc <= 5; ++indexc){
             for(int indexr = 0; indexr <= 5; ++ indexr){

                switch(grid[indexc,indexr] ){
                     case 1:
                     arr[0] = true; 
                    isDead = false;
                     break;
                    case 2:
                     arr[1] = true; 
                    isDead = false;
                     break;
                      case 3:
                     arr[3] = true; 
                    isDead = false;
                     break; 
                     case 4:
                     arr[3] = true; 
                    isDead = false;
                     break;
                      case 5:
                     arr[5] = true; 
                    isDead = false;
                     break;
                     case 6 :
                    isDead = false;
                     break;

                }
         
            }
        }
        this.workingParts[3] = arr[3];
        this.workingParts[5] = arr[5];



        return isDead;
    }

}
