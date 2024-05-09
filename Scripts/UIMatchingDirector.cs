using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMatchingDirector : MonoBehaviour
{
    public UIGameOver uiGameOver;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiGameClear;
    public GameObject basicTotem;
    public int totemCnt;
    public Text txtCurrentTotemCnt;
    public Text txtMaxTotemCnt;
    public bool isClear;

    public Text timerText;
    //public float totalTime = 60f; // 시작할 시간(60초)

    public float currentTime;


    private void Start()
    {
        this.basicTotem = GameObject.Find("BasicTotems(Clone)");
        //this.totemSet = GameObject.Find("TotemSet(Clone)");
        this.txtMaxTotemCnt.text = 3.ToString();

        //매칭 토템 이벤트
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.DestroyTotem, new EventHandler((type) =>
        {
            this.PlusTotemCnt();
        }));

        //currentTime = totalTime;
        //UpdateTimerDisplay();

    }

    private void Update()
    {
        this.ShowGameOver();

        this.txtCurrentTotemCnt.text = this.totemCnt.ToString();

        if (this.txtCurrentTotemCnt.text == this.txtMaxTotemCnt.text)
        {
            this.uiGameClear.gameObject.SetActive(true);
        }

        //if (currentTime > 0)
        //{
        //    currentTime -= Time.deltaTime;
        //    UpdateTimerDisplay();
        //}
        //else
        //{
        //    currentTime = 0;
        //    UpdateTimerDisplay();

        //    this.uiGameClear.gameObject.SetActive(true);
        //}

    }

    public void ShowGameOver()
    {
        if (this.basicTotem == null)
        {
            this.uiGameOver.gameObject.SetActive(true);

            //Invoke("TimeStop", 0.5f);
        }


    }

    public void PlusTotemCnt()
    {
        totemCnt++;
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
    }

    //private void UpdateTimerDisplay()
    //{
    //    // 시간을 정수로 반올림하여 텍스트에 표시
    //    timerText.text = Mathf.Ceil(currentTime).ToString();
    //}


}
