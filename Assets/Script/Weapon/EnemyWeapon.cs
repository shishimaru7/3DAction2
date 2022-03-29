using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の武器のクラス Playerに体当たりでダメージを与える
/// </summary>
public class EnemyWeapon : WeaponBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& other.gameObject.TryGetComponent(out PlayerStatus playerStatus))
        {
            float damage = weaponPower - playerStatus.DefensePower;
            playerStatus.HP -= damage;
        }
    }
}