using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posUpdater : MonoBehaviour
{
   // public GameObject transfo;
    public GameObject target;
    public Vector3 offset;
    public GameObject child;
    bool flip;
    float val = 0;

    public bool usePlayer;
    public Vector3 pos;

    private void Start()
    {
        this.enabled = false;
        this.enabled = true;
    }
    void Update()
    {
        if (usePlayer)
        {
            target = PlayerManager.instance.player.gameObject;
        }
        //target = transform.Find("TornadoPos").gameObject;

        if(target != null)
        {
            transform.position = target.transform.position + offset;
        }


        if (flip)
        {
            if (val < 30)
            {
                 val += Time.deltaTime;
            }
            //float val = Mathf.Lerp(0, 30, 1);  

            //Vector3 a = new Vector3(0, 0, 0);
           // Vector3 b = new Vector3(30, 0, 0);

           // Vector3 dis = Vector3.Lerp(a, b, Time.deltaTime);
            transform.localPosition = new Vector3(val, 0,0);
        }
    }

    public void Move()
    {
        flip = !flip;
        if(flip)
        {
            transform.localPosition = new Vector3(-20, 0, 0);

            //Quaternion.Lerp(transform.position, new Vector3(30, 0, 0), 1);

            //Time.timeScale = 0;
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);

           // Time.timeScale = 1;
        }
        

    }
}
