using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Interactable, ITarget
{
    public float speed;
    public GameObject vfx;
    public GameObject projectile;
    public float boost;
    List<GameObject> targetEnemy;

    public void Init(GetNearestEnemy target)
    {
        targetEnemy = target.sortedEnemies;
        //Leap();
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * (speed + boost) / 500;
        if (boost > 0)
        {
            boost -= 3f * Time.deltaTime;
            if (boost < 0) boost = 0;
        }
    }

    public override void Interact(CharacterStats.atkart myType, int modifier, GameObject obj)
    {
        Debug.Log("tornado");
        switch (myType)
        {
            case CharacterStats.atkart.projectile:
                Split(obj);
                break;
            case CharacterStats.atkart.hit:
                Whip();
                break;
            case CharacterStats.atkart.spell:
                Ignite();
                break;
        }
    }

    void Split(GameObject projectileObj)
    {
        projectile = projectileObj;
        if (projectile.GetComponent<Hitbox>().isDirty == false)
        {
            projectile.GetComponent<Hitbox>().isDirty = true;
            for (int i = 0; i < 3; i++)
            {
                var obj = Instantiate(projectile);
                if(targetEnemy != null)
                {
                    obj.transform.LookAt(targetEnemy[i].transform);
                }
                else
                {
                    obj.transform.rotation = transform.rotation;
                }              
            }
        }
    }

    void Ignite()
    {
        //change to fire
    }

    void Whip()
    {
        boost += 15f;
    }
}
