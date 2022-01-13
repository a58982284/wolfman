using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Collider : MonoBehaviour
{
    public Collider Collider;
    private AudioSource audioSource;
    public void Init(AudioSource audioSource)
    {
        this.audioSource = audioSource;
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
            //���Ӵ��Ч��
            //�����˺�
            Debug.Log("�Ҵ��˵���");
        }
    }
}
