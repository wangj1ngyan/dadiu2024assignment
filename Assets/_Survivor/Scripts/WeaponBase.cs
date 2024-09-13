using System.Collections.Generic;
using UnityEngine;

namespace _Survivor.Scripts
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected Bullet BulletPrefab;
        
        protected List<Bullet> PooledBullets = new List<Bullet>();
        protected List<Bullet> ActiveBullets = new List<Bullet>();
        
        [SerializeField] protected int MaxBullets = 10;  
        [SerializeField] protected float BulletRecoveryTime = 15f;
        protected int CurrentBullets;
        
        protected float FireInterval = 0.5f;
        protected float TimeSinceLastFire;
        protected float BulletRecoveryTimer;

        private int _enemyIndex;
        
        protected virtual void Awake()
        {
            for (var i = 0; i < 100; ++i)
            {
                var bullet = Object.Instantiate(BulletPrefab);
                bullet.gameObject.SetActive(false);
                PooledBullets.Add(bullet);
            }
            CurrentBullets = MaxBullets;
        }

        public abstract void Fire(Vector3 direction, Vector3 position);

        protected virtual void Update()
        {
            TimeSinceLastFire += Time.deltaTime;
            BulletRecoveryTimer += Time.deltaTime;
            
            if (BulletRecoveryTimer >= BulletRecoveryTime && CurrentBullets < MaxBullets)
            {
                CurrentBullets++;
                BulletRecoveryTimer = 0;
            }
        }
        
        protected T GetBulletFromPool<T>() where T : Bullet
        {
            if (PooledBullets.Count == 0) return null;

            var instance = PooledBullets[PooledBullets.Count - 1];
            PooledBullets.RemoveAt(PooledBullets.Count - 1);

            instance.gameObject.SetActive(true);
            instance.ResetState();
            ActiveBullets.Add(instance);

            return instance as T;
        }
    }
}