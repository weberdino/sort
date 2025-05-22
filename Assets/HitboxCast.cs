using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCast : AbilityCore
{
    public GameObject hitbox;
    public AnimationClip animation;
    //public AnimationHandleNew anim;
    //public AnimationHandle ah;
   // public AnimationHandleNew2 ah;
    //public AnimationHandelFInal test;
    public  AnimationHandleCast aht;

    private void Awake()
    {
        
    }

    void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            if (useable())
            {
                cast();
                base.use();
            }      
        }
    }

    void cast()
    {
        aht.PlayAbilityAnimation( animation);
        //anim.Play(2);
        //ah.PlaySwordSwing();
        // test.PlayClip("test");

        //aht.Play("test");
        var obj = Instantiate(hitbox, transform);
        Destroy(obj, 2);
    }
}
