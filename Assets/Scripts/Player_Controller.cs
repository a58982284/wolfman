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
                Move();
                break;
            case PlayerState.Attack:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
