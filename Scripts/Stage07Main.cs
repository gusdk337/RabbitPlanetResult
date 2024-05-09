using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage07Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    //�� �մ� �Ĺ�
    public GameObject poison;

    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        this.poison.GetComponent<PoisonGenerator>().Init(0f, 5.6f, 1f, 0f, 6.8f);
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
