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
        //��֮ǰ�������������ɫ������
        if (targets.Contains(other.gameObject)) return;
        //�Է��ǲ��ǵ���
        if (other.gameObject.tag=="Enemy")
        {
            //ʵ������������
            GameObject.Instantiate(Resources.Load("����"),other.bounds.ClosestPoint(transform.position),Quaternion.identity,null);
            //�����Ч
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/����"));
            //�����˺�
            Debug.Log("�Ҵ��˵���");
            //�������
            Camera_Controller.Instance.Shake();
            //����
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
        //�ö���ֹͣ
        animator.speed = 0.2f;
        //�ӳ�һ��ʱ��
        yield return new WaitForSeconds(0.2f);
        //�ö����ָ�
        animator.speed = 1;
    }
}
