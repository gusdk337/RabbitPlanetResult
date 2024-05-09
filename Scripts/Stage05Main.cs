using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage05Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    //��鸮�� ��
    public GameObject[] shakeBlocks;

    //�� �մ� �Ĺ�
    public GameObject poison;

    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        this.shakeBlocks[0].GetComponent<ShakeBlock>().Init(1);
        this.shakeBlocks[1].GetComponent<ShakeBlock>().Init(3);
        this.shakeBlocks[2].GetComponent<ShakeBlock>().Init(5);
        this.shakeBlocks[3].GetComponent<ShakeBlock>().Init(7);

        this.poison.GetComponent<PoisonGenerator>().Init(0f, 6f, 1f, 0f, 5f);
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
