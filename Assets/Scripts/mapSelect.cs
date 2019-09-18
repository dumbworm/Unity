using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class mapSelect : MonoBehaviour {


    public int starNum = 0;
    private bool isSelect = false;

    public GameObject stars;
    public GameObject locks;
    public GameObject Panel;
    public GameObject map;

    public Text starstext;
    public int starnum = 1;
    public int nownum  = 6;
	// Use this for initialization
	private void Start () {

       //PlayerPrefs.DeleteAll();
        if(PlayerPrefs.GetInt("total", 0) >=starNum)
        {
            isSelect = true;
        }

        if(isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
            int counts = 0;
           for(int i = starnum; i < nownum + 1; i++)
            {
                
                counts += PlayerPrefs.GetInt("level" + i.ToString(),0);

            }
            starstext.text = counts.ToString() +"/" +"18";
        }
	}
	
	

    public  void Selected()
    {
        Panel.SetActive(true);
        map.SetActive(false);
    }

    public void Return()
    {
        Panel.SetActive(false);
        map.SetActive(true);
    }

    public void ReturnStart()
    {
        SceneManager.LoadScene(0);
    }

    public void Enter()
    {
        SceneManager.LoadScene(2);
    }
}
