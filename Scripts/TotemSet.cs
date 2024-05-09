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
            //���� 2���� �ϳ��� �������� �����ؼ� 9ĭ�� �ڸ��� ��ġ
            randomTotem[i] = totemPrefabs[Random.Range(0, totemPrefabs.Length)];    
            GameObject totem = Instantiate(randomTotem[i], totemPos[i].position, Quaternion.Euler(0, -90, 0));
            totem.transform.SetParent(totemSetPrefab);
        }
    }

    public void MoveTotemSet()
    {
        //���ۼ�Ʈ�� �����Ǹ� ������ �̵�
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
