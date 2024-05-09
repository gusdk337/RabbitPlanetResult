using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    //��鸮�� ��
    //public GameObject[] shakeBlock;

    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        //this.shakeBlock[0].GetComponent<ShakeBlock>().Init(2);
        //this.shakeBlock[1].GetComponent<ShakeBlock>().Init(3);
    }
    void Update()
    {
        this.director.txtCurrentHeartCount.text = this.player.currentHeartCount.ToString();  //update�� �δ� ���� : ���� ��Ʈ ������ ��� �˾ƾ� ��

        if (this.player.currentHeartCount == this.player.maxHeartCount) //���� ��Ʈ ������ 0�̸� ������ �������� Ŭ���� �̹��� ������
        {
            this.player.isStopped = true;
            this.director.ShowStageClear();
        } 
    }

}
