using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private float speed = 3f;


    private void Update()
    {
        //Invoke("MoveBoard", 0.3f);
        this.MoveBoard();
    }

    public void MoveBoard()
    {
        this.transform.Translate(-this.transform.forward * this.speed * Time.deltaTime);

        if (this.gameObject.transform.position.z < 7f)
        {
            Destroy(this.gameObject);
        }

    }
}
