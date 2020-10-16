using UnityEngine;
using UnityEngine.UI;

public class Life_Counter : MonoBehaviour
{
    public static int lifeValue;
    public Text life;

    public void OnEnable()
    {
        lifeValue = 5; 
    }

    void Start()
    {
        life = GetComponent<Text>();
    }

    void Update()
    {
        life.text = "" + lifeValue;
    }
}
