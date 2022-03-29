using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// CharacterBaseから継承し敵専用の基底クラス
/// </summary>
public class EnemyStatusBase : CharacterBase
{
    [Header("アイテムドロップ")]
    [SerializeField]
    protected GameObject item;

    [Header("所持経験値")]
    [SerializeField]
    protected int expPoint;
    public int GetExpPoint {get{ return expPoint; } }
    [Header("対象")]
    [SerializeField]
    protected GameObject player;

    [Header("追跡距離0なら接近")]
    [SerializeField]
    protected float trackingrange;

    [Header("追跡速度")]
    [SerializeField]
    protected float tracking;

    protected virtual void Start()
    {
        player = GameObject.Find("Player");
    }
    protected virtual void Update()
    {
        //Playerに向く
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) > trackingrange)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * tracking);
        }
    }
    //剣が当たった時の処理
    protected virtual void OnTriggerEnter(Collider other)
    {
        EnemyDie();
    }

    //死亡処理
    protected virtual void EnemyDie()
    {
        if (hp <= 0)
        {
            // ↓Interfaceのクラス　自作変数　＝　                 　。ゲットコンポ＜Interfaceクラス＞
            //フィールドで変数宣言可能
            Expmanager exp = GameObject.Find("EXPManager").GetComponent<Expmanager>();
            //もし経験値が無い事が無い（あるなら）Interfaceに経験値が一旦渡り、それからstockのあるScriptに渡る
            if (exp != null)
            {
                exp.Slimeexp(expPoint);
                        Debug.Log(gameObject.name +"経験値" +expPoint);
            }

            //アイテムドロップ(ランダム)
            //その場でドロップ  transform.position;はその場（今立っている）の意味
            if (Random.Range(0, 100) < 10)
            {
                Vector3 pos = transform.position;
                Instantiate(item, new Vector3(pos.x, 2, pos.z), Quaternion.identity);
                //   Debug.Log("アイテムドロップ");
            }
            Destroy(gameObject);
        }
    }
}