using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private List<Warcasters> _listWarcaster;
    private Warcasters _actualWarcaster;


    private void Start()
    {

    }
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
    public void PassTurn()
    {
        if (_listWarcaster[0] == _actualWarcaster)
            _actualWarcaster = _listWarcaster[1];
        else
            _actualWarcaster = _listWarcaster[0];

    }
    public Warcasters getActualWarcaster()
    {
    
        return _actualWarcaster;
    }


    private void Awake()
    {
        _instance = this;

    }

}
