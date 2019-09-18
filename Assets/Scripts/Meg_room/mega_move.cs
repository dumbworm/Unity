using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mega_move : MonoBehaviour
{
    // Start is called before the first frame update
    //磁极的2个质心,CE为整个磁铁的质心
    public GameObject NP;
    public GameObject SP;
    public GameObject CE;
    //计算磁性力大小的常量,strength是力大小常量。
    public float Maxstrength_H, Maxstrength_V;//分别分开设置上下，和左右结构的磁力大小
    public float Minstrength_H, Minstrength_V;//设置最小磁力大小
    private float strength_min, other_strength_min, strength_max, other_strength_max;//本体力度和对方力度
    [HideInInspector] public int direct;//2个物体相对方向，1均为上下结构，2均为左右结构，0其它方式，
    [HideInInspector] public int force_type;//力的类型，0无任何磁力，1吸力，2斥力。注：有磁力影响时，才有相对方向和力类型
    void Start()
    {
        direct = 0;
        force_type = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

}