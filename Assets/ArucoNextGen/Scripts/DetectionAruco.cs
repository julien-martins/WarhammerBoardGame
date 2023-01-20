using UnityEngine;

using System;
using System.ComponentModel;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using UnityEngine.UI;

public class DetectionAruco : MonoBehaviour
{

    public RawImage rawImage;
    VideoCapture capture;
    private Mat webcamFrame;
    private Texture2D _tex;

    public GameObject TestPrefab;
    
    [Category("Parameters")]
    int markersX = 10;
    int markersY = 6;
    int markersLength = 200;
    int markersSeparation = 30;
    private Dictionary ArucoDict;
    private GridBoard ArucoBoard;
    private DetectorParameters ArucoParameters;
    private String cameraConfigurationFile;
    private Mat cameraMatrix;
    private Mat distortionMatrix;
    
    // Get the centroid of an object based on 4 coners.
    PointF GetCentroidFromCorner(VectorOfPointF corner)
    {
        PointF center = new PointF(0, 0);
        center.X = (corner[0].X + corner[1].X + corner[2].X + corner[3].X) / 4; //X is on horizontal axis = cols /!\ opencv Mat are row based (y,x) = (i,j), left to right, top to bottom
        center.Y = (corner[0].Y + corner[1].Y + corner[2].Y + corner[3].Y) / 4; //Y is on vertical axis = rows /!\ opencv Mat are row based (y,x) = (i,j), left to right, top to bottom
        return center;
    }

    // Convert a rotation vector in a rotation matrix using Rodrigues algorithm.
    Mat GetRotationMatrixFromRotationVector(VectorOfDouble rvec)
    {
        Mat rmat = new Mat();
        CvInvoke.Rodrigues(rvec, rmat);
        return rmat;
    }
    
    // Convert a rotation matrix to a Quternion (array of double).
    // C# translation of the C++ version available here https://gist.github.com/shubh-agrawal/76754b9bfb0f4143819dbd146d15d4c8
    void GetQuaternion(Mat Rmat, out double[] Q)
    {
        Image<Gray, Byte> R = Rmat.ToImage<Gray, Byte>();
        double trace = R.Data[0, 0, 0] + R.Data[1, 1, 0] + R.Data[2, 2, 0];

        Q = new double[4];

        if (trace > 0.0)
        {
            double s = Math.Sqrt(trace + 1.0);
            Q[3] = (s * 0.5);
            s = 0.5 / s;
            Q[0] = ((R.Data[2, 1, 0] - R.Data[1, 2, 0]) * s);
            Q[1] = ((R.Data[0, 2, 0] - R.Data[2, 0, 0]) * s);
            Q[2] = ((R.Data[1, 0, 0] - R.Data[0, 1, 0]) * s);
        }

        else
        {
            int i = R.Data[0, 0, 0] < R.Data[1, 1, 0] ? (R.Data[1, 1, 0] < R.Data[2, 2, 0] ? 2 : 1) : (R.Data[0, 0, 0] < R.Data[2, 2, 0] ? 2 : 0);
            int j = (i + 1) % 3;
            int k = (i + 2) % 3;

            double s = Math.Sqrt(R.Data[i, i, 0] - R.Data[j, j, 0] - R.Data[k, k, 0] + 1.0f);
            Q[i] = s * 0.5;
            s = 0.5 / s;

            Q[3] = (R.Data[k, j, 0] - R.Data[j, k, 0]) * s;
            Q[j] = (R.Data[j, i, 0] + R.Data[i, j, 0]) * s;
            Q[k] = (R.Data[k, i, 0] + R.Data[i, k, 0]) * s;
        }
    }

    private void DisplayFrameOnPlane()
    {
        if (webcamFrame.IsEmpty) return;
        
        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        if (_tex != null)
        {
            Destroy(_tex);
            _tex = null;
        }
        
        _tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        CvInvoke.Resize(webcamFrame, webcamFrame, new System.Drawing.Size(width, height));
        CvInvoke.CvtColor(webcamFrame, webcamFrame, ColorConversion.Bgr2Rgba);
        CvInvoke.Flip(webcamFrame, webcamFrame, FlipType.Vertical);

        _tex.LoadRawTextureData(webcamFrame.ToImage<Rgba, byte>().Bytes);
        _tex.Apply();
        
        rawImage.texture = _tex;
    }
    
    void Start()
    {
        capture = new VideoCapture(0);
        
        ArucoDict = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50); // bits x bits (per marker) _ number of markers in dict
        ArucoBoard = new GridBoard(markersX, markersY, markersLength, markersSeparation, ArucoDict);
        
        ArucoParameters = new DetectorParameters();
        ArucoParameters = DetectorParameters.GetDefault();
        
        /*
        // Calibration done with https://docs.opencv.org/3.4.3/d7/d21/tutorial_interactive_calibration.html
        cameraConfigurationFile = "F:/Documents/Unreal Projects/WarhammerBoardGame/Assets/ArucoNextGen/CamParam/cameraParameters.xml";
        FileStorage fs = new FileStorage(cameraConfigurationFile, FileStorage.Mode.Read);
        if (!fs.IsOpened)
        {
            Console.WriteLine("Could not open configuration file " + cameraConfigurationFile);
            return;
        }
        */
        cameraMatrix = new Mat(new Size(3, 3), DepthType.Cv32F, 1);
        distortionMatrix = new Mat(1, 8, DepthType.Cv32F, 1);
        /*
        fs["cameraMatrix"].ReadMat(cameraMatrix);
        fs["dist_coeffs"].ReadMat(distortionMatrix);*/
    }

    private void Update()
    {
        webcamFrame = new Mat();
        webcamFrame = capture.QueryFrame();

        if (!webcamFrame.IsEmpty)
        {
            VectorOfInt ids = new VectorOfInt(); // name/id of the detected markers
            VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF(); // corners of the detected marker
            VectorOfVectorOfPointF rejected = new VectorOfVectorOfPointF(); // rejected contours
            ArucoInvoke.DetectMarkers(webcamFrame, ArucoDict, corners, ids, ArucoParameters, rejected);

            if (ids.Size > 0)
            {
                ArucoInvoke.DrawDetectedMarkers(webcamFrame, corners, ids, new MCvScalar(255, 0, 255));
                
                Mat rvecs = new Mat(); // rotation vector
                Mat tvecs = new Mat(); // translation vector
                ArucoInvoke.EstimatePoseSingleMarkers(corners, markersLength, cameraMatrix, distortionMatrix, rvecs, tvecs);
                
                for (int i = 0; i < ids.Size; i++)
                {
                    using (Mat rvecMat = rvecs.Row(i))
                    using (Mat tvecMat = tvecs.Row(i))
                    using (VectorOfDouble rvec = new VectorOfDouble())
                    using (VectorOfDouble tvec = new VectorOfDouble())
                    {
                        double[] values = new double[3];
                        rvecMat.CopyTo(values);
                        rvec.Push(values);
                        tvecMat.CopyTo(values);
                        tvec.Push(values);
                        TestPrefab.transform.position = new Vector3((float)rvec[0], (float)rvec[1], (float)rvec[2]);
                        //ArucoInvoke.DrawAxis(frame, 
                        //                     cameraMatrix, 
                        //                     distortionMatrix, 
                        //                     rvec, 
                        //                     tvec, 
                        //                     markersLength * 0.5f);

                    }
                }
                
            }
        }
        
        //DisplayFrameOnPlane();
    }
}
