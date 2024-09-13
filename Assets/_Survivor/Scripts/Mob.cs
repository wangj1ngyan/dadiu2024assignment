using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Health))]
public class Mob : MonoBehaviour
{

    public static List<Mob> Actives = new List<Mob>();



    [SerializeField] MobSettings _settings;

    CharacterController _controller;

    Hero _target;
    private Health _health;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _target = Object.FindAnyObjectByType<Hero>();
        _health = GetComponent<Health>();
        _health.SetMaxHealth(_settings.MaxHealth);
        _health.Died.AddListener(OnMobDied);
    }
    
    void OnMobDied()
    {
        Destroy(gameObject); 
    }

    void OnEnable()
    {
        Actives.Add(this);
    }

    void OnDisable()
    {
        Actives.Remove(this);
    }
    
    void Update()
    {
        if (_target != null)
        {
            var delta = _target.transform.position - transform.position;
            if (delta.magnitude > 0)
            {
                var motion = delta.normalized * Time.deltaTime * _settings.MoveSpeed;
                _controller.Move(motion);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
}
