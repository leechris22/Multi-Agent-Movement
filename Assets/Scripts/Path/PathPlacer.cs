using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a line from the path using the linerenderer
[RequireComponent(typeof(LineRenderer))]
public class PathPlacer : MonoBehaviour {
    // Declare variables
    public float spacing = 1f;
    public float resolution = 1;

    // Hold the linerenderer
    private LineRenderer line;

    // On start, create points
    void Start() {
        line = GetComponent<LineRenderer>();
        Vector2[] points = GetComponent<PathCreator>().path.CalculateEvenlySpacedPoints(spacing, resolution);
        line.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++) {
            line.SetPosition(i, points[i]);
        }
    }


}