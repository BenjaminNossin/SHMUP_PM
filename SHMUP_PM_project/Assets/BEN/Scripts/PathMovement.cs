using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************************************************************************************************
- predefined movement (the other type is dynamic movement [player/strong AI/Seeking Head entity..], and you can go from one to the other via state/tasks)
- this component doesn't allow points outside of the pathHolder subhierarchy
********************************************************************************************************************************************************************/
public class PathMovement : MonoBehaviour
{
    [SerializeField] private Transform pathHolder;
    [Space, SerializeField, Range(1f, 8f)] private float speed = 5f;

    [SerializeField, Range(0f, 5f),] private float waitTime = .3f;
    [SerializeField] private bool loopPath = true;
    public bool IsWaiting { get; set; }

    [Space, SerializeField, Range(1f, 2f)] private float parentCapacityMultiplier = 1.5f;

    [Space, SerializeField] private List<Behaviour> behaviorsToActivateOnPathStart;
    [SerializeField, Range(0f, 5f)] private float delayBetweenCycles_StartOfPath = 0f;

    [Space, SerializeField] private List<Behaviour> behaviorsToActivateOnPathEnd;
    [SerializeField, Range(0f, 5f)] private float delayBetweenCycles_EndOfPath = 0f;

    [SerializeField] private List<SpriteRenderer> visualFeedBack;

    private int maxHierarchyCapacity;
    private Vector3[] waypoints;
    private bool canMove = true;
    private bool endOfPath = false;
    private bool valueIsStored = false;

    private float colorValue = 1f; 

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }

        if (loopPath)
            Gizmos.DrawLine(previousPosition, startPosition); // if you want to make a loop 
    }

    void OnEnable()
    {
        foreach (Behaviour behaviour in behaviorsToActivateOnPathEnd)
        {
            behaviour.enabled = false;
        }

        foreach (Behaviour behaviour in behaviorsToActivateOnPathStart)
        {
            behaviour.enabled = false;
        }

        maxHierarchyCapacity = (int)(transform.hierarchyCount * parentCapacityMultiplier);

        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y);
        }

        IsWaiting = false;
        StartCoroutine(FollowPath(waypoints));
    }

    private void Update()
    {
        if (transform.hierarchyCapacity > maxHierarchyCapacity)
            Debug.LogWarning($"hierarchy capacity is {transform.hierarchyCapacity}, which is bigger than max capacity : {maxHierarchyCapacity}");
        //set this to an amount slightly larger than actual capacity to improve performance of Destroy and SetParent
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        int targetWaypointIndex = 0;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        while (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

            IsWaiting = false;

            if (transform.position.Equals(targetWaypoint))
            {
                int previousIndex = targetWaypointIndex;
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                endOfPath = true;

                if (targetWaypointIndex == 0 && !valueIsStored)
                {
                    foreach (Behaviour behaviour in behaviorsToActivateOnPathStart)
                    {
                        behaviour.enabled = !behaviour.enabled;
                    }

                    foreach (SpriteRenderer spriteRenderer in visualFeedBack)
                    {
                        colorValue *= -1f;
                        spriteRenderer.color = new Vector4(Mathf.Clamp(colorValue, 0f, 1f),
                                                           Mathf.Clamp(-colorValue, 0f, 1f),
                                                           0f, 1f);
                    }

                    valueIsStored = true;
                    yield return new WaitForSeconds(0f);
                }

                if (!IsWaiting)
                {
                    IsWaiting = true;
                }
                yield return new WaitForSeconds(waitTime);

                if (previousIndex > targetWaypointIndex) // doesnt work well
                {
                    StartCoroutine(ActivateBehavioursOnPathStart());

                    valueIsStored = false; 
                    if (endOfPath)
                        StartCoroutine(ActivateBehavioursOnPathEnd());

                    if (!loopPath)
                        canMove = false;    
                }
            }
            yield return null;
        }
    }

    private IEnumerator ActivateBehavioursOnPathEnd()
    {
        foreach (Behaviour behaviour in behaviorsToActivateOnPathEnd)
            behaviour.enabled = true;

        yield return new WaitForSeconds(delayBetweenCycles_EndOfPath);

        foreach (Behaviour behaviour in behaviorsToActivateOnPathEnd)
            behaviour.enabled = false;
      
        endOfPath = false; 
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator ActivateBehavioursOnPathStart()
    {
        foreach (Behaviour behaviour in behaviorsToActivateOnPathStart)
        {
            behaviour.enabled = !behaviour.enabled;
        }

        foreach (SpriteRenderer spriteRenderer in visualFeedBack)
        {
            colorValue *= -1f;
            spriteRenderer.color = new Vector4(Mathf.Clamp(colorValue, 0f, 1f),
                                               Mathf.Clamp(-colorValue, 0f, 1f),
                                               0f, 1f);
        } // ugly duplicate code

        valueIsStored = true;
        yield return new WaitForSeconds(0f);
    }
}
