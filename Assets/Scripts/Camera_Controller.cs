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

    public void Shake()
    {
        StartCoroutine(Shake(0.1f, 0.1f));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        //duration持续时间 ,magnitude振动幅度
        //保存原本的位置坐标
        Vector3 originalPostion = transform.localPosition;
        float currTime = 0f;
        //如果时间小于外面指定的
        while (currTime < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;
            transform.localPosition = originalPostion + new Vector3(x, y, 0);
            //让时间增加
            currTime += Time.deltaTime;
            //延迟一帧
            yield return null;
        }
        //到此处说明震动结束了
        transform.localPosition = originalPostion;
    }
}
