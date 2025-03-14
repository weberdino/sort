using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyclone : MonoBehaviour
{
    int trackHits;

    //Shuriken shuri;
    public AbilityObject ability;

    private Vector3 center;
    private float radius;
    bool pressed = false;
    private float countdown;

    public PlayerStats pStats;
    public Transform animParent;

    public Animator animation;
    const float locomationAnimationSmoothTime = .1f;
    public float cycloneCharge;
    public bool cyc;

    public Rotation rot;
    public CharacterAnimator ca;

    public GameObject spinvfx;

    private void Start()
    {
        pStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        //animation = GetComponent<Animator>();
        //shuri = PlayerManager.instance.player.GetComponent<Shuriken>();
    }

    public void Slow(int m)
    {
        pStats.mSpeedPerc.AddModifier(m);
    }
    public void removeSlow(int m)
    {
        pStats.mSpeedPerc.RemoveModifier(m);
    }

    public void Button(bool _pressed)
    {

        pressed = _pressed;
        spinvfx.active = _pressed;
    }

    void FixedUpdate()
    {
        pressed = AbilityManager.instance.buttonPressed;
        //GameVariables.rotaLock = 0;
        countdown -= Time.deltaTime;

       // pressed = AbilityManager.instance.buttonPressed;

        if (pressed == true)
        {
            Debug.Log(AbilityManager.instance.abilityIsReady(ability) + "cy");
            //if (AbilityManager.instance.abilityIsReady(ability))
            {            
                cycloneCharge = 1;
                cyc = true;
                Debug.Log(cycloneCharge + "cycy");
            
                if (countdown <= 0)
                {                
                        Cyclone();
                }                            
            }
        }
        else
        {
            cycloneCharge = 0;
            cyc = false;
            //GameVariables.rotaLock = 1;
        }

        //Animation
        float cycloneVel = cycloneCharge;

        if(animation == null)
        {
            animation = PlayerManager.instance.player.GetComponent<PlayerStats>().animParent.GetChild(0).GetComponent<Animator>();
        }

        animation.SetFloat("cycloneVel", cycloneVel); //, locomationAnimationSmoothTime, Time.deltaTime
        
        //animation.Play("Spin");
    }

    void Cyclone()
    {
        //rot.Timeup();

        //animation.SetFloat("cycöpo")

        center = transform.position + new Vector3(0, -3, 0);
        radius = 3 + pStats.aoeMod.GetValue();

        Collider[] colliders = Physics.OverlapSphere(center, radius);

        int callOnce = colliders.Length - 1;

        /*for(int i = callOnce; i < colliders.Length; i++)
        {
            if(trackHits == 3)
            {
              trackHits = 0;
            }
            else
            {
                trackHits++;
                Debug.Log(trackHits + "th");
            }
        }*/

        foreach (Collider nearbyObject in colliders)
        {
            //shuri.CountUp(); // change to make it more viable for all attacks

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            Interactable dest = nearbyObject.GetComponent<Interactable>();
            if (dest != null)
            {
                dest.Interact(CharacterStats.atkart.hit, ((int)pStats.modifier));
                float val = pStats.attackSpeed.GetValue();
                countdown = 1f / (1 + val / 100);
            }

            if(nearbyObject.name == "Bouncer(Clone)" )
            {
               // BouncingProjectile bp = nearbyObject.gameObject.GetComponent<BouncingProjectile>();

                //bp.Serve(this.gameObject);
            }

        }       
    }

    /*void Animation()
    {
        float cycloneVel = cycloneCharge / 10;

        animation.SetFloat("cycloneVel", cycloneVel, locomationAnimationSmoothTime, Time.deltaTime);
    }*/
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, radius);
    }
}
