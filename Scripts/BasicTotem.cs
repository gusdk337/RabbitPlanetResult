using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTotem : MonoBehaviour
{
    public bool isGameOver;
    public GameObject parentObject;

    private void Start()
    {
        this.parentObject = GameObject.Find("BasicTotems(Clone)");
    }

    private void OnTriggerEnter(Collider other)
    {
        //베이직 토템 1과 토템2가 부딪히거나, 베이직 토템2와 토템1이 부딪히면 베이직토템 삭제 -> 게임오버(디렉터에서)
        if(this.gameObject.CompareTag("BasicTotem1") && other.gameObject.CompareTag("Totem2") || this.gameObject.CompareTag("BasicTotem2") && other.gameObject.CompareTag("Totem1"))
        {
            Destroy(this.parentObject);
        }
    }
}
