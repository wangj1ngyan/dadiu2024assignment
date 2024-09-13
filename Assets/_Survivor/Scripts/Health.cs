using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public UnityEvent Died = new UnityEvent();

    public struct DamageTakenArgs
    {
        public float CurrentRatio;
    }

    public UnityEvent<DamageTakenArgs> DamageTaken = new UnityEvent<DamageTakenArgs>();


    public float currentHealth;
    public float maxHealth = 100f;


    int _countPerFrame;
    int _lastCountPerFrame;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    
    void FixedUpdate()
    {
        _lastCountPerFrame = _countPerFrame;
        _countPerFrame = 0;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0 || currentHealth <= 0) return; 

        var prevHealth = currentHealth;
        
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        
        DamageTaken?.Invoke(new DamageTakenArgs
        {
            CurrentRatio = currentHealth / maxHealth,
        });
        
        if (prevHealth > 0 && currentHealth <= 0)
        {
            Die(); 
        }

        _countPerFrame += 1; 
    }
    
    public void Heal(float amount)
    {
        if (amount <= 0 || currentHealth <= 0) return; 
        
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        
        DamageTaken?.Invoke(new DamageTakenArgs
        {
            CurrentRatio = currentHealth / maxHealth,
        });
    }
    
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Died?.Invoke();  
        Destroy(gameObject);
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Min(currentHealth, maxHealth); 
    }
    
}
