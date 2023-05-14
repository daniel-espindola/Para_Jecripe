using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemOponents : MonoBehaviour
{
    public SystemMainSwimming systemMain;

    [Header("Oponents Settings")]
    public GameObject oponents;
    public float speedMin, speedMax, speed;
    public float X, Y, Z;
    public float rX, rY, rZ;
    public bool isWater;
    public bool start;
    public GameObject timeLine;
    public GameObject render;

    Animator anim;
    private void Start()
    {
        render.SetActive(false);
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(systemMain.pauseGame == false && systemMain.startGame == true && isWater == true)
        {
            transform.position += Vector3.forward * speed;
            start = true;
        }
        if (start == false && systemMain.startGame == true && systemMain.pauseGame == false)
        {
            oponents.transform.rotation = Quaternion.Euler(rX, rY, rZ);
            transform.position = transform.position = new Vector3(X, Y, Z);
            timeLine.SetActive(true);
            render.SetActive(true);
            isWater = true;
        }
    }
    private void LateUpdate()
    {
        speed = Random.Range(speedMin, speedMax);
        //Debug.Log(speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            Debug.Log("Win");
            systemMain.timeGame = 0;
        }
    }
}
