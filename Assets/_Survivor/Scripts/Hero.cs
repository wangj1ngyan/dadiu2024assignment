using _Survivor.Scripts;
using UnityEngine;

[RequireComponent(typeof(HeroMotor))]
[RequireComponent(typeof(WeaponManager))]
public class Hero : MonoBehaviour
{
    public static System.Action<Hero, float> DamageTaken;
    private WeaponManager _weaponManager;


    [SerializeField] Health _health;
    HeroMotor _motor;

    public Health Health => _health;

    public static Hero Instance;

    void Awake()
    {
        Instance = this;

        _motor = GetComponent<HeroMotor>();

        _health.Died.AddListener(() => 
        {
            _motor.enabled = false;
        });

        _health.DamageTaken.AddListener(args =>
        {
            DamageTaken?.Invoke(this, args.CurrentRatio);
        });

    
    }

    void Start()
    {
        DamageTaken?.Invoke(this, _health.currentHealth);
        _weaponManager = GetComponent<WeaponManager>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weaponManager.FireCurrentWeapon();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _weaponManager.SwitchWeapon(0); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _weaponManager.SwitchWeapon(1); 
        }
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        _motor.Teleport(position, rotation);
    }
}
