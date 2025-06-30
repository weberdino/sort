using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float pitch = 2f;

    private float currentZoom = 10f;

    void LateUpdate ()
    {
       // if(Input.GetKeyDown(KeyCode.Alpha2))
       // {
           // offset = offset + new Vector3(2,0,0);
        //}

        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
