using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtleStance : AbilityCore, IStance
{
    public Animator anim;
    StanceController controller;

    void Start()
    {
        
    }
    void OnEnable()
    {
        controller = StanceController.instance;
        controller.SetStance(new turtleStance());
    }

    public void Enter()
    {
        
        createUi();
    }

    public void Exit()
    {
        PlayerManager.instance.player.GetComponent<PlayerStats>().Str.RemoveModifier(15);
        Destroy(imageInstance);
    }
}
