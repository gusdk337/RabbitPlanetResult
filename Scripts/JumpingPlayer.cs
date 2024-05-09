using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Jump
    }

    private float moveSpeed = 2f;
    private Animator anim;
    private eState state;
    private Rigidbody rb;
    private float jumpForce = 4f;
    private bool isAnimating = false;

    public int heartCnt;

    public VariableJoystick joy;

    private void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        this.Move();
    }

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal") + joy.Horizontal;

        Vector3 dir = Vector3.Normalize(new Vector3(h, 0, 0));

        this.transform.Translate(dir * this.moveSpeed * Time.deltaTime, Space.World);
    }

    public void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;

        this.state = eState.Jump;
        this.anim.SetInteger("State", 1);

        this.isAnimating = true;

        if (isAnimating)
        {
            this.anim.Play("Jump", 0, 0);
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
