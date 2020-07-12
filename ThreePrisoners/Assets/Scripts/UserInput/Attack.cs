using UnityEngine;

namespace UserInput
{
    class Attack : MonoBehaviour
    {
        [SerializeField]
        private bool isPlayer = false;

        private WeaponInventory weaponInventory;
        private bool input;

        public void Start()
        {
            weaponInventory = GetComponent<WeaponInventory>();
        }

        private void Update()
        {
            if(isPlayer)
            {
                input = Input.GetButton("Fire1");
            }

            if(input)
            {
                weaponInventory.GetActiveWeapon().Fire();
            }
        }

        public void SetInput(bool inp)
        {
            if (!isPlayer)
            {
                input = input;
            }
            else
            {
                Debug.LogError("AttackScript: tried to set input for player object");  //error handling
            }
        }
    }
}