using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    public Transform target;
    public Transform target1;
    public float MinX;
    public float MaxX;
    */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //控制摄像机跟随玩家旋转
        /*
        Transform tar;
        if (target == null && target1 == null) return;
        else if (target == null && target1 != null) tar = target1;
        else if (target != null && target1 == null) tar = target;
        else    tar = target.localPosition.x > target1.localPosition.x ? target : target1;

        Vector3 v = transform.position;
        v.x = tar.position.x;
        if (v.x > MaxX) v.x = MaxX;
        else if (v.x < MinX) v.x = MinX;
        transform.position = v;
    */
 
    }
}
