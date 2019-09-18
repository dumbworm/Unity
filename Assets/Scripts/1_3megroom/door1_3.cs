using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door1_3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] scenekeys;
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
            int count = 0;
            foreach (GameObject star in scenekeys)
                if (star != null) count++;
            if (count == 0)
            {
                Destroy(collision.gameObject);
                Win.SetActive(true);
            }
        }
    }



}
