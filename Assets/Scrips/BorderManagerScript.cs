using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManagerScript : MonoBehaviour
{
    public List<GameObject> corners;
    LineRenderer lineRenderer;

    public float width;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = corners.Count;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.startWidth = width;
        for (int i = 0; i < corners.Count; i++)
        {
            lineRenderer.SetPosition(i, corners[i].transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        if(corners != null)
        {
            for (int i = 0; i < corners.Count - 1; i++)
            {
                Gizmos.DrawLine(corners[i].transform.position, corners[i + 1].transform.position);
            }
            Gizmos.DrawLine(corners[corners.Count-1].transform.position, corners[0].transform.position);
        }
        
    }
}
