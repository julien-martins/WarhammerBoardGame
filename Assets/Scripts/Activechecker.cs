using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activechecker : MonoBehaviour
{
    [SerializableField] public GameObject _validator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_validator.activeSelf)
        {
            Debug.Log("Hidden");
        }
        else
        {
            Debug.Log("Shown!");
        }
    }
}
