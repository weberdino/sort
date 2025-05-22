using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimationHandle : MonoBehaviour
{
    private PlayableGraph graph;
    private AnimationPlayableOutput output;
    private AnimationClipPlayable currentPlayable;
    public AnimationClip clip;

    void Awake()
    {
        // Graph vorbereiten
        graph = PlayableGraph.Create("AnimationGraph");
        output = AnimationPlayableOutput.Create(graph, "Animation", GetComponentInParent<Animator>());
        graph.Play();
        Play(clip);
    }

    private void OnEnable()
    {
        Play(clip);
    }

    public void Play(AnimationClip clip)
    {
        Debug.Log("test");
        if (currentPlayable.IsValid())
        {
            currentPlayable.Destroy();
        }

        currentPlayable = AnimationClipPlayable.Create(graph, clip);
        output.SetSourcePlayable(currentPlayable);
    }

    void OnDestroy()
    {
        graph.Destroy();
    }
}
