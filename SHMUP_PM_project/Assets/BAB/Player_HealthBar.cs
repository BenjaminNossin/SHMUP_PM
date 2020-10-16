using UnityEngine;
using UnityEngine.UI;

public class Player_HealthBar : MonoBehaviour
{
    public Slider slider;

    private void OnEnable()
    {
        SetMaxHealth(); 
    }

    public void SetMaxHealth()
    {
        slider.maxValue = Player_Controller.maxHealth;
        slider.value = slider.maxValue;
    }

    public void UpdateHealth (float health)
    {
        slider.value = health;   
    }
}
