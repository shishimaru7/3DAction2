using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player武器の基底クラス
/// </summary>
public class PlayerWeaponBase : WeaponBase
{
    protected SlimeStatus slimeStatus;
    protected BatStatus batstatus;
    protected RabbitStatus rabbitstatus;
    protected GhostStatus ghoststatus;
    AudioSource audioSource;
    //会心音
    public AudioClip critical;
    protected PlayerStatus playerstatus;
    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerstatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        Addpower();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.TryGetComponent(out EnemyStatusBase enemyStatusBase))
        {
            //会心設定
            if (Random.Range(0, 100) < 30)
            {
                //会心音
                audioSource.PlayOneShot(critical);

                float damage = Mathf.Clamp(playerstatus.AttackPower - enemyStatusBase.DefensePower, 0, playerstatus.AttackPower * 1.5f);
                enemyStatusBase.HP -= damage;
                Debug.Log("会心" + other.gameObject.name + "にダメージ" + damage + "与えた");
            }
            else
            {
                float damage = Mathf.Clamp(playerstatus.AttackPower - enemyStatusBase.DefensePower, 0, weaponPower);
                enemyStatusBase.HP -= damage;
                Debug.Log(other.gameObject.name + "にダメージ" + damage + "与えた");
            }
        }
    }
    protected virtual void Addpower()
    {
        playerstatus.Weaponpowersword(weaponPower);
    }
}