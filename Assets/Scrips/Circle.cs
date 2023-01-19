using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [Range(6, 60)]   //creates a slider - more than 60 is hard to notice
    public int lineCount = 60;       //more lines = smoother ring
    public float radius;
    public float width = 30;

    public Material red;
    public Material green;

    private Vector3 center;


    public bool isAlreadyEnabled = false;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;
    }

    private void Update()
    {
        if (isAlreadyEnabled)
        {
            Draw();
            isAlreadyEnabled = false;
        }

        if(Vector3.Distance(center, transform.position) > radius)
        {
            lineRenderer.material = red; 
        }
        else
        {
            lineRenderer.material = green;
        }

    }

    void Draw() //Only need to draw when something changes
    {
        lineRenderer.positionCount = lineCount;
        lineRenderer.startWidth = width;
        float theta = (2f * Mathf.PI) / lineCount;  //find radians per segment
        float angle = 0;

        center = transform.position;

        for (int i = 0; i < lineCount; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector3 pos = transform.localToWorldMatrix * new Vector4(x, 0, y, 1);
            lineRenderer.SetPosition(i, pos);
            //switch 0 and y for 2D games
            angle += theta;
        }
    }

    public void DrawDistance(Transform figTransform, float vitesse)
    {
        lineRenderer.positionCount = lineCount;
        lineRenderer.startWidth = width;
        float theta = (2f * Mathf.PI) / lineCount;  //find radians per segment
        float angle = 0;

        for (int i = 0; i < lineCount; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector3 pos = figTransform.localToWorldMatrix * new Vector4(x, 0, y, 1);
            lineRenderer.SetPosition(i, pos);
            //switch 0 and y for 2D games
            angle += theta;
        }
    }
}