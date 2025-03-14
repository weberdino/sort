using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    int modifier;

    void Start ()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    // override to deal damage on Enemys
    public override void Interact(CharacterStats.atkart myType, int modifier)
    {
        base.Interact(myType, modifier);
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            playerCombat.Attack(myStats, myType, modifier);
            Debug.Log("normal");
        }
    }

    public override void FireInteract()
    {
        base.FireInteract();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            playerCombat.FireAttack(myStats);
        }
    }

    public override void LightningInteract()
    {
        base.LightningInteract();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            playerCombat.LightningAttack(myStats);
        }
    }
}