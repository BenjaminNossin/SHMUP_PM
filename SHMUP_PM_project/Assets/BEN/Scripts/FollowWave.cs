using System;
using System.Collections;
using UnityEngine;

public class FollowWave : MonoBehaviour
{
    [Header("Automation")]
    [SerializeField] private bool automated = true;
    [Tooltip("only fill this if automated is toggled on")]
    [SerializeField] private BasicAIBrain basicBrain; // this is added even to the player's instance script.. cut the input source from the shooting logic

    [Space, SerializeField] private Transform[] firePoints;
    // -- WARNING -- all the following is specific to the weapon, not the shooting mechanic. Move it somewhere else 
    [Space, SerializeField] private GameObject entityToShoot;

    [SerializeField, Range(0f, 10f)] private float delayBetweenFire = 0.2f;
    [SerializeField, Range(0f, 5f)] private float delayBetweenFireOnEnable = 2f;

    bool attacking;
    private bool canShoot = true;

    void Update()
    {
        if (!automated)
        {
            Debug.Log($"attacking as not automated");
            attacking = Input.GetMouseButton(0);
            if (attacking && canShoot)
            {
                ShootProjectile();
                StartCoroutine(CoolDown());
            }
        }
        else if (basicBrain)
        {
            attacking = basicBrain.CanShoot;
        }
        else
        {
            try { GetComponentInParent<BasicAIBrain>(); }
            catch (Exception)
            {
                GetComponent<MiniBossBrain>();
            }
        }

        if (attacking && canShoot)
        {
            Debug.Log("wave shooting");
            Debug.Log($"attacking is {attacking}"); 
            ShootProjectile();
            StartCoroutine(CoolDown());
        }

    }

    void ShootProjectile()
    {
        foreach (Transform point in firePoints)
            Instantiate(entityToShoot, point.position, point.rotation);

    }

    IEnumerator CoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(delayBetweenFire);
        canShoot = true;
    }
}
