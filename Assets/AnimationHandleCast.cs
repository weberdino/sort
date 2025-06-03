using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class AnimationHandleCast : MonoBehaviour
{
    public Animator animator;

    private PlayableGraph playableGraph;
    private AnimationPlayableOutput playableOutput;
    private AnimationMixerPlayable mixer;

    private AnimationClipPlayable currentClipPlayable;
    private AnimationClip currentClip;

    //avatarMask
    private AnimationLayerMixerPlayable layerMixer;
    public AvatarMask upperBodyMask;

    void Start()
    {
        playableGraph = PlayableGraph.Create("AbilityGraph");
        playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);

        mixer = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixer);

        playableGraph.Play();
    }

    public void PlayAbilityAnimation(AnimationClip clip, bool shouldLoop = false)
    {
        if (clip == null) return;

        // Stop previous clip
        if (currentClipPlayable.IsValid())
        {
            mixer.DisconnectInput(0);
            currentClipPlayable.Destroy();
        }

       // currentClip = clip;

        currentClipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        currentClipPlayable.SetApplyFootIK(true);
        currentClipPlayable.SetDuration(clip.length);
        currentClipPlayable.SetTime(0);
        clip.wrapMode = shouldLoop ? WrapMode.Loop : WrapMode.Once;

        currentClipPlayable.SetSpeed(1);

        mixer.ConnectInput(0, currentClipPlayable, 0);
        mixer.SetInputWeight(0, 1);

        //avatarMask:
        layerMixer = AnimationLayerMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(layerMixer);
        playableGraph.Connect(currentClipPlayable, 0, layerMixer, 0);
        layerMixer.SetInputWeight(0, 1f);
        if(upperBodyMask != null) {
            layerMixer.SetLayerMaskFromAvatarMask(1, upperBodyMask);
        }

    }

    private void OnDisable()
    {
        playableGraph.Destroy();
    }
}
