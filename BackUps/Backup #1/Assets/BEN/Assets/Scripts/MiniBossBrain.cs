using System;
using UnityEngine;

// this pseudo-FSM is ok for scripted behaviours, 
// but not flexible or scalable + doesnt allow for complex/emergent/dynamic behaviour
public class MiniBossBrain : MonoBehaviour
{
    public static event Action OnPlayerDetection;
    public static event Action OnHealthUnderFirstFloor;
    public static event Action OnHealthUnderSecondFloor;
    public static event Action OnHealthUnderThirdFloor;

    [Tooltip("percent of maxHealth under which the mini boss goes to state 2")]
    [SerializeField, Range(0f, 1f)] private float healthFirstFloor = 0.75f;
    public float firstFloor;
    public float secondFloor;
    public float thirdFloor;

    public BoxCollider2D selfCollider;
    public Health health;

    void Start()
    {
        firstFloor = health.MaxHP * healthFirstFloor;
        secondFloor = firstFloor * 0.5f;
        thirdFloor = secondFloor * 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D detectedCollider2D) // change with Vector2.Distance
    {
        if (detectedCollider2D.CompareTag("Player"))
        {
            try { OnPlayerDetection(); }
            catch (Exception) { }

            selfCollider.enabled = true;
            selfCollider.tag = "Killable";
            health.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.CurrentHP -= 1;
        }

        if (health.CurrentHP <= firstFloor)
            try { OnHealthUnderFirstFloor(); }
            catch (Exception) { }

        if (health.CurrentHP <= secondFloor)
            try { OnHealthUnderSecondFloor(); }
            catch (Exception) { }

        if (health.CurrentHP <= thirdFloor)
            try { OnHealthUnderThirdFloor(); }
            catch (Exception) { }
    }
}
