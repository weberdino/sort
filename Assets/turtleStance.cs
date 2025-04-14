using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtleStance : AbilityCore, IStance
{
    public Animator anim;
    StanceController controller;

    void OnEnable()
    {
        controller = StanceController.instance;
        controller.SetStance(this);
    }

    public void Enter()
    {
        createUi();
    }

    public void Exit()
    {   
        Destroy(imageInstance);
        PlayerManager.instance.player.GetComponent<PlayerStats>().Str.RemoveModifier(15);
        stanceDisable();
    }

}
