using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class camHandleUI : MonoBehaviour
{
    public Camera cam;
    public GameObject Figur;
    public Vector3 rotation;
    public float zoom;

    public void setFigur()
    {
        Quaternion current = Figur.transform.rotation;
        Quaternion increment = Quaternion.Euler(rotation);
        Quaternion rot = current * increment;

        StartCoroutine(SmoothRotate(rot, .4f));
        cam.orthographicSize = zoom;
        GetComponent<Button>().interactable = false;
    }

    IEnumerator SmoothRotate(Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = Figur.transform.rotation;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            Figur.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);
            yield return null;
        }

        //block button
        Transform parent = this.transform.parent;
        foreach(Transform trans in parent)
        {
            Button button = trans.GetComponent<Button>();
            if (trans.gameObject != this.gameObject)
            {
                
                button.interactable = true;
            }
            
        }
    }
}
