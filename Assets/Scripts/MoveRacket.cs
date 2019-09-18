using UnityEngine;
using System.Collections;

public class MoveRacket : MonoBehaviour
{
    public float speed = 5f;
    public string axis;
    public bool Isleftplayer;
    [HideInInspector] public bool IsBlue;
    //public SpriteRenderer NS;
    // Use this for initialization
    void Start()
    {
        IsBlue = Isleftplayer ? false:true;

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
        if (Input.GetKeyDown(Isleftplayer ? KeyCode.A : KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * 180);
            IsBlue = !IsBlue;
        }
       // if (Input.GetKeyDown(KeyCode.A)) { IsBluel = !IsBluel; }
        //if (Input.GetKeyDown(KeyCode.RightArrow)) { IsBluer = !IsBluer; }

    }

}
