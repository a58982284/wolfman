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
    //        //��״̬�л�ʱ,�Զ�����һЩ�߼�
    //        //�߼������״̬�л�
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
                //����ҿ��Կ���
                Move();
                CheckAndEnterAttackState();
                //����ҿ��Թ���(�л�������״̬)
                break;
            case PlayerState.Attack:
                //��������
                Attack();


                //�����ǰ���ں�ҡ��Χ��,������Ұ�����,�ص��ƶ�״̬normal״̬
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ������
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
        //�Ƚ�h��v�ľ���ֵ��С,
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

        //������ת
        if (h != 0 || v != 0)
        {
            //Ŀ����ת�Ƕ�
            Quaternion targerDirQuaternion = Quaternion.LookRotation(new Vector3(h, 0, v));
            //ƽ������
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targerDirQuaternion, Time.deltaTime * 10f);
        }
    }

    //��鲢���빥��״̬
    private void CheckAndEnterAttackState()
    {
        //��J�չ�
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerState = PlayerState.Attack;
            Animator.SetTrigger("StandAttack");
        }
        //����
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playerState = PlayerState.Attack;
            Animator.SetTrigger("Skill");
        }
    }

    //�����¼�

    private bool canAttack = true;
    //��ʼ��������
    private void StartAttack()
    {
        Animator.ResetTrigger("AttackOver");
        canAttack = false;
    }
    //�������ܵĽ���
    private void EndAttack()
    {
        canAttack = true;
        Animator.SetTrigger("AttackOver");
        playerState = PlayerState.Normal;
    }
    //�����˺�
    private void StartHit(int weaponNum)
    {
        //������Ч
        AudioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/צ"));
        weapon_Colliders[weaponNum].gameObject.SetActive(true);

    }
    //ֹͣ�˺�
    private void StopHit(int weaponNum)
    {
        weapon_Colliders[weaponNum].gameObject.SetActive(false);
    }
    //�����л�-��ҡ
    private void CanSwitch()
    {
        canAttack = true;
    }

    private void Attack()
    {
        //�����ǰ���ܹ���,��ʲô��������;
        if (!canAttack) return;
        //��ǰ�ְ��չ���
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
        //�����ǰ���ں�ҡ��Χ��,������Ұ�����,�ص��ƶ�״̬
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Animator.SetTrigger("AttackOver");
            playerState = PlayerState.Normal;
        }

    }

}






