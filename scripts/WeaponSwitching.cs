using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    public int SelectedWeapon = 0;
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int prevSelectedWeapon = SelectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") >0f)
        {
            if (SelectedWeapon >= transform.childCount - 1)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") <0f)
        {
            if (SelectedWeapon <= 0)
            {
                SelectedWeapon = transform.childCount - 1;
            }
            else
            {
                SelectedWeapon--;
            }
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2)) 
        {
            SelectedWeapon = 1; 
        }

        if(prevSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform Weapon in transform)
        {
            if(i==SelectedWeapon)
            {
                Weapon.gameObject.SetActive(true);
            }
            else
            {
                Weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
