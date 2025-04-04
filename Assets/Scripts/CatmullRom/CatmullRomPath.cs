using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomPath : MonoBehaviour
{
    private CatmullRomSpline spline;
    private List<Vector3> pathPoints = new List<Vector3>();

    public List<Vector3> Points { get { return pathPoints; } }

    void Awake()
    {
        spline = GetComponent<CatmullRomSpline>();
        RebuildPath();
    }

    public void RebuildPath()
    {
        if (spline == null)
            spline = GetComponent<CatmullRomSpline>();

        const int detail = 16;

        pathPoints.Clear();

        for (int i = 0; i < spline.PointCount; i++)
        {
            CatmullRomSegment segment = spline.GetSegment(i);

            for (float t = segment.T1; t < segment.T2; t += (segment.T2 - segment.T1) / detail)
            {
                Vector3 point = segment.GetPoint(t, false);
                if (pathPoints.Count == 0 || pathPoints[pathPoints.Count - 1] != point)
                {
                    pathPoints.Add(point);
                }
            }
        }
    }
}
