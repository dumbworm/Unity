using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgsound : MonoBehaviour
{
    // Start is called before the first frame update 
    public GameObject objPrefabInstantSource;//音乐预知物体 
    private GameObject musicInstant = null;//场景中是否有这个物体  
    void Start()
    {
        musicInstant = GameObject.FindGameObjectWithTag("sounds");
        if (musicInstant == null)
        {
            musicInstant = (GameObject)Instantiate(objPrefabInstantSource);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Use this for initialization  




}
