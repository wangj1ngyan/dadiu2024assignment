using UnityEngine;

namespace _Survivor.Scripts
{
    public class SpecialWeapon : WeaponBase
    {
        [SerializeField] int bulletsPerShot = 3; 

        protected override void Awake()
        {
            MaxBullets = 9;
            BulletRecoveryTime = 5f; 
            base.Awake();
        }

        public override void Fire(Vector3 direction, Vector3 position)
        {
            if (CurrentBullets < bulletsPerShot) return; 

            for (int i = 0; i < bulletsPerShot; i++)
            {
                SpecialBullet bullet = GetBulletFromPool<SpecialBullet>();
                if (bullet == null) return;

                bullet.transform.rotation = Quaternion.LookRotation(direction);
                bullet.transform.position = position + direction;

                bullet.ResetState();
            }
        }
    }
}