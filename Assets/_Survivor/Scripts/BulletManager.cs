using UnityEngine;

namespace _Survivor.Scripts
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private SpecialBullet specialBulletPrefab;
        [SerializeField] private StandardBullet standardBulletPrefab;
        private ObjectPool<StandardBullet> _standardBulletPool;
        private ObjectPool<SpecialBullet> _specialBulletPool;

        void Start()
        {
            _standardBulletPool = new ObjectPool<StandardBullet>(standardBulletPrefab, 10);
            _specialBulletPool = new ObjectPool<SpecialBullet>(specialBulletPrefab, 10);
        }

        public StandardBullet GetStandardBullet()
        {
            return _standardBulletPool.Get();
        }

        public SpecialBullet GetSpecialBullet()
        {
            return _specialBulletPool.Get();
        }


        public void ReturnToPool(Bullet bullet)
        {
            if (bullet is StandardBullet standardBullet)
            {
                _standardBulletPool.ReturnToPool(standardBullet);
            }
            else if (bullet is SpecialBullet specialBullet)
            {
                _specialBulletPool.ReturnToPool(specialBullet);
            }
        }
    }
}