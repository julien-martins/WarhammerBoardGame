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

    public GameObject model;


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
            Draw(30);
            isAlreadyEnabled = false;
        }

        if(lineRenderer.positionCount > 0 && Vector3.Distance(center, transform.position) > radius * 0.8)
        {
            Debug.Log(Vector3.Distance(center, transform.position) + " vs " + radius);
            lineRenderer.material = red; 
        }
        else
        {
            Debug.Log("In circle");
            lineRenderer.material = green;
        }

    }

    public void Draw(float speed) //Only need to draw when something changes
    {
        lineRenderer.positionCount = lineCount;
        lineRenderer.startWidth = width;
        float theta = (2f * Mathf.PI) / lineCount;  //find radians per segment
        float angle = 0;

        radius = speed * 200;

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

    public void DestroyCircle()
    {
        lineRenderer.positionCount = 0;
    }
}