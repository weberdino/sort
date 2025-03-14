using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotSpeed;
    public bool rotateClockwise;

    public float radius;

    public float xSpread;
    public float zSpread;
    public float yOffset;//camera
    public Transform centerPoint;

    public float timer = 0;

    public bool button;

    void Update()
    {
        Rotate();
        if (!button)
        {
            timer += Time.deltaTime * rotSpeed;
        }      
        transform.LookAt(centerPoint);

        if(Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(Burst());
        }
    }

    public void Timeup()
    {
        timer += Time.deltaTime * rotSpeed;
    }

    void Rotate()
    {
        if (rotateClockwise)
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            transform.position = pos + centerPoint.position;
        }

        else
        {
            float x = Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            transform.position = pos + centerPoint.position;
        }
    }

    public void externBurst()
    {
        StartCoroutine(Burst());
    }

    IEnumerator Burst()
    {
        var wait = new WaitForSeconds(.5f);

        rotSpeed = rotSpeed * 12;
        yield return wait;
        rotSpeed = rotSpeed / 12;
    }
}
