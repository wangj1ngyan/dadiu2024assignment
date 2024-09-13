using UnityEngine;

namespace _Survivor.Scripts
{
    public class SpecialBullet : Bullet
    {
        [SerializeField] private float explosionRadius = 10f;
        [SerializeField] private float explosionDamage = 50f;
        [SerializeField] private float maxRange = 100f; 
        private Vector3 initialPosition;
        
        public ObjectPool<SpecialBullet> BulletPool { get; set; }

        protected override void Awake()
        {
            base.Awake();
            damage = 15f;
            initialPosition = transform.position;
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            if (Vector3.Distance(initialPosition, transform.position) > maxRange)
            {
                IsDone = true;
                ReturnToPool();
            }
        }
        
        protected override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);
            Explode();
            ReturnToPool();
        }
        
        private void ReturnToPool()
        {
            BulletPool?.ReturnToPool(this); 
        }

        private void Explode()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent<Mob>(out var mob))
                {
                    mob.TakeDamage(explosionDamage);
                }
            }
        }
    }

}
