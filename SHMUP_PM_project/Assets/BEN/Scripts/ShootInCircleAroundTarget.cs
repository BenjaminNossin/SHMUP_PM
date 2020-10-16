using MyUtility.MyUtility.Checks;
using System.Collections.Generic;
using UnityEngine;

// this script should not care about destroying
// it should only translate objects of the list
// and doing nothing if list is empty

// Shoot in circle could be called once every time the script is enabled
// allowing for on/off pattern
public class ShootInCircleAroundTarget : MonoBehaviour
{
    [SerializeField] private GameObject entityToShoot;
    [Tooltip("If left empty, the target will be self"), SerializeField] private Transform sourceOfShoot;
    [SerializeField, Range(0f, 10f)] private float delayBetweenActivations = 3f;
    [SerializeField, Range(0f, 3f)] private float delayBetweenShoots = 0f;
    [SerializeField, Range(1f, 10f)] private float speed = 3f;
    [SerializeField, Range(1f, 10f)] private float secondsBeforeDestroyingEntity = 6f;
    [SerializeField, Range((int)1f, (int)50f)] private int amountOfEntitiesToShoot = 20;
    [SerializeField, Range(1f, 90f)] private float coneValue; 
    [SerializeField] private bool loop = true; 
    [SerializeField] private PathMovement movementToStop;
    public bool activateOnEachEnable = false;

    private GameObject instantiatedEntity;
    private List<GameObject> instantiatedEntitiesList;
    private float angleToAdd;
    private float angle;
    private float timer;
    private float timeOfInstantiation;
    private float startingSpeed;

    private void Start()
    {
        timer = 0f;

        if (!sourceOfShoot)
            sourceOfShoot = transform;

        if (!loop)
            delayBetweenShoots = 0f;

        angleToAdd = coneValue / Mathf.Floor(amountOfEntitiesToShoot);

        if (movementToStop)
        {
            movementToStop.StopAllCoroutines();
            movementToStop.enabled = false;
        }

        startingSpeed = speed; 
    }

    private void FixedUpdate()
    {
        if (loop)
        {
            timer = Mathf.Repeat(timer + Time.fixedDeltaTime, delayBetweenActivations);

            if (Checks.ValueIsBetweenMinAndMax(timer, 0f, Time.fixedDeltaTime + 0.01f))
            {
                angleToAdd = coneValue / Mathf.Floor(amountOfEntitiesToShoot);
                Invoke(nameof(ShootInCircle), 0f);
            }

            timeOfInstantiation = Time.time; 
        }

        if (Time.time - secondsBeforeDestroyingEntity > timeOfInstantiation)
        {
            CancelInvoke("TranslateEntities");

            foreach (GameObject item in instantiatedEntitiesList)
            {
                Destroy(item);
                // big GC and C++ entity not destroyed, only C# one :(
            }

            if (activateOnEachEnable)
                Invoke(nameof(OnDisable), Time.fixedDeltaTime * 2f);
        }
    }

    private void ShootInCircle()
    {
        instantiatedEntitiesList = new List<GameObject>();

        // calculate 0 angle from target
        angle = entityToShoot.transform.localRotation.eulerAngles.z;

        // instantiate with new angle
        for (int i = 0; i < amountOfEntitiesToShoot; i++)
        {
            instantiatedEntity = Instantiate(entityToShoot, sourceOfShoot.position, Quaternion.Euler(0f, 0f, angle - (coneValue * 0.5f)));
            angle += angleToAdd; 

            instantiatedEntitiesList.Add(instantiatedEntity);
        }

        InvokeRepeating(nameof(TranslateEntities), 0f, Time.fixedDeltaTime);
    }

    private void TranslateEntities()
    {
        speed = startingSpeed;

        foreach (GameObject item in instantiatedEntitiesList)
        {
            int numberOfEmptySlots = 0;
            try
            {
                item.transform.Translate(new Vector3(0f, 0.02f * -speed, 0f), Space.Self);
            }
            catch (MissingReferenceException)
            {
                for (int i = 0; i < instantiatedEntitiesList.Count; i++)
                {
                    if (instantiatedEntitiesList[i] == null)
                    {
                        numberOfEmptySlots++;
                        // this is enough to ignore the problem, but what impact on memory and processing to have emply references ?
                        // ignoring the problem != fixing it
                    }
                }
            }
        }
        timer += Time.fixedDeltaTime;
    }

    private void OnDisable()
    {
        CancelInvoke("TranslateEntities");

        foreach (GameObject item in instantiatedEntitiesList)
        {
            Destroy(item);
        }

        instantiatedEntitiesList = new List<GameObject>();
        StopAllCoroutines();
        this.enabled = false;
    }
}
