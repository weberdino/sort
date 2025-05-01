using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimationHandleNewNew : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;
    private AnimationClipPlayable attackPlayable;

    public AnimationClip swordSwingClip;  // Deine Animation (Sword Swing)
    private Animator animator;

    private AnimationLayerMixerPlayable layerMixer;
    public AvatarMask upperBodyMask;
    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        graph = PlayableGraph.Create("AttackGraph");
        output = AnimationPlayableOutput.Create(graph, "AttackOutput", animator);
        graph.Play();
        PlaySwordSwing();
    }

    private void OnEnable()
    {
        animator = GetComponentInParent<Animator>();
        graph = PlayableGraph.Create("AttackGraph");
        output = AnimationPlayableOutput.Create(graph, "AttackOutput", animator);

        //mask
        var runPlayable = AnimationClipPlayable.Create(graph, swordSwingClip);

        layerMixer = AnimationLayerMixerPlayable.Create(graph, 2);
        output.SetSourcePlayable(layerMixer);
        graph.Connect(runPlayable, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f);
        layerMixer.SetLayerMaskFromAvatarMask(1, upperBodyMask);

        graph.Play();
        PlaySwordSwing();
    }

    public void PlaySwordSwing()
    {
        if (attackPlayable.IsValid())
        {
            attackPlayable.Destroy();
        }

        swordSwingClip.wrapMode = WrapMode.Loop; // Für Schleifen
        attackPlayable = AnimationClipPlayable.Create(graph, swordSwingClip);
        output.SetSourcePlayable(attackPlayable);
    }

    void OnDestroy()
    {
        // Zerstöre den PlayableGraph, wenn das Objekt nicht mehr existiert
        graph.Destroy();
    }
}
