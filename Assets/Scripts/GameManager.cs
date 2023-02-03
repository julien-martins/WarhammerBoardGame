using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GridReader _gridReader;
    private List<Warcasters> _listWarcaster;
    private Warcasters _actualWarcaster;
    public Testing test;

 
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("error!");
            }
            return _instance;
        }

    }

    public void SetRandomCaster()
    {
        if (Random.Range(0, 1) == 0)
        _actualWarcaster = _listWarcaster[0];
        else
        {
            _actualWarcaster = _listWarcaster[1];
        }

    }
    
    public void PassTurn()
    {
        if (_listWarcaster[0] == _actualWarcaster)
            _actualWarcaster = _listWarcaster[1];
        else
            _actualWarcaster = _listWarcaster[0];

    }
    public Warcasters GetActualWarcaster()
    {
    
        return _actualWarcaster;
    }
    public Warcasters GetActualEnemyCaster()
    {

        if (_listWarcaster[0] == _actualWarcaster)
            return _listWarcaster[1];
        else
            return _listWarcaster[0];
    }


    private void Awake()
    {
        _instance = this;

        //Initialize the grid reader
        _gridReader.Initialize();

        _listWarcaster = new List<Warcasters>();
        test.StartTesting();
        SetRandomCaster();


    }

    public void SetCharacters(Warcasters caster1, Warcasters caster2)
    {
        _listWarcaster.Add(caster1);
        _listWarcaster.Add(caster2);


    }
}
