using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public GameObject respawnPoint;
    public GameObject cam;
    public EnemyStatsHandler esh;
    public GameObject pickup;

    float time;

    public GetNearestEnemy enemyTracker;
    public posUpdater posUpdater;

    private void Update()
    {
        time -= Time.deltaTime;
    }

    public void KillPlayer()
    {
        Player p = player.GetComponent<Player>();

        if(!p.CanUseRevenge())
        {
            player.transform.position = respawnPoint.transform.position;
        }
        else
        {
            PlayerStats ps = player.GetComponent<PlayerStats>();

            ps.Heal(ps.maxHealth.GetValue() / 2);
        }
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Destroy(player);//tranfrom player position, scenestartposition
    }

    public void KillSubPlayer()
    {
        //set player on cd
    }
}
