using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : CharacterBase
{
    [Header("player本体の攻撃力")]
    [SerializeField]
    private float attackPower;
    public float AttackPower { set { attackPower = value; } get { return attackPower; } }
    [Header("Player本体の最大攻撃力")]
    [SerializeField]
    private float maxAttackPower;
    public float MaxAttackPower { set{ maxAttackPower = value; } get { return maxAttackPower; } }
    [SerializeField]
    private float stockAttackPower;
    [SerializeField]
    private float stockDefensePower;
    [Header("SP強化回数")]
    [SerializeField]
    private float spCount;

    [Header("強化時間")]
    [SerializeField]
    private float timeCount;

    [Header("速さ")]
    [SerializeField]
    private float moveSpeed;
    [Header("回転速度")]
    [SerializeField]
    private float rotateSpeed;

    [Header("SPのイメージ")]
    [SerializeField]
    private Image SPImage;

    [Header("SP回数")]
    [SerializeField]
    private GameObject[] SPAmountObjs;

    [Header("強化時間テキスト")]
    [SerializeField]
    private Text SPCountDownText;

    [Header("残SPのテキスト")]
    [SerializeField]
    private Text SPAmountText;

    [Header("HPテキスト")]
    [SerializeField]
    private Text HPText;
    [SerializeField]
    private Slider hpBar;
    private Rigidbody rb;
    private Vector3 dir;
    //ダメージを貰うため敵Actionクラス使用
    private EnemyWeapon enemyWeapon;
    private Animator animator;
    //攻撃時のみコライダーONにする
    private Collider swordCollider;
    private AudioSource audioSource;

    //武器振り
    public AudioClip weaponSE;

    //SP使用時の声
    public AudioClip SPUPvoice;

    //SP使用時の音
    public AudioClip SPSE;

    private void Start()
    {
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();
        swordCollider = GameObject.Find("Broadsword").GetComponent<BoxCollider>();
        hpBar.value = 1;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SPCountDownText.text = "強化終わりまで" + timeCount.ToString("0") + "秒";
        HPText.text = "HP:" + HP + "/" + maxHp;
    }

    private void Update()
    {
        Debug.Log("操作:Zで攻撃、XでHP80以下で強化、Spaceで武器替え");

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 1, -10);
        }
        SPbooster();
        hpBar.value = HP / maxHp;
        SPAmountText.text = "残SP：" + SPImage;
    }
    private void FixedUpdate()
    {
        Move();
        Stop();
        AttackStart();
    }

    // ////////////////////////////Animatorの動き
    //AttackStartとAttackEndはイベントで設定した
    //AttackEndは参照無くても動く
    private void AttackStart()
    {
        //止まっているときにしか攻撃できない
        //その場にいる時
        if (dir == Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //アニメーター発動

                animator.SetBool("Slash", true);

                audioSource.PlayOneShot(weaponSE);

                //攻撃時のみコライダーON
                swordCollider.enabled = true;
            }
        }
    }
    ///　　　　　　　　　　　　　　　　//////////////////////////////////////
    //コライダーリセット処理
    private void AttackEnd()
    {
        //剣のコライダーオフ
        swordCollider.enabled = false;
        animator.SetBool("Slash", false);
    }


    private void OnTriggerEnter(Collider other)
    {
        Die();
    }

    //ボタン離したらすぐその場に止まる
    private void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetButtonUp("Vertical"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    //移動処理
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        dir = new Vector3(x, 0, z).normalized;
        rb.AddForce(x * moveSpeed, 0, z * moveSpeed);
        LookDirection(dir);

        //Animator/////////////////////
        //停止していないなら（移動しているなら）
        if (dir != Vector3.zero)
        {
            animator.SetFloat("Speed", 0.8f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
    private void LookDirection(Vector3 dir)
    {
        // ベクトル(向きと大きさ)の2乗の長さをfloatで戻す = Playerが移動しているかどうかの確認
        if (dir.sqrMagnitude <= 0f)
        {
            return;
        }
        // 補間関数 Slerp（始まりの位置, 終わりの位置, 時間）　なめらかに回転する
        Vector3 forward = Vector3.Slerp(transform.forward, dir, rotateSpeed * Time.deltaTime);

        // 引数はVector3　Playerの向きを、自分を中心に変える
        transform.LookAt(transform.position + forward);
    }
    //死亡処理
    private void Die()
    {
        if (HP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    //回復アイテム拾得の値貰う処理
    public void Addheal(float amonut)
    {
        //HPに回復量追加
        HP += amonut;
    }


    public void Weaponpowersword(float weapon)
    {
        //武器の値貰っても本体の上限超えない
        if (attackPower >= maxAttackPower)
        {
            attackPower += weapon;
        }
    }

    //SPを使用し能力アップ
    void SPbooster()
    {
        ///////////////ボタン決めておく 上昇数　回数も
        //HP9から下、カウントがある状態で発動
        if (Input.GetKeyDown(KeyCode.X) && HP <= 80 && spCount > 0)
        {
            audioSource.PlayOneShot(SPSE);
            audioSource.PlayOneShot(SPUPvoice);

            timeCount = 20f;

            spCount--;
            //値の保持
            stockAttackPower = maxAttackPower;
            stockDefensePower = maxDefensePower;
            attackPower += 10;
            defensePower += 10;

            //SPのイメージが使っていくと減っていく処理
            for (int i = 0; i < SPAmountObjs.Length; i++)
            {
                if (spCount <= i)
                {
                    SPAmountObjs[i].SetActive(false);
                }
                else
                {
                    SPAmountObjs[i].SetActive(true);
                }
            }
        }
        //ボタンを押したときにtimecountに20が入りカウントダウンが発動
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            SPCountDownText.text = "強化終わりまで" + timeCount.ToString("0") + "秒";
        }
        //時間たつと強化時間終わり
        if (timeCount < 0)
        {
            attackPower = stockAttackPower;
            defensePower = stockDefensePower;
        }
    }
}