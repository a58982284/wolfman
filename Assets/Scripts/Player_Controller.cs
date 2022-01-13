using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Normal,
    Attack,
}

public class Player_Controller : MonoBehaviour
{

    //[SerializeField]
    //private Animator animator;
    //public Animator Animator { get => animator; private set => animator = value; }
    public Animator Animator;
    public CharacterController CharacterController;
    public AudioSource AudioSource;
    private PlayerState playerState;
    public Weapon_Collider[] weapon_Colliders;
    public float MoveSpeed=1.5f;
    //private PlayerState PlayerState
    //{
    //    get => playerState;
    //    private set
    //    {
    //        playerState = value;
    //        //当状态切换时,自动处理一些逻辑
    //        //逻辑层面的状态切换
    //        switch (playerState)
    //        {
    //            case PlayerState.Attack:
    //                break;
    //            case PlayerState.Normal:
    //                break;
    //        }
    //    }
    //}

    private void StateUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Normal:
                //让玩家可以控制
                Move();
                CheckAndEnterAttackState();
                //让玩家可以攻击(切换到攻击状态)
                break;
            case PlayerState.Attack:
                //连续攻击
                Attack();


                //如果当前不在后摇范围内,并且玩家按键了,回到移动状态normal状态
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //初始化武器
        weapon_Colliders[0].Init(AudioSource,Animator);
        weapon_Colliders[1].Init(AudioSource, Animator);
        weapon_Colliders[2].Init(AudioSource, Animator);
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    float runTranstition = 0;
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //CharacterController.Move(new Vector3(h, 0, v) * MoveSpeed*Time.deltaTime);
        //比较h和v的绝对值大小,
        float move = Mathf.Abs(h)>Mathf.Abs(v)?Mathf.Abs(h):Mathf.Abs(v);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (runTranstition<1)
            {
                runTranstition+=Time.deltaTime*1.5f;
            }
            else
            {
                runTranstition = 1;
            }
        }
        else
        {
            if (runTranstition >0)
            {
                runTranstition -= Time.deltaTime * 1.5f;
            }
            else
            {
                runTranstition = 0;
            }
        }
        move += runTranstition;


        Animator.SetFloat("Move", move);
        CharacterController.SimpleMove(new Vector3(h,0,v)* MoveSpeed);

        //处理旋转
        if (h != 0 || v != 0)
        {
            //目标旋转角度
            Quaternion targerDirQuaternion = Quaternion.LookRotation(new Vector3(h, 0, v));
            //平滑过渡
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targerDirQuaternion, Time.deltaTime * 10f);
        }
    }

    //检查并进入攻击状态
    private void CheckAndEnterAttackState()
    {
        //按J普攻
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerState = PlayerState.Attack;
            Animator.SetTrigger("StandAttack");
        }
        //技能
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playerState = PlayerState.Attack;
            Animator.SetTrigger("Skill");
        }
    }

    //动画事件

    private bool canAttack = true;
    //开始整个技能
    private void StartAttack()
    {
        Animator.ResetTrigger("AttackOver");
        canAttack = false;
    }
    //整个技能的结束
    private void EndAttack()
    {
        canAttack = true;
        Animator.SetTrigger("AttackOver");
        playerState = PlayerState.Normal;
    }
    //开启伤害
    private void StartHit(int weaponNum)
    {
        //播放音效
        AudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/爪"));
        weapon_Colliders[weaponNum].gameObject.SetActive(true);

    }
    //停止伤害
    private void StopHit(int weaponNum)
    {
        weapon_Colliders[weaponNum].gameObject.SetActive(false);
    }
    //技能切换-后摇
    private void CanSwitch()
    {
        canAttack = true;
    }

    private void Attack()
    {
        //如果当前不能攻击,就什么都不能做;
        if (!canAttack) return;
        //当前又按普攻了
        if (Input.GetKeyDown(KeyCode.J))
        {
            Animator.SetTrigger("StandAttack");
            return;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Animator.SetTrigger("Skill");
            return;
        }
        //Animator.CrossFadeInFixedTime("Attack1",0.2f);
        //如果当前不在后摇范围内,并且玩家按键了,回到移动状态
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Animator.SetTrigger("AttackOver");
            playerState = PlayerState.Normal;
        }

    }

}






