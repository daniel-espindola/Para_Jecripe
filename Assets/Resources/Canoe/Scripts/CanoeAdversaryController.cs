﻿using UnityEngine;
using System.Collections;

public class CanoeAdversaryController : MonoBehaviour {

    private string name;
    private Animator animator;
    private Rigidbody rb;
    private float maxSpeed;
    private float acceleration;
    private float t;
    public bool start;

	// Use this for initialization
	void Start () {
        start = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maxSpeed = Random.Range(60f, 100f);
        acceleration = Random.Range(17f, 20f);
        t = -2;
	}
	
	// Update is called once per frame
	void Update () {
        if (start == false && Input.GetKeyDown(KeyCode.Space)) start = true;
        if (start == true)
        {
            animator.SetTrigger("Paddle");
            if (Time.time >= t + 0.8)
            {
                int r = Random.Range(0, 10);
                if (r <= 7 && rb.velocity.magnitude < maxSpeed)
                {
                    rb.velocity += acceleration * -transform.forward;
                }
                else if (r<=9&&-rb.velocity.z > 0)
                {
                    rb.velocity += acceleration/2 * -transform.forward;
                }
                else
                {
                    rb.velocity -= acceleration / 2 * -transform.forward;
                }
                t = Time.time;
            }
            if (-rb.velocity.z > 0) rb.velocity -= acceleration / 100 * -transform.forward;
        }
        else Stop();
	}

    void Stop()
    {

        if (-rb.velocity.z > 0) rb.velocity -= acceleration / 100 * -transform.forward;
        else
        {
            animator.SetTrigger("Idle");
            rb.velocity = Vector3.zero;
        }
    }

    public void setName(string s)
    {
        name = s;
    }

    public string getName()
    {
        return name;
    }
}