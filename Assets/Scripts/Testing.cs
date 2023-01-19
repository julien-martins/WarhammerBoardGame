using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    public  Warcasters caster1;
    public  Warcasters caster2;

    public void StartTesting()
    {
        InitializeGameObjectLink();
        GameManager.Instance.SetCharacters(caster1, caster2);
        GameManager.Instance.SetRandomCaster();
    }


    void InitializeGameObjectLink()
    {
        caster1.arucoGameObject = GameObject.FindWithTag(caster1.name);
        Debug.Log(caster1.arucoGameObject);
        caster1.distanceComponent = caster1.arucoGameObject.GetComponentInChildren<Circle>();
        
        caster2.arucoGameObject = GameObject.FindWithTag(caster2.name);
        caster2.distanceComponent = caster2.arucoGameObject.GetComponentInChildren<Circle>();

        for (int i = 0; i < caster1.warjackBattleGroup.Count; i++)
        {
            caster1.warjackBattleGroup[i].arucoGameObject = GameObject.FindWithTag(caster1.warjackBattleGroup[i].name);
            caster1.warjackBattleGroup[i].distanceComponent = caster1.warjackBattleGroup[i].arucoGameObject.GetComponentInChildren<Circle>();
        }
        for (int i = 0; i < caster2.warjackBattleGroup.Count; i++)
        {
            caster2.warjackBattleGroup[i].arucoGameObject = GameObject.FindWithTag(caster2.warjackBattleGroup[i].name);
            caster2.warjackBattleGroup[i].distanceComponent = caster2.warjackBattleGroup[i].arucoGameObject.GetComponentInChildren<Circle>();
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
