using UnityEngine;
using UnityEngine.UI;

public class Life_Counter : MonoBehaviour
{
    public static int lifeValue = 10;
    public Text life;

    void Start()
    {
        life = GetComponent<Text>();
    }

    void Update()
    {
        life.text = "" + lifeValue;
    }
}
