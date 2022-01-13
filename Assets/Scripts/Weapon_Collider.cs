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
        //我之前碰到过的这个角色就无视
        if (targets.Contains(other.gameObject)) return;
        //对方是不是敌人
        if (other.gameObject.tag=="Enemy")
        {
            //附加打击效果
            //附加伤害
            Debug.Log("我打到了敌人");
        }
    }
}
