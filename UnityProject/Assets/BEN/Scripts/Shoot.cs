using System;
using System.Collections;
using UnityEngine;

// TODO : add a precision factor (offset from the perfect shot at 0° from target)
public class Shoot : MonoBehaviour
{
    [Header("Automation")]
    [SerializeField] private bool automated = true;
    [Tooltip("only fill this if automated is toggled on")]
    [SerializeField] private BasicAIBrain basicBrain; // this is added even to the player's instance script.. cut the input source from the shooting logic

    [Header("Manual")]
    [SerializeField] private bool animated = false;
    [Tooltip("only fill this if animated is toggled on")]
    [SerializeField] private Animator animator;

    [Space, SerializeField] private Transform[] firePoints;

    // -- WARNING -- all the following is specific to the weapon, not the shooting mechanic. Move it somewhere else 
    [Space, SerializeField] private GameObject entityToShoot;
    [SerializeField, Range(0f, 10f)] private float delayBeforeFirstShoot = 0.2f;

    [SerializeField, Range(2f, 50f)] private float bulletForce = 15f;
    [SerializeField, Range(0f, 10f)] private float delayBetweenFire = 0.2f;
    [SerializeField, Range(0f, 1f)] private float delayBetweenFireOnEnable = 0.5f;

    bool attacking;
    private bool canShoot = true;
    [SerializeField] private bool shootOnEachEnable = false;

    private void OnEnable()
    {
        if (shootOnEachEnable)
            Invoke(nameof(ShootProjectile), delayBetweenFireOnEnable);
    }

    void Update()
    {
        if (!automated)
        {
            attacking = Input.GetMouseButton(0);
            if (attacking && canShoot)
            {
                ShootProjectile();
                UpdateAnimation();
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
            Invoke(nameof(ShootProjectile), delayBeforeFirstShoot);
            StartCoroutine(CoolDown());
        }

    }

    void ShootProjectile()
    {
        foreach (Transform point in firePoints)
        {
            if (point)
            {
                GameObject bullet = Instantiate(entityToShoot, point.position, point.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(point.up * bulletForce, ForceMode2D.Impulse);
            }
        }
    }

    void UpdateAnimation()
    {
        try { animator.SetTrigger("shoot"); }
        catch (Exception)
        {
            Debug.LogWarning("---WARNING--- you need to drag an animator component !");
        }
    }

    IEnumerator CoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(delayBetweenFire);
        canShoot = true;
    }
}
