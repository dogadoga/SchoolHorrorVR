using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    //�ϐ��錾
    Animator animator;
    NavMeshAgent agent;
    public float walkingSpeed;
    GameObject target; //�v���C���[
    public float runSpeed;
    [Tooltip("�]���r�̍U����")] public int attackDamage;
    private GameManager GM;
    private int zombieHP = 3;
    public AudioSource zomVoice;
    public AudioClip howl, attack;
    public GameObject heartSound;
 
    //state�쐬
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
    /// �A�j���[�V������S�Ď~�߂�
    /// </summary>
    public void TurnOFFTrigger()
    {
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", false);
        animator.SetBool("dying", false);
    }

    /// <summary>
    /// �v���C���[�Ƃ̋�����Ԃ�
    /// </summary>
    /// <returns></returns>
    float DistanceToPlayer()
    {
        //�Q�[���I�[�o�[�̎��̓v���C���[��������
        if (GameManager.GameOver)
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(target.transform.position, transform.position);
    }

    /// <summary>
    /// �v���C���[����������
    /// ������15m�ȓ��������K(����3m�ȓ�)�ɂ���Ƃ��ɂ�����
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
    /// �v���C���[������������
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
    /// �v���C���[�ւ̍U��
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
    /// �]���r���S��
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
                else if (Random.Range(0, 5000) < 5) // �K���Ȋm���Ń]���r�����܂�킹��
                {
                    state = STATE.WANDER;
                }
                if (Random.Range(0,5000)<5)
                {
                    Howl(); //���Ȃ�
                }
                break;
            case STATE.WANDER:
                // �ړI�n�������Ȃ��Ƃ��Ƀ����_���ȏꏊ�ɍs���悤�ɂ���
                if (!agent.hasPath)
                {
                    float newX = transform.position.x + Random.Range(-5, 5);
                    float newZ = transform.position.z + Random.Range(-5, 5);
                    Vector3 nextPos = new Vector3(newX, transform.position.y, newZ);
                    agent.SetDestination(nextPos);
                    agent.stoppingDistance = 0; //destination�܂�0�ɂȂ�܂ŋ߂Â�

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
                agent.stoppingDistance = 1.5f; //�v���C���[���狗������������

                TurnOFFTrigger();
                agent.speed = runSpeed;
                animator.SetBool("run", true);

                //�v���C���[�ɋ߂Â����Ƃ�
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = STATE.ATTACK;
                }

                //�v���C���[���������Ƃ�
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

                //�v���C���[�̕����������悤�ɂ���
                transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z));

                //�v���C���[�����ꂽ��ǂ�������
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
    /// ���Ȃ鉹�Đ�
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
    /// �U�����̉��Đ�
    /// </summary>
    public void AttackSE()
    {
        zomVoice.clip = attack;
        zomVoice.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        //�e�e�ɓ��������Ƃ�HP-1
        if (other.gameObject.tag == "Bullet")
        {
            //UI�\��
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
    /// ����̃R���g���[����U��������
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
