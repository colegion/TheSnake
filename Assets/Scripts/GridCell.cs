using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEditor.Playables;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int _x;
    private int _y;

    public int X => _x;
    public int Y => _y;

    public void ConfigureSelf(int x, int y)
    {
        _x = x;
        _y = y;
        name = $"{name}{x}_{y}";
        SetRotation();
    }

    private void SetRotation()
    {
        if (Utilities.IsEdgeCell(_y))
        {
            transform.localRotation = Quaternion.Euler(Vector3.down * 90f);
        }
    }
}
