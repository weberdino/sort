using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEngine.Animations;

public class defaultAttack : GetNearestEnemy
{
    NavMeshAgent agent;

    public AnimationClip clip;

    private PlayableGraph graph;
    private AnimationPlayableOutput output;
    private AnimationClipPlayable playable;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        graph = PlayableGraph.Create();
        output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        playable = AnimationClipPlayable.Create(graph, clip);
        output.SetSourcePlayable(playable);
        graph.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            agent.destination = closestEnemy.transform.position;
        }
    }
}
