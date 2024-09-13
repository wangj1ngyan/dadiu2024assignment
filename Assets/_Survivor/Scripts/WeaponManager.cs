using UnityEngine;

namespace _Survivor.Scripts
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] public WeaponBase[] weapons; 
        private int _currentWeaponIndex = 0;

        private void Start()
        {
            weapons = FindObjectsOfType<WeaponBase>();
            
            if (weapons == null || weapons.Length == 0)
            {
                Debug.LogError("Weapons array is not initialized or is empty!");
                return;
            }
            
            SelectWeapon(0);
        }

        private void Update()
        {
            WeaponSwitch();
        }
        
        public void FireCurrentWeapon()
        {
            Vector3 direction = transform.forward; 
            Vector3 position = transform.position;
            
            weapons[_currentWeaponIndex].Fire(direction, position);
        }
        
        private void WeaponSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(1);
            }
        }
        
        public void SwitchWeapon(int weaponIndex)
        {
            if (weaponIndex != _currentWeaponIndex && weaponIndex >= 0 && weaponIndex < weapons.Length)
            {
                weapons[_currentWeaponIndex].gameObject.SetActive(false);
                
                _currentWeaponIndex = weaponIndex;
                SelectWeapon(_currentWeaponIndex);
            }
        }
        
        public void SelectWeapon(int index)
        {
            
            if (index < 0 || index >= weapons.Length)
            {
                Debug.LogError($"Invalid weapon index: {index}. The index is out of bounds.");
                return;
            }
            
            weapons[index].gameObject.SetActive(true);
        }
    }
}