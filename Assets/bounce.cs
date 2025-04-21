using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bounce : MonoBehaviour, ITarget
{
    //GetNearestEnemy getNearest;
    Transform targetEnemy;

    private Vector3 endPos;
    public float jumpHeight = 5f;    // Einstellbare Höhe des Sprungs
    public float jumpSpeed = 1f;     // Geschwindigkeit des Sprungs

    public Vector3 adjustEnd;

    public GameObject explovfx;
    public Vector3 offset;

    public void Init(GetNearestEnemy target)
    {
        targetEnemy = target.closestEnemy.transform;
        Leap();
    }

    void Awake()
    {
       // getNearest = transform.parent.GetComponentInChildren<GetNearestEnemy>();
       // Leap();
    }

    void Leap()
    {
        endPos = targetEnemy.position + adjustEnd;
        StartCoroutine(LeapMotion(PlayerManager.instance.player));
    }

    private IEnumerator LeapMotion(GameObject player)
    {
        Vector3 startPos = player.transform.position;
        float duration =  20 / jumpSpeed;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            transform.position = Vector3.Lerp(startPos, endPos, t) + new Vector3(0, jumpHeight * Mathf.Sin(t * Mathf.PI), 0);
            yield return null;
        }

        var obj = Instantiate(explovfx, PlayerManager.instance.player.transform.position, Quaternion.identity);
        obj.transform.position += offset;
        obj.transform.localScale = obj.transform.localScale * 1.5f;
        Destroy(obj, .75f);
        Destroy(gameObject);

        transform.position = endPos;
        yield return null;
    }
}
