using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimationHandleNew : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;
    private AnimationClipPlayable attackPlayable;

    public AnimationClip Clip;  // Deine Animation (Sword Swing)

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        graph = PlayableGraph.Create("AttackGraph");
        output = AnimationPlayableOutput.Create(graph, "AttackOutput", animator);
        graph.Play();
        //Play();
    }

    private void OnEnable()
    {
        animator = GetComponentInParent<Animator>();
        graph = PlayableGraph.Create("AttackGraph");
        output = AnimationPlayableOutput.Create(graph, "AttackOutput", animator);

        graph.Play();
       // Play();
    }

    public void Play()
    {
        if (attackPlayable.IsValid())
        {
            attackPlayable.Destroy();
        }

        Clip.wrapMode = WrapMode.Loop; // Für Schleifen
        attackPlayable = AnimationClipPlayable.Create(graph, Clip);
        output.SetSourcePlayable(attackPlayable);
    }

    void OnDestroy()
    {
        // Zerstöre den PlayableGraph, wenn das Objekt nicht mehr existiert
        graph.Destroy();
    }

    public void Idle()
    {
        attackPlayable.Pause();
    }
}
