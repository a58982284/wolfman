using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public static Camera_Controller Instance;
    //��Ҫ�����Ŀ��
    public Transform target;
    //�ͽ�ɫλ��ƫ����--���������
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
    {//lerp:�ӵ�ǰ������ɵ�Ŀ������
        //Ŀ������ = target
        transform.localPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 10f);

    }
}
