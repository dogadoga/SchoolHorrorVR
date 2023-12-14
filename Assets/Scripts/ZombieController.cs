using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    //変数宣言
    Animator animator;
    NavMeshAgent agent;
    public float walkingSpeed;
    GameObject target; //プレイヤー
    public float runSpeed;
    [Tooltip("ゾンビの攻撃力")] public int attackDamage;
    private GameManager GM;
    private int zombieHP = 3;
    public AudioSource zomVoice;
    public AudioClip howl, attack;
    public GameObject heartSound;
 
    //state作成
    enum STATE {IDLE, WANDER, ATTACK, CHASE, DEAD };
    STATE state = STATE.IDLE;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        Howl();
    }

    /// <summary>
    /// アニメーションを全て止める
    /// </summary>
    public void TurnOFFTrigger()
    {
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", false);
        animator.SetBool("dying", false);
    }

    /// <summary>
    /// プレイヤーとの距離を返す
    /// </summary>
    /// <returns></returns>
    float DistanceToPlayer()
    {
        //ゲームオーバーの時はプレイヤーを見失う
        if (GameManager.GameOver)
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(target.transform.position, transform.position);
    }

    /// <summary>
    /// プレイヤーを見つけたか
    /// 距離が15m以内かつ同じ階(高さ3m以内)にいるときにしたい
    /// </summary>
    /// <returns></returns>
    bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 20 & Mathf.Abs(target.transform.position.y - transform.position.y) < 3)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// プレイヤーを見失ったか
    /// </summary>
    /// <returns></returns>
    bool ForgetPlayer()
    {
        if (DistanceToPlayer() > 15)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// プレイヤーへの攻撃
    /// </summary>
    public void DamagePlayer()
    {
        if (target != null)
        {
            AttackSE();
            GM.TakeHit(attackDamage);
            StartCoroutine(VibrateController(0.5f));
        }
    }
    /// <summary>
    /// ゾンビ死亡時
    /// </summary>
    public void ZombieDeath()
    {
        TurnOFFTrigger();
        animator.SetBool("dying", true);
        state = STATE.DEAD;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case STATE.IDLE:
                TurnOFFTrigger();

                if (CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0, 5000) < 5) // 適当な確率でゾンビをさまよわせる
                {
                    state = STATE.WANDER;
                }
                if (Random.Range(0,5000)<5)
                {
                    Howl(); //うなる
                }
                break;
            case STATE.WANDER:
                // 目的地を持たないときにランダムな場所に行くようにする
                if (!agent.hasPath)
                {
                    float newX = transform.position.x + Random.Range(-5, 5);
                    float newZ = transform.position.z + Random.Range(-5, 5);
                    Vector3 nextPos = new Vector3(newX, transform.position.y, newZ);
                    agent.SetDestination(nextPos);
                    agent.stoppingDistance = 0; //destinationまで0になるまで近づく

                    TurnOFFTrigger();

                    agent.speed = walkingSpeed;
                    animator.SetBool("walk", true);
                }
                if (Random.Range(0, 5000) < 5)
                {
                    state = STATE.IDLE;
                    agent.ResetPath();
                }
                if (CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                break;
            case STATE.CHASE:
                if (GameManager.GameOver)
                {
                    TurnOFFTrigger();
                    agent.ResetPath();
                    state = STATE.WANDER;
                    return;
                }
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 1.5f; //プレイヤーから距離を持たせる

                TurnOFFTrigger();
                agent.speed = runSpeed;
                animator.SetBool("run", true);

                //プレイヤーに近づいたとき
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = STATE.ATTACK;
                }

                //プレイヤーを見失うとき
                if (ForgetPlayer())
                {
                    agent.ResetPath();
                    state = STATE.WANDER;
                }
                break;
            case STATE.ATTACK:
                if (GameManager.GameOver)
                {
                    TurnOFFTrigger();
                    agent.ResetPath();
                    state = STATE.WANDER;
                    return;
                }
                TurnOFFTrigger();
                animator.SetBool("attack", true);

                //プレイヤーの方向を向くようにする
                transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z));

                //プレイヤーが離れたら追いかける
                if (DistanceToPlayer() > agent.stoppingDistance + 1)
                {
                    state = STATE.CHASE;
                    Howl();
                }
                break;
            case STATE.DEAD:
                zomVoice.Stop();
                Destroy(agent);
                break;
        }
    }

    /// <summary>
    /// うなる音再生
    /// </summary>
    public void Howl()
    {
        if (!zomVoice.isPlaying)
        {
            zomVoice.clip = howl;
            zomVoice.Play();
        }
    }

    /// <summary>
    /// 攻撃時の音再生
    /// </summary>
    public void AttackSE()
    {
        zomVoice.clip = attack;
        zomVoice.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        //銃弾に当たったときHP-1
        if (other.gameObject.tag == "Bullet")
        {
            //UI表示
            UIManager.instance.ChangeHitText("Hit",0.5f);
            zombieHP -= 1;
            if (zombieHP == 0)
            {
                GameManager.zombiesDefeated += 1;
                ZombieDeath();
            }
        }
    }
    /// <summary>
    /// 両手のコントローラを振動させる
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator VibrateController(float time)
    {
        OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(time);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
    }
}
