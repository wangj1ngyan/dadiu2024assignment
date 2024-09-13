using UnityEngine;

namespace _Survivor.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Bullet : MonoBehaviour
    {
        public event System.Action HitMob;

        public bool IsDone { get; protected set; }

        [SerializeField] protected float speed = 20;
        [SerializeField] protected float duration = 2;
        [SerializeField]protected float damage = 10f;
    
        protected float ElapsedTime;
        protected Rigidbody Rigidbody;
        
        public ObjectPool<Bullet> BulletPool { get; set; }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
    
        public virtual void ResetState()
        {
            ElapsedTime = 0;
            IsDone = false;
            gameObject.SetActive(true);
        }
    
        public virtual void UpdateState()
        {
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime > duration)
            {
                IsDone = true;
                ReturnToPool();
            }

            Rigidbody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        
        private void ReturnToPool()
        {
            ResetState();
            BulletPool?.ReturnToPool(this);
        }
    
        protected virtual void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<Mob>(out var mob))
            {
                mob.TakeDamage(damage);
                IsDone = true;
                HitMob?.Invoke();
                ReturnToPool();
            }
        }

    }
}
