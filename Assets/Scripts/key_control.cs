using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_control : MonoBehaviour
{
    private door_control door_script;
    //private GameObject[] keys;
    void Start()
    {
        door_script = GameObject.FindGameObjectWithTag("Doors").GetComponent<door_control>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            door_script.key_count = door_script.key_count - 1;
            door_script.Check();
            Destroy(this.gameObject);
        }        
    }


}
