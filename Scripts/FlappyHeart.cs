using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyHeart : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        this.transform.Translate(Vector3.left * this.speed * Time.deltaTime, Space.World);

        if (this.gameObject.transform.position.x < -14.5)
        {
            Destroy(this.gameObject);
        }
    }
}
