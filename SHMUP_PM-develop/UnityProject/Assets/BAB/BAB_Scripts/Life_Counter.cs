using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Life_Counter : MonoBehaviour
{
    public static int lifeValue = 10;
    public Text life;
    public Player_HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        life.text = "" + lifeValue;
    }
}
