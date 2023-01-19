using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializableField] public Warcasters caster1;
    [SerializableField] public Warcasters caster2;

    void Start()
    {
        GameManager.Instance.SetCharacters(caster1, caster2);
        GameManager.Instance.SetRandomCaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
