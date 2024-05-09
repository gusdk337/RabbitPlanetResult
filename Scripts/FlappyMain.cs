using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMain : MonoBehaviour
{
    public UIFlappyDirector director;

    public GameObject stemPrefab;
    public GameObject flowerPrefab;
    public GameObject heartPrefab;

    private float repeatInterval1 = 3.0f;
    private float repeatInterval2 = 4.0f;
    private float repeatInterval3 = 4.5f;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    public void Init()
    {
        this.director.Init();
    }

    private void Start()
    {
        //n초마다 장애물 생성
        InvokeRepeating("GenerateStem", 0.0f, repeatInterval1);
        InvokeRepeating("GenerateFlower", 0.0f, repeatInterval2);

        //n초마다 하트 생성
        InvokeRepeating("GenerateHeart", 0.0f, repeatInterval3);
    }

    public void GenerateStem()
    {
        GameObject stem = Instantiate(stemPrefab);
        stem.transform.position = new Vector3(12.01f, Random.Range(4.9f, 5.3f), -0.354388f);
        stem.transform.localScale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));

    }

    public void GenerateFlower()
    {
        GameObject flower = Instantiate(flowerPrefab);
        flower.transform.position = new Vector3(12.01f, -2.53f, -0.354388f);
        flower.transform.localScale = new Vector3(Random.Range(8.817859f, 15f), Random.Range(8.817859f, 15f), Random.Range(8.817859f, 15f));
    }
    public void GenerateHeart()
    {
        GameObject heartGo = Instantiate(heartPrefab);
        heartGo.transform.position = new Vector3(12.01f, Random.Range(-0.1f, 3f), -0.354388f);
    }

}
