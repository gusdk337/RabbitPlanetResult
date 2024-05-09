using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSet : MonoBehaviour
{
    public GameObject[] totemPrefabs;
    public Transform[] totemPos;

    private GameObject[] randomTotem = new GameObject[9];

    private float speed = 13f;

    public Transform totemSetPrefab;

    public int totemCnt;

    private void Start()
    {
        this.RandomGenerateTotem();
    }

    private void Update()
    {
        this.MoveTotemSet();

    }

    public void RandomGenerateTotem()
    {
        for(int i = 0; i < 9; i++)
        {
            //토템 2종중 하나를 랜덤으로 선택해서 9칸의 자리에 배치
            randomTotem[i] = totemPrefabs[Random.Range(0, totemPrefabs.Length)];    
            GameObject totem = Instantiate(randomTotem[i], totemPos[i].position, Quaternion.Euler(0, -90, 0));
            totem.transform.SetParent(totemSetPrefab);
        }
    }

    public void MoveTotemSet()
    {
        //토템세트가 생성되면 앞으로 이동
        this.transform.Translate(-this.transform.forward * this.speed * Time.deltaTime);

        if (this.gameObject.transform.position.z < 15)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.DestroyTotem);
    }
}
