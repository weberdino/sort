using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCast : MonoBehaviour
{
    public GameObject hitbox;
    public AnimationClip animation;
    AnimationHandle ah;

    void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            cast();
        }
    }

    void cast()
    {
        ah.Play(animation);
        var obj = Instantiate(hitbox, transform);
        Destroy(obj, 2);
    }
}
