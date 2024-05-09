using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMain : MonoBehaviour
{
    public Transform goalPos;
    public MazePlayer player;
    public UIMazeDirector director;

    public float arrivalDistance = 0.5f;

    public float distanceToGoal;

    public GameObject heart;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    private void Update()
    {
        this.distanceToGoal = Vector3.Distance(player.transform.position, goalPos.position);

        //골인 지점 도착
        if (distanceToGoal <= arrivalDistance)
        {
            Destroy(this.heart);

            this.player.isStopped = true;

            this.director.uiGameClear.gameObject.SetActive(true);

            //this.director.currentTime = Time.timeScale = 0;
        }
    }
}
