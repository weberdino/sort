using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;

public class AnimationHandleLoop : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;

    private AnimationMixerPlayable mixer; // Für Loops
    private AnimationLayerMixerPlayable layerMixer; // Für Abilities

    private AnimationClipPlayable currentAbilityPlayable;

    public AnimationClip idle;
    public AnimationClip stomp;
    public AnimationClip attack;

    public AvatarMask upperBodyMask;

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();

        graph = PlayableGraph.Create("AnimGraph");
        output = AnimationPlayableOutput.Create(graph, "Output", animator);

        // LayerMixer für Loops + Abilities
        layerMixer = AnimationLayerMixerPlayable.Create(graph, 2);
        output.SetSourcePlayable(layerMixer);

        // Mixer für Loop-Animations (Idle/Walk/Stomp)
        mixer = AnimationMixerPlayable.Create(graph, 3, true);
        graph.Connect(mixer, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f); // Loops-Weight auf 1

        // Idle/Walk/Attack Setup
        var idlePlayable = AnimationClipPlayable.Create(graph, idle);
        idlePlayable.SetApplyFootIK(true);
        graph.Connect(idlePlayable, 0, mixer, 0);
        mixer.SetInputWeight(0, 1f);

        var stompPlayable = AnimationClipPlayable.Create(graph, stomp);
        stompPlayable.SetApplyFootIK(true);
        graph.Connect(stompPlayable, 0, mixer, 1);
        mixer.SetInputWeight(1, 0f);

        var attackPlayable = AnimationClipPlayable.Create(graph, attack);
        attackPlayable.SetApplyFootIK(true);
        graph.Connect(attackPlayable, 0, mixer, 2);
        mixer.SetInputWeight(2, 0f);

        // Ability-Layer initialisieren (noch leer)
        layerMixer.SetInputWeight(1, 0f);
        if (upperBodyMask != null)
        {
            layerMixer.SetLayerMaskFromAvatarMask(1, upperBodyMask);
        }

        graph.Play();
    }

    public void Play(int index)
    {
        StartCoroutine(BlendLoopTo(index));
    }

    public void PlayAbilityAnimation(AnimationClip clip, bool loop = false)
    {
        if (clip == null) return;

        if (currentAbilityPlayable.IsValid())
        {
            currentAbilityPlayable.Destroy();
        }

        currentAbilityPlayable = AnimationClipPlayable.Create(graph, clip);
        currentAbilityPlayable.SetApplyFootIK(false);
        currentAbilityPlayable.SetTime(0);
        currentAbilityPlayable.SetSpeed(1f);
        currentAbilityPlayable.SetDuration(loop ? double.PositiveInfinity : clip.length);

        graph.Connect(currentAbilityPlayable, 0, layerMixer, 1);
        layerMixer.SetInputWeight(1, 1f); // Ability-Layer aktivieren
    }

    public void StopAbility()
    {
        if (currentAbilityPlayable.IsValid())
        {
            layerMixer.SetInputWeight(1, 0f);
        }
    }

    private IEnumerator BlendLoopTo(int targetInput)
    {
        float duration = 0.1f;
        float time = 0f;

        float[] startWeights = new float[mixer.GetInputCount()];
        for (int i = 0; i < startWeights.Length; i++)
            startWeights[i] = mixer.GetInputWeight(i);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            for (int i = 0; i < mixer.GetInputCount(); i++)
            {
                float target = i == targetInput ? 1f : 0f;
                mixer.SetInputWeight(i, Mathf.Lerp(startWeights[i], target, t));
            }

            yield return null;
        }

        for (int i = 0; i < mixer.GetInputCount(); i++)
            mixer.SetInputWeight(i, i == targetInput ? 1f : 0f);
    }

    void OnDestroy()
    {
        graph.Destroy();
    }
}
