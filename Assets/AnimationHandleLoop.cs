using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;

public class AnimationHandleLoop : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;
    private AnimationMixerPlayable mixer;
    AnimationClipPlayable[] clipPLayables;
    private AnimationClipPlayable idlePlayable;
    private AnimationClipPlayable attackPlayable;
    private AnimationClipPlayable stompclip;

    public List<AnimationClip> clips;
    public AnimationClip Clip;
    public AnimationClip idle;
    public AnimationClip stomp;

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        graph = PlayableGraph.Create("AnimGraph");
        output = AnimationPlayableOutput.Create(graph, "Output", animator);

        mixer = AnimationMixerPlayable.Create(graph, 3, true);
        output.SetSourcePlayable(mixer);

        idlePlayable = AnimationClipPlayable.Create(graph, idle);
        idlePlayable.SetApplyFootIK(true);

        attackPlayable = AnimationClipPlayable.Create(graph, Clip);
        attackPlayable.SetApplyFootIK(true);

        stompclip = AnimationClipPlayable.Create(graph, stomp);
        stompclip.SetApplyFootIK(true);

        graph.Connect(idlePlayable, 0, mixer, 0);
        graph.Connect(attackPlayable, 0, mixer, 1);
        graph.Connect(stompclip, 0, mixer, 2);


        mixer.SetInputWeight(0, 1f);
        mixer.SetInputWeight(1, 1f);
        mixer.SetInputWeight(2, 1f);

        graph.Play();
    }

    public void Play(int index)
    {
        StartCoroutine(BlendTo(index));
    }

    private IEnumerator BlendTo(int targetInput)
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
