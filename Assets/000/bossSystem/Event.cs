using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Event
{
    public enum eventType{ move, attack};
    public eventType selectType;
    public Animation animation;
    public GameObject Hitbox;


    public Move mover;
    public List<Move> events = new List<Move>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move()
    {

    }
}
