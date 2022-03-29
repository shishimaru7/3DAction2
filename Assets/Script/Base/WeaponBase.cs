using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵味方の武器の基底クラス
/// </summary>
public class WeaponBase : MonoBehaviour
{
    [Header("武器攻撃力")]
    [SerializeField]
    protected float weaponPower;
}