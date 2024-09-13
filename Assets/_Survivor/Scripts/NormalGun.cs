using UnityEngine;

namespace _Survivor.Scripts
{
    public class NormalGun : WeaponBase
    {
        public override void Fire(Vector3 direction, Vector3 position)
        {
            if (CurrentBullets <= 0) return;

            StandardBullet bullet = GetBulletFromPool<StandardBullet>();
            if (bullet == null) return;

            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.transform.position = position + direction;

            CurrentBullets--;
        }
    }
}