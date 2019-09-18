using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;



public class ModelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void chuanguan()
    {
        SceneManager.LoadScene("start");
        
    }
	public void pong()
    {
        SceneManager.LoadScene("pingpong");
        
    }
}
