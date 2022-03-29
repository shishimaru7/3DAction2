using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  //////////////////////////////////////////////////↓Interface使う時忘れるな
public class EXPManager : MonoBehaviour, Expmanager
{
    //   空のオブジェクトに装着
    //  エネミーが死んだら空のオブジェクトが経験値貰い経験値管理
    //レベルアップの値はPlayerに渡す必要ない
    //Playerのコンポ―ネントが出来れば渡すことになる

    [SerializeField]
    private int level;
    public int GetLevel { get{ return level; } }
    [SerializeField]
    private int expstock;
    [SerializeField]
    private int nextlevelpoint;

    //経験値受け取りの為のクラス
    SlimeStatus slimeStatus;
    BatStatus batstatus;
    GhostStatus ghoststatus;
    RabbitStatus rabbitstatus;

    //Playerレベルアップのためのstatusクラス
    PlayerStatus playerstatus;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Text HPtext;

    private AudioSource audioSource;

    public AudioClip LevelupSE;

    public AudioClip Levelvoice;

    public AudioClip BossGO;

    private void Start()
    {
        level = 1;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Levelparameter();
        levelText.text = "Level：" + level;
        HPtext.text = "HP:" + playerstatus.HP + "/" + playerstatus.GetMaxHP;
    }
    //敵から経験値を貰う処理
    public void Slimeexp(int expstok)
    {
        //イベントのたびにクラス取得
        slimeStatus = GameObject.Find("Slime").GetComponent<SlimeStatus>();

        expstock += slimeStatus.GetExpPoint;
    }
    public void Batexp(int expstok)
    {
        batstatus = GameObject.Find("Bat").GetComponent<BatStatus>();

        expstock += batstatus.GetExpPoint;
    }

    public void Ghostexp(int expstok)
    {
        ghoststatus = GameObject.Find("Ghost").GetComponent<GhostStatus>();

        expstock += ghoststatus.GetExpPoint;
    }

    public void Rabbitexp(int expstok)
    {
        rabbitstatus = GameObject.Find("Rabbit").GetComponent<RabbitStatus>();

        expstock += rabbitstatus.GetExpPoint;
    }

    public void Levelparameter()
    {
        playerstatus = GameObject.Find("Player").GetComponent<PlayerStatus>();

        //次の経験値ポイントをストック（多くなったら）が超えたらレベルアップ
        if (expstock >= nextlevelpoint)
        {
            level++;

            //数値低くすると上がりやすい数値あげればレベル上がりにくい
            nextlevelpoint = Mathf.RoundToInt(nextlevelpoint * 2f);

            //Random.Range使えるからランダムに上げられる　*の場合数値低いと元の数値が下がる
            playerstatus.MaxAttackPower = Mathf.RoundToInt(playerstatus.MaxAttackPower * 1.1f);

            playerstatus.AttackPower = playerstatus.MaxAttackPower;

            //*の場合数値低いと元の数値が下がる
            playerstatus.MaxDefensePower = Mathf.RoundToInt(playerstatus.MaxDefensePower * 1.1f);

            playerstatus.DefensePower = playerstatus.MaxDefensePower;
            //  Debug.Log("守備最大");

            //*の場合数値低いと元の数値が下がる
            playerstatus.MaxHP = Mathf.RoundToInt(playerstatus.MaxHP * 1.1f);

            //レベルアップしたら体力満タンに出来る
            playerstatus.HP = playerstatus.MaxHP;

            audioSource.PlayOneShot(LevelupSE);

            audioSource.PlayOneShot(Levelvoice);

            if (level == 10)
            {
                audioSource.PlayOneShot(BossGO);
            }
        }
    }
}