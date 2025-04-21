using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : GetNearestEnemy
{
    void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            Instantiate(prefab, transform.parent);
            use();
        }
    }
}
