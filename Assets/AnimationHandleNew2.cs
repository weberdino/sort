using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimationHandleNew2 : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;

    private AnimationClipPlayable baseLoopPlayable;      // Für Idle/Walk Loops
    private AnimationClipPlayable attackPlayable;        // Für Angriffe
    private AnimationLayerMixerPlayable layerMixer;      // Layered Animation

    private Animator animator;

    [Header("Animation Clips")]
    public AnimationClip idleClip;
    public AnimationClip walkClip;
    public AnimationClip swordSwingClip;

    [Header("Avatar Mask")]
    public AvatarMask upperBodyMask;                     // Für Attack Layer

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();

        // Graph & Output erstellen
        graph = PlayableGraph.Create("CharacterGraph");
        output = AnimationPlayableOutput.Create(graph, "AnimationOutput", animator);

        // Layer Mixer mit 2 Layern: 0 = Base (Idle/Walk), 1 = Attack
        layerMixer = AnimationLayerMixerPlayable.Create(graph, 2);
        output.SetSourcePlayable(layerMixer);

        // Initialisiere Base-Loop (Idle z.B.)
        baseLoopPlayable = AnimationClipPlayable.Create(graph, idleClip);
        baseLoopPlayable.SetDuration(double.PositiveInfinity);
        baseLoopPlayable.SetTime(0);
        baseLoopPlayable.SetSpeed(1f);

        graph.Connect(baseLoopPlayable, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f);

        // Setup Attack-Layer (initial leer)
        attackPlayable = AnimationClipPlayable.Create(graph, swordSwingClip);
        attackPlayable.SetDuration(swordSwingClip.length);
        attackPlayable.SetApplyFootIK(false);
        attackPlayable.Pause(); // Initial nicht aktiv

        graph.Connect(attackPlayable, 0, layerMixer, 1);
        layerMixer.SetInputWeight(1, 0f);

        // Setze Mask für Attack-Layer
        if (upperBodyMask != null)
        {
            layerMixer.SetLayerMaskFromAvatarMask(1, upperBodyMask);
        }

        graph.Play();
    }

    // Loop-Animation abspielen (Idle/Walk)
    public void PlayLoop(AnimationClip clip, bool loop = true)
    {
        if (clip == null) return;

        if (baseLoopPlayable.IsValid())
            baseLoopPlayable.Destroy();

        baseLoopPlayable = AnimationClipPlayable.Create(graph, clip);
        baseLoopPlayable.SetDuration(double.PositiveInfinity);
        baseLoopPlayable.SetTime(0);
        baseLoopPlayable.SetSpeed(1f);
        baseLoopPlayable.SetApplyFootIK(true);

        graph.Connect(baseLoopPlayable, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f);
    }

    // Angriff abspielen
    public void PlayAttack(AnimationClip clip, bool loop = false)
    {
        if (clip == null) return;

        if (attackPlayable.IsValid())
            attackPlayable.Destroy();

        attackPlayable = AnimationClipPlayable.Create(graph, clip);
        attackPlayable.SetApplyFootIK(false);
        attackPlayable.SetTime(0);
        attackPlayable.SetSpeed(1f);

        if (loop)
            attackPlayable.SetDuration(double.PositiveInfinity);
        else
            attackPlayable.SetDuration(clip.length);

        graph.Connect(attackPlayable, 0, layerMixer, 1);
        layerMixer.SetInputWeight(1, 1f);
    }

    // Angriff stoppen (Layer deaktivieren)
    public void StopAttack()
    {
        layerMixer.SetInputWeight(1, 0f);
    }

    private void OnDestroy()
    {
        graph.Destroy();
    }
}
