using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReynaEyeCast : MonoBehaviour
{
    public GameObject prefab;
    float cd;
    Player p;
    public Vector3 spawntrans;

    private void Start()
    {
        p = PlayerManager.instance.player.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        cd += Time.deltaTime;

        //if (p.BlindingEye())
        {
            if (cd > 4)
            {
                var proj = Instantiate(prefab);

                proj.transform.position = this.transform.position;
                proj.transform.rotation = this.transform.rotation;
                proj.transform.Rotate(spawntrans);

                Destroy(proj.gameObject, 5);

                cd = 0;
            }
        }
    }
}
