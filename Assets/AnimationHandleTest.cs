using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using System.Collections;

public class AnimationHandleTest : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;

    private AnimationClipPlayable walkPlayable;
    private AnimationClipPlayable attackPlayable;

    private AnimationLayerMixerPlayable layerMixer;

    public AnimationClip walkClip;
    public AvatarMask upperBodyMask;

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        graph = PlayableGraph.Create("ComboGraph");
        output = AnimationPlayableOutput.Create(graph, "Animation", animator);

        // 2 Layer: 0 = walk, 1 = attack
        layerMixer = AnimationLayerMixerPlayable.Create(graph, 2);
        output.SetSourcePlayable(layerMixer);

        // Walk Layer
        walkPlayable = AnimationClipPlayable.Create(graph, walkClip);
        walkPlayable.SetApplyFootIK(true);

        walkClip.wrapMode = WrapMode.Loop;
        graph.Connect(walkPlayable, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f);

        // Initial dummy attack playable (wird später ersetzt)
        attackPlayable = AnimationClipPlayable.Create(graph, walkClip); // Dummy
        graph.Connect(attackPlayable, 0, layerMixer, 1);
        layerMixer.SetInputWeight(1, 0f);

        if (upperBodyMask != null)
        {
            layerMixer.SetLayerMaskFromAvatarMask(1, upperBodyMask);
        }

        graph.Play();
    }

    public void PlayAttack(AnimationClip clip)
    {
        if (clip == null) return;

        if (attackPlayable.IsValid())
        {
            graph.Disconnect(attackPlayable, 0);
            attackPlayable.Destroy();
        }

        attackPlayable = AnimationClipPlayable.Create(graph, clip);
        attackPlayable.SetApplyFootIK(true);
        attackPlayable.SetDuration(clip.length);
        attackPlayable.SetTime(0);
        attackPlayable.SetSpeed(1);

        graph.Connect(attackPlayable, 0, layerMixer, 1);
        layerMixer.SetInputWeight(1, 1f);

        StartCoroutine(ResetAttackLayerAfterClip(clip.length));
    }

    private IEnumerator ResetAttackLayerAfterClip(float duration)
    {
        yield return new WaitForSeconds(duration);
        layerMixer.SetInputWeight(1, 0f);
    }

    public void ReloadWalk(AnimationClip newWalk)
    {
        if (walkPlayable.IsValid())
        {
            graph.Disconnect(walkPlayable, 0);
            walkPlayable.Destroy();
        }

        walkClip = newWalk;
        walkPlayable = AnimationClipPlayable.Create(graph, walkClip);
        walkPlayable.SetApplyFootIK(true);
        //walkPlayable.SetLoopTime(true);

        graph.Connect(walkPlayable, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f);
    }

    void OnDestroy()
    {
        graph.Destroy();
    }
}
