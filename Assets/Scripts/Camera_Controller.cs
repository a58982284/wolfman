using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public static Camera_Controller Instance;
    //需要跟随的目标
    public Transform target;
    //和角色位置偏移量--计算出来的
    [SerializeField]
    private Vector3 offset;
    private void Awake()
    {
        Instance = this;
        offset = transform.position - target.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    private void Follow()
    {//lerp:从当前坐标过渡到目标坐标
        //目标坐标 = target
        transform.localPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 10f);

    }
}
