using UnityEngine;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool alive;
    void Awake()
    {
        CurrentHealth = MaxHealth;
        alive = true;
    }

    public void TakeDamage(float damage)
    {
        
        if (CurrentHealth > damage)
        {
            CurrentHealth -= damage;
        }
        if (CurrentHealth < damage)
        {
            CurrentHealth = 0;
            alive = false;
        }
    }
    void Update()
    {
        
    }
}
