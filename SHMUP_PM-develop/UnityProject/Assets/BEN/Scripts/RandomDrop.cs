using System.Collections;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    [Space, SerializeField] private GameObject parentToDestroy;
    [SerializeField, Range(0f, 1f)] private float probability = 0.85f;
    [SerializeField, Range(0f, 1f)] private float probabilityIncreaser = 0.2f;
    [SerializeField] private GameObject[] dropArray; 
    public bool resetProbabilityOnQuit = true;

    private float value;
    Object self;
    private float initialDropProbability;

    public bool rewriteValue = false;


    private void Update()
    {
        if (rewriteValue)
        {
            probability += probabilityIncreaser;
            rewriteValue = false;
        }
    }

    public void Drop()
    {
        // probability is only changed as a copy, not the value in memory. Save(rng) won't work

        for (int index = 0; index < dropArray.Length; index++)
        {
            value = Random.Range(0f, 1f);

            if (value <= probability)
            {
                Instantiate(dropArray[index], transform.position - new Vector3(0f, 0f, -0.005f), Quaternion.identity);
                Debug.Log("-- dropped an item");
                probability = initialDropProbability;
            }
            else if (value > probability && probability < 1f)
            {
                probability += probabilityIncreaser;
                Debug.Log($"probability value -- UPDATE -- is {probability}");
            }
        }
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        Destroy(parentToDestroy, Time.fixedDeltaTime * 3f);
        self = null;

        yield return new WaitForFixedUpdate();
    }

    private void OnDisable()
    {
        if (resetProbabilityOnQuit)
            probability = initialDropProbability;
    }

}
