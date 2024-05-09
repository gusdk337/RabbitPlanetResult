using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlappyDirector : MonoBehaviour
{
    public FlappyPlayer player;

    public UIGameOver gameOverPopup;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiGameClear;
    public Text txtCurrentHeartCnt;
    public Text txtMaxHeartCnt;

    public void Init()
    {

    }

    private void Start()
    {
        this.txtMaxHeartCnt.text = 5.ToString();
    }

    private void Update()
    {
        if(player == null)
        {
            this.gameOverPopup.gameObject.SetActive(true);
        }

        this.txtCurrentHeartCnt.text = this.player.heartCnt.ToString();

        if(this.txtCurrentHeartCnt.text == this.txtMaxHeartCnt.text)
        {
            this.uiGameClear.gameObject.SetActive(true);
        }
    }
}
