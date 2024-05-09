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
        //������ ���� 1�� ����2�� �ε����ų�, ������ ����2�� ����1�� �ε����� ���������� ���� -> ���ӿ���(���Ϳ���)
        if(this.gameObject.CompareTag("BasicTotem1") && other.gameObject.CompareTag("Totem2") || this.gameObject.CompareTag("BasicTotem2") && other.gameObject.CompareTag("Totem1"))
        {
            Destroy(this.parentObject);
        }
    }
}
