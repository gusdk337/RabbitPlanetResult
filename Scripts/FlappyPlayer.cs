using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Jump
    }

    public Animator anim;
    private eState state;
    private Rigidbody rb;
    public float jumpForce = 5f;
    private bool isAnimating = false;

    public int heartCnt;

    private void Start()
    {
        AudioListener.volume = 5;
        this.anim = this.GetComponent<Animator>();
        this.state = eState.Idle;
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.anim.SetInteger("State", 1);
            this.state = eState.Jump;
            this.Jump();
            this.isAnimating = true;

            if (isAnimating)
            {
                this.anim.Play("Jump", 0, 0);
            }
        }
        else
        {
            this.anim.SetInteger("State", 0);
            this.state = eState.Idle;
        }

        if(this.gameObject.transform.position.y > 3.9 || this.gameObject.transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }
    }

    public void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;

        this.state = eState.Jump;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            this.heartCnt++;
            Debug.Log(heartCnt);
            SoundManager.PlaySFX("Pop");
            Destroy(collision.gameObject);

        }
    }
}
