using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_control : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public  int key_count;
    private GameObject door_open, door_close;
    void Start()
    {
        key_count=GameObject.FindGameObjectsWithTag("Key").Length;
        door_open = this.transform.Find("door_open").gameObject;
        door_close = this.transform.Find("door_close").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        if (key_count == 0)
        {
            door_open.SetActive(true);
            door_close.SetActive(false);
        }
    }

}
