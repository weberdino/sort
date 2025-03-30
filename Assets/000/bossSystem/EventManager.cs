using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<Event> events = new List<Event>();
    Animator animator;

    private void OnEnable()
    {
        startEvent();
    }

    void startEvent()
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i].doEvent(this);
        }
    }

    public void doEvent(GameObject hitbox)
    {
        Instantiate(hitbox);
    }
}
