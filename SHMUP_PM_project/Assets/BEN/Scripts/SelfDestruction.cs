using System.Collections;
using UnityEngine;

// when triggered, destroy itself after a short delay
// for the moment, the destruction is just automated after shooting
public class SelfDestruction : MonoBehaviour
{
    [Range((int)0f, (int)5f)] public float delayBeforeDestruction = 0.5f;
    [SerializeField] private bool selfDestructionIsActivated = true;
    Object selfEntity;

    private void OnEnable()
    {
        if (selfDestructionIsActivated)
            SelfDestroy();
    }

    IEnumerator SelfDestroy()
    {
        selfEntity = gameObject;
        Destroy(gameObject, delayBeforeDestruction);
        selfEntity = null;
        yield return new WaitForFixedUpdate();
    }
}
