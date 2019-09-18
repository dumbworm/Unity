using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;



public class Teach : MonoBehaviour
{
   
    

    // Start is called before the first frame update
    void Start()
    {
       



    }
    

    void Update()
    {
        


    }


    public void click_teach()
    {

        SceneManager.LoadScene("meg_teach");

    }
    public void click_1_1()
    {
        SceneManager.LoadScene("meg1_1");
        
    }
    public void click_1_2()
    {
        SceneManager.LoadScene("meg1_2");

    }
    public void click_1_3()
    {
        SceneManager.LoadScene("meg1_3");

    }
    public void click_2_1()
    {
        SceneManager.LoadScene("meg2_1");
        
    }
    public void click_2_2()
    {
        SceneManager.LoadScene("meg2_2");

    }
    public void click_2_3()
    {
        SceneManager.LoadScene("meg2_3");

    }
    public void click_3_1()
    {
        SceneManager.LoadScene("meg3_1");
        
    }
    public void click_3_2()
    {
        SceneManager.LoadScene("meg3_2");

    }
    public void click_3_3()
    {
        SceneManager.LoadScene("meg3_3");

    }
    public void exit()
    {
        SceneManager.LoadScene("main");
        
    }
   

}