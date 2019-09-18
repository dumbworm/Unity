using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_open : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject[] scenekeys;
    public GameObject Win;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
               Win.SetActive(true);
               Destroy(collision.gameObject);
        }
    }



}
