using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMain : MonoBehaviour
{
    public GameObject boardPrefab;
    private float repeatInterval = 1.3f;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    private void Start()
    {
        InvokeRepeating("GenerateBoard", 0.0f, repeatInterval);
    }

    public void GenerateBoard()
    {
        GameObject boardGo = Instantiate(boardPrefab);
        boardGo.transform.position = new Vector3(Random.Range(-3f, 3f), 19.46f, 33f);
    }

}
