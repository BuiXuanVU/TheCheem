using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCtrl : ComponentBehaviour
{
    public static PointCtrl instance;
    [SerializeField] private TextMeshProUGUI point;
    private int currentScore;
    private float timeLeft;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    private void Update()
    {
        timeLeft += Time.deltaTime;
        FormatTime(timeLeft);
    }
    void FormatTime(float timeLeft)
    {
        currentScore = Mathf.FloorToInt(timeLeft % 60);
        point.text = string.Format("{00:00}", currentScore);
        UIManager.instance.GetScore(currentScore);
    }    
}
