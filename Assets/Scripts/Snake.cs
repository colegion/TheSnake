using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : BaseTile
{
    [SerializeField] private GameObject visuals;

    public void RotateSelf()
    {
        var oldRotation = visuals.transform.localRotation.eulerAngles;
        visuals.transform.localRotation = Quaternion.Euler(new Vector3(oldRotation.x, oldRotation.y + 90f, oldRotation.z));
    }
}
