using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject[] winstars;
    //private bool SoundOver = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
          if (!SoundOver && gameObject.activeSelf)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sound/胜利.mp3");
            GetComponent<AudioSource>().PlayOneShot(clip);
            SoundOver = true;


        }     
        */
    }
    public void showstar()
    {
        //判断
        //StartCoroutine("Waitforshow");

    }
    IEnumerator Waitforshow()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.2f);
           // winstars[i].SetActive(true);
        }
    }
    /// <summary>
    /// 重试本关
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("start");
    }
    public void Next()
    {
        Time.timeScale = 1;
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index+1);
    }
}
