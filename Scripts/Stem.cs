using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        this.transform.Translate(this.transform.right * this.speed * Time.deltaTime);

        if(this.gameObject.transform.position.x < -14.5)
        {
            Destroy(this.gameObject);
        }
    }
}
