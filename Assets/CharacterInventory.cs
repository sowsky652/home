using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public Weapon[] weapons = new Weapon[3];
    public Transform weaponDummy;

    private GameObject weaponGo;

    public Weapon CurrentWeapon { get; private set; }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (newWeapon == null)
            return;

        UnequipWeapon();

        CurrentWeapon = newWeapon;
        weaponGo = Instantiate(CurrentWeapon.prefab, weaponDummy);
    }

    public void UnequipWeapon()
    {
        if (weaponGo == null)
            return;

        CurrentWeapon = null;
        Destroy(weaponGo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(weapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(weapons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(weapons[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UnequipWeapon();
        }
    }

}
