using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimationHandleTest : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;
    private AnimationMixerPlayable mixer;
    private Animator animator;


    private Dictionary<string, (AnimationClipPlayable playable, int index)> playables = new();

    private void Start()
    {
        animator = GetComponent<Animator>();
        graph = PlayableGraph.Create("AnimGraph");
        output = AnimationPlayableOutput.Create(graph, "Output", animator);
        mixer = AnimationMixerPlayable.Create(graph, 1, true);
        output.SetSourcePlayable(mixer);
    }

    public void AddClip(string name, AnimationClip clip)
    {
        if (playables.ContainsKey(name)) return;

        var playable = AnimationClipPlayable.Create(graph, clip);
        int inputIndex = playables.Count;
        //mixer.SetInputCount(inputIndex + 1);
        graph.Connect(playable, 0, mixer, inputIndex);
        mixer.SetInputWeight(inputIndex, 1);

        playables.Add(name, (playable, inputIndex));

        graph.Play();
    }

    public void Play(string name)
    {
        Debug.Log(!playables.ContainsKey(name));
        if (!playables.ContainsKey(name)) return;

        StartCoroutine(BlendTo(name));
    }

    private IEnumerator BlendTo(string name)
    {
        int targetInput = 0;

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
}
