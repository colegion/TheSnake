using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    protected int _x;
    protected int _y;
    protected int _layer;
    
    public int X => _x;
    public int Y => _y;
    public int Layer => _layer;

    public void ConfigureSelf(int x, int y)
    {
        _x = x;
        _y = y;
        SetTransform();
    }

    public void SetLayer(int layer)
    {
        _layer = layer;
    }

    public void SetTransform()
    {
        transform.localPosition = new Vector3(_x, 1, _y);
    }

    public void SetXCoordinate(int x)
    {
        _x = x;
        SetTransform();
    }

    public void SetYCoordinate(int y)
    {
        _y = y;
        SetTransform();
    }
}
