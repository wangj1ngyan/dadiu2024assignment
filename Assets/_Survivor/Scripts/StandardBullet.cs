using _Survivor.Scripts;
using UnityEngine;

public class StandardBullet : Bullet
{

    [SerializeField] private float normalDamage = 10f;

    protected override void Awake()
    {
        base.Awake();
        damage = normalDamage;
    }
    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        
        HitEffect();
    }

    private void HitEffect()
    {
        // TODO: 标准子弹的击中效果
    }
}