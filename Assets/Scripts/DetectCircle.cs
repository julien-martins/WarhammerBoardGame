using System;
using System.Drawing;
using ArucoUnity.Plugin;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

using UnityEngine;
using UnityEngine.UI;

public class DetectCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage rawImage;
    
    private VideoCapture _capture;
    private Mat webcamFrame;
    public Texture2D tex;
    
    void Start()
    {
        _capture = new VideoCapture(0);
        webcamFrame = new Mat();
        _capture.ImageGrabbed += VidOnImageGrabbed;
        _capture.Start();
    }

    private void VidOnImageGrabbed(object sender, EventArgs e)
    {
        try
        {
            _capture.Retrieve(webcamFrame);
        }
        catch (Exception)
        {
        }

        lock (webcamFrame)
        {
            Mat gray = new Mat(webcamFrame.Width, webcamFrame.Height, DepthType.Cv8U, 1);
            Mat hsv = new Mat(webcamFrame.Width, webcamFrame.Height, DepthType.Cv8U, 1);
            CvInvoke.CvtColor(webcamFrame, hsv, ColorConversion.Bgr2Hsv);

            int minRadius = 10;
            int maxRadius = 128;

            Mat lower_red_hue_range = new Mat();
            CvInvoke.InRange(hsv, new ScalarArray(new MCvScalar(0, 255, 255)), new ScalarArray(new MCvScalar(10, 255, 255)), lower_red_hue_range);

            CircleF[] circles = CvInvoke.HoughCircles(hsv, HoughModes.Gradient, 3, hsv.Rows / 8, 200, 200, minRadius, maxRadius);

            foreach (var circle in circles)
            {
                Point pt = new Point((int)circle.Center.X, (int)circle.Center.Y);
                
                CvInvoke.Circle(webcamFrame, pt, (int)circle.Radius, new MCvScalar(255, 0, 0), 1);
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_capture.IsOpened) return;

        bool grabbed = _capture.Grab();

        if (!grabbed) return;
        
        DisplayFrameOnPlane();
        
        System.Threading.Thread.Sleep(60);
    }
    
    private void DisplayFrameOnPlane()
    {
        if (webcamFrame.IsEmpty) return;
        
        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        if (tex != null)
        {
            Destroy(tex);
            tex = null;
        }
        
        tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        CvInvoke.Resize(webcamFrame, webcamFrame, new System.Drawing.Size(width, height));
        CvInvoke.CvtColor(webcamFrame, webcamFrame, ColorConversion.Bgr2Rgba);
        CvInvoke.Flip(webcamFrame, webcamFrame, FlipType.Vertical);

        tex.LoadRawTextureData(webcamFrame.ToImage<Rgba, byte>().Bytes);
        tex.Apply();
        
        rawImage.texture = tex;
    }
}
