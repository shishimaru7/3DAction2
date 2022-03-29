using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵Playerの基底クラス
/// </summary>
public class CharacterBase : MonoBehaviour
{
    [Header("HP")]
    [SerializeField]
    protected float hp;
    public float HP { set { hp = value; hp = Mathf.Clamp(hp, 0, maxHp); } get { return hp; } }
    [Header("最大HP")]
    [SerializeField]
    protected float maxHp;
    public float MaxHP { set { maxHp = value; } get { return maxHp; } }
    public float GetMaxHP { get { return maxHp; } }
    [Header("守備")]
    [SerializeField]
    protected float defensePower;
    public float DefensePower { set { defensePower = value; } get { return defensePower; } }
    [Header("最大守備")]
    [SerializeField]
    protected float maxDefensePower;
    public float MaxDefensePower { set { maxDefensePower = value; } get { return defensePower; } }
}