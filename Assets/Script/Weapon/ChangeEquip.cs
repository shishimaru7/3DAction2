using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquip : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private int equipmentNo;

    void Update()
    {
        Changeweapon();
    }

    void Changeweapon()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            equipmentNo++;
            if (equipmentNo >= weapons.Length)
            {
                equipmentNo = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == equipmentNo)
                {
                    weapons[i].SetActive(true);
                    continue;
                }
                weapons[i].SetActive(false);
            }
        }
    }
}