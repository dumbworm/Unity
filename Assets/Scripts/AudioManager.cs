using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;
    private AudioSource player;
    void Start()
    {
        Instance = this;
        player = GetComponent<AudioSource>();


        //DontDestroyOnLoad(this.gameObject);

    }
    public void PlaySound(string name)
    {
        AudioClip clip=Resources.Load<AudioClip>(name);
        player.PlayOneShot(clip);
    }
    public void StopSound()
    {
        player.Stop();
    }
}
