using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pausepanel : MonoBehaviour  //, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private Animator ani;
    public GameObject btn_pause;
    public GameObject help_image;
    private string[] help_texts = { "利用同极斥力可以拿到钥匙！！",
        "小磁人要飞高高！！",
        "隔着平板，磁力仍然可以对跳跃提供帮助哦！！",
        "斥力+惯性是不是可以飞的更高？！",
        "云彩可以被穿过，试着把另一个小伙伴吸上去吧！！",
        "这关的难点在横向吸引合作，请加油！！",
        "思考平板控制机制",
        "在平板下面滑行，一定很酷！！",
        "突起的障碍物不可以碰哦，思考怎么突破难关吧！",
        "躲避飞镖需要两人合作哦"};
    void Start()
    {
        Text help_t= help_image.GetComponentInChildren<Text>();
        int scene_index = SceneManager.GetActiveScene().buildIndex;
        help_t.text = help_texts[scene_index-2];

    }
    private void Awake()
    {
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 点击了pause按钮
    /// </summary>
    public void Pause()
    {
        gameObject.SetActive(true);
        btn_pause.SetActive(false);
        ani.SetBool("ispause", true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        ani.SetBool("ispause", false);
        btn_pause.SetActive(true);
        if (help_image.activeInHierarchy) help_image.SetActive(false);
    }
    public void ResumeEnd()
    {
        gameObject.SetActive(false);
        if (help_image.activeInHierarchy) help_image.SetActive(false);

        //btn_pause.SetActive(true);
    }
    public void PauseEnd()
    {
        //暂停
        Time.timeScale = 0;
    }
    public void Retry()
    {
        Time.timeScale = 1;
        if (help_image.activeInHierarchy) help_image.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {
        Time.timeScale = 1;
        if (help_image.activeInHierarchy) help_image.SetActive(false);

        SceneManager.LoadScene("start");
    }
    public void Help()
    {
        help_image.SetActive(!help_image.activeInHierarchy);
    }
    

}
