using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Event
{
    public enum eventType{ move, attack};
    public eventType selectType;
    public Animator animation;
    public GameObject hitbox;

    NavMeshAgent agent;
    public Transform target;

    public void doEvent(EventManager em)
    {
        animation.Play("");
        switch (selectType)
        {
            case eventType.move:
                move();
                break;
            case eventType.attack:
                attack(em);
                break;
        }
    }

    void move()
    {

        agent.destination = target.position;
    }

    void attack(EventManager em) 
    {
        em.doEvent(hitbox);
    }
}
