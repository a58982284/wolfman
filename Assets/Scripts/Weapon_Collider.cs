using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Collider : MonoBehaviour
{
    public Collider Collider;
    private AudioSource audioSource;
    private Animator animator;
    public void Init(AudioSource audioSource,Animator animator)
    {
        this.audioSource = audioSource;
        this.animator = animator;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        targets.Clear();
    }

    private List<GameObject> targets = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        //我之前碰到过的这个角色就无视
        if (targets.Contains(other.gameObject)) return;
        //对方是不是敌人
        if (other.gameObject.tag=="Enemy")
        {
            //实例化命中粒子
            GameObject.Instantiate(Resources.Load("刀光"),other.bounds.ClosestPoint(transform.position),Quaternion.identity,null);
            //打击音效
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/命中"));
            //附加伤害
            Debug.Log("我打到了敌人");
            //摄像机震动
            Camera_Controller.Instance.Shake();
            //卡肉
            StartCoroutine(PauseFrame());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider
    }


    //
    private IEnumerator PauseFrame()
    {
        //让动画停止
        animator.speed = 0.2f;
        //延迟一段时间
        yield return new WaitForSeconds(0.2f);
        //让动画恢复
        animator.speed = 1;
    }
}
