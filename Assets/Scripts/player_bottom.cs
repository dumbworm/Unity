using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bottom : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerControl me_script;
   // private PlayerControl other_script;


    private void Start()
    {
        me_script = GetComponentInParent<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Ground")
        {
            me_script.PlaySoundOne("Sound/落地");
            //AudioClip clip = Resources.Load<AudioClip>("Sound/落地.wav");
            //GetComponent<AudioSource>().PlayOneShot(clip);

            me_script.ani.SetBool("Isjump", false);
            me_script.IsGround = true;
        } 
       else if( collision.transform.tag == "Player")//
        {
            me_script.PlaySoundOne("Sound/落地");

            //AudioClip clip = Resources.Load<AudioClip>("Sound/落地.wav");
            //GetComponent<AudioSource>().PlayOneShot(clip);

            me_script.ani.SetBool("Isjump", false);
            me_script.IsOnPlayer = true;



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            //Debug.Log("ground,out");

            me_script.ani.SetBool("Isjump", true);
            me_script.IsGround = false;
        }
       else if( collision.transform.tag == "Player")//
        {
            //Debug.Log("player,out");

            me_script.ani.SetBool("Isjump", true);
            me_script.IsOnPlayer = false;

        }
    }
}
