using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] enemyPefab;

    //少ないとどんどん出る（最大値と同じ）
    [Header("召喚時間")]
    [SerializeField]
    protected float span;

    //０カウントから始まる（現在値と同じ）
    [Header("チャージ時間")]
    [SerializeField]
    protected float time;

    // 出現ごとに出現が早くなる
    private void Update()
    {
        //*で遅くすることも出来る

        time += Time.deltaTime;

        if (time > span)
        {
            //スタートにtime = 0 すると一気に生成されるがif文内なら条件満たしたときからスタートするからゆっくり生成される
            //↓必ずつける無いと一気に生成
            time = 0;
            span += Time.deltaTime;
            // spanの値マイナスだと一気に生成　設定値高くする ↓加算数出現ごとに変化　値小さくすればspan短くてもOK
            span = span + (Time.deltaTime) - 0.1f;
            int random = Random.Range(0, 3);
            GameObject appearance = Instantiate(enemyPefab[random]);
            //出現場所変化付ける
            appearance.transform.position = new Vector3(Random.Range(-35, 35), Random.Range(1, 2), Random.Range(-35, 35));
        }
        //マイナス暴発防止
        if (span <= 0)
        {
            Destroy(gameObject);
        }
    }
}