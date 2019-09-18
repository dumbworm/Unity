using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myswitch : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator ani, ani_area;
    public GameObject go_area;
    public bool Is_area_N;
    void Start()
    {
        ani = GetComponent<Animator>();
        ani_area = go_area.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.transform.tag);
        if (collision.tag == "Player")
        {
            ani.SetBool("IsUp", false);
            ani_area.SetBool("IsN", Is_area_N);
            ani_area.SetBool("IsS", !Is_area_N);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ani.SetBool("IsUp", true);
            ani_area.SetBool("IsN", false);
            ani_area.SetBool("IsS", false);


        }
    }


}
