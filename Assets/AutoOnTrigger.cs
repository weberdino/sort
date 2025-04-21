using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOnTrigger : GetNearestEnemy
{

    public override void use()
    {
        if (useable())
        {
            var obj = Instantiate(prefab, transform.parent);
            if (obj.TryGetComponent<ITarget>(out var targetable) && closestEnemy != null)
            {
                targetable.Init(this);
            }
        }
        base.use();
    }
}
