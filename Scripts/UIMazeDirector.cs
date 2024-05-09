using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMazeDirector : MonoBehaviour
{
    public UIGameOver uiGameOver;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiGameClear;
    public MazePlayer player;
    public Text timerText;
    public float totalTime = 30f; // 시작할 시간(60초)

    public float currentTime;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay();

            if (this.uiGameOver != null)
            {
                this.uiGameOver.gameObject.SetActive(true);
            }
        }
    }
    private void UpdateTimerDisplay()
    {
        // 시간을 정수로 반올림하여 텍스트에 표시
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }

}
