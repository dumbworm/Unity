using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOver : MonoBehaviour
{
    // Start is called before the first frame update
    //scene结束之后的UI
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
        if (collision.transform.tag == "Player")
        {
            Destroy(collision.gameObject);
            Win.SetActive(true);
        }
    }
}

