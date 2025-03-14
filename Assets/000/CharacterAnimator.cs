using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replacableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    //public WeaponAnimation[] weaponAnimations;
    //Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    const float locomationAnimationSmoothTime = .1f;
    CharacterController controller;
    public Animator animator;
    CharacterStats myStats;
    Vector3 Velocity;
    float speedPercent;

    public cyclone cyc;
    bool cy;

    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myStats = GetComponent<CharacterStats>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;

        //combat.OnAttack += OnAttack();

        //animator.Play("sit");
    }

    // Update is called once per frame
    void Update()
    {
        cy = cyc.cyc;
        Velocity = controller.velocity;
        Velocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        speedPercent = Velocity.magnitude;

       // Debug.Log(GameVariables.rotaLock + "rota");
        if (cyc.cycloneCharge == 0)
        {
            animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("speedPercent", 0);
        }

        animator.SetBool("inCombat", combat.InCombat);
    }

    void wakeNewChar(Animator anim)
    {
        animator = anim;
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replacableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    /*[System.Serializable]
    public struct WeaponAnimation
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }*/
        
}
