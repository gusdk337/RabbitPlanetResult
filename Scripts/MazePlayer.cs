using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Walk
    }

    public Animator anim;
    private eState state;
    public float speed = 3f;

    //게임 오버 시 플레이어 멈추기
    public bool isStopped;

    public VariableJoystick joy;

    private void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.state = eState.Idle;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") + joy.Horizontal;
        float v = Input.GetAxisRaw("Vertical") + joy.Vertical;
        Vector3 dir = Vector3.Normalize(new Vector3(h, 0, v));

        this.transform.Translate(dir * this.speed * Time.deltaTime, Space.World);

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        if (dir != Vector3.zero)
        {
            this.anim.SetInteger("State", 1);
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        }
        else
        {
            this.anim.SetInteger("State", 0);
        }

        this.StopMove();    //게임 오버되면 멈춤

    }

    public void StopMove()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (this.isStopped)
        {
            this.speed = 0;
            rb.velocity = Vector3.zero;
        }
    }

}
