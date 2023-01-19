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
        bool allMakersVisible = true;
        for (int i = 0; i < corners.Count; i++)
        {
            if (!corners[i].activeInHierarchy)
                allMakersVisible = false;
            lineRenderer.SetPosition(i, corners[i].transform.position);
        }

        if (!allMakersVisible)
            lineRenderer.enabled = false;
        else
            lineRenderer.enabled = true;
    }


}
