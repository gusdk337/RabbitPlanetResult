using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage07Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    //독 뿜는 식물
    public GameObject poison;

    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        this.poison.GetComponent<PoisonGenerator>().Init(0f, 5.6f, 1f, 0f, 6.8f);
    }
    void Update()
    {
        this.director.txtCurrentHeartCount.text = this.player.currentHeartCount.ToString();  //update에 두는 이유 : 현재 하트 개수를 계속 알아야 함

        if (this.player.currentHeartCount == this.player.maxHeartCount) //현재 하트 개수가 0이면 디렉터의 스테이지 클리어 이미지 보여줌
        {
            this.player.isStopped = true;
            this.director.ShowStageClear();
        }
    }

}
