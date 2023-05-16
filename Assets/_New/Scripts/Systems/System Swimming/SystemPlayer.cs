using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPlayer : MonoBehaviour
{
    private static SystemPlayer instance;

    [Header("Player Settings")]
    public GameObject player;
    public float speed;
    [Header("Others")]
    public Animator anim;
    public bool isWater;
    public GameObject fimDeJogo;

    public SystemMainSwimming systemMain;
    bool fimdogame;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        //Move player
        if(systemMain.pauseGame == false && systemMain.startGame == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.forward * speed;
                anim.SetInteger("setInt", 1);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                transform.position += Vector3.forward * speed;
                //transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
                anim.SetInteger("setInt", 0);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                transform.position += Vector3.forward * speed;
                //transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
                anim.SetInteger("setInt", 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.forward * speed;
                //transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
                anim.SetInteger("setInt", 2);
            }
            else
            {
                //anim.SetInteger("setInt", 0);
            }
            if (fimdogame == true)
            {
                systemMain.winPlayer = true;
            }
            isWater = true;
        } 
    }
    public void WaterPlayer()
    {
        player.transform.rotation = Quaternion.Euler(-90, 180, 0);
        transform.position = transform.position = new Vector3(0, -1.8f, 6);
        //anim.SetInteger("setInt", 3);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            //Debug.Log("Win");
            Debug.Log(fimdogame);
            systemMain.winPlayer = true;
            systemMain.ShowWin();
            fimDeJogo.SetActive(true);
            fimdogame = true;
            
        }
    }
}
