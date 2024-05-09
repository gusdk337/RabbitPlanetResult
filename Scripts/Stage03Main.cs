using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage03Main : MonoBehaviour
{
    public MainPlayer player;
    public UIStageDirector director;

    //�� �մ� �Ĺ�
    public GameObject[] fire;

    //�� �մ� �Ĺ�
    public GameObject poison;

    private void Start()
    {
        this.director.txtMaxHeartCount.text = this.player.maxHeartCount.ToString();

        this.fire[0].GetComponent<FireObstacle>().Init(1, 3);
        this.fire[1].GetComponent<FireObstacle>().Init(2, 2);

        this.poison.GetComponent<PoisonGenerator>().Init(0.72f, 6.1f, 1f, 0.99f, 4.62f);
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
