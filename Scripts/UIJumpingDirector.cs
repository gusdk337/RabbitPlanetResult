using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJumpingDirector : MonoBehaviour
{
    public Button btnJump;
    public JumpingPlayer player;
    public UIGameRule uiGameRule;
    public UIGameOver uiGameOver;
    public UIMemoryClear uiGameClear;

    public Text txtCurrentHeartCnt;
    public Text txtMaxHeartCnt;

    private bool isClear;

    private void Start()
    {
        this.txtMaxHeartCnt.text = 3.ToString();

    }
    void Update()
    {
        this.btnJump.onClick.AddListener(() =>
        {
            this.player.Jump();
        });

        this.txtCurrentHeartCnt.text = this.player.heartCnt.ToString();

        if (this.txtCurrentHeartCnt.text == this.txtMaxHeartCnt.text)
        {
            this.uiGameClear.gameObject.SetActive(true);
            this.isClear = true;
        }

        if(this.player.transform.position.y <= 21f)
        {
            if (!this.isClear)
            {
                this.uiGameOver.gameObject.SetActive(true);
            }
        }

    }
}
