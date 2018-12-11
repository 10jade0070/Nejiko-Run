using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //---1.UI 네임스페이스의 임포트

public class GameControll : MonoBehaviour
{
    public NejikoController nejiko;
    public Text scoreLabel; //---2.ScoreLabel 참조
    public LifePanel lifePanel;
    
    void Update()
    {
        // 스코어 레이블을 업데이트
        int score = CalcScore();
        scoreLabel.text = "Score : " + score + "m"; //---3.텍스트 업데이트

        // 라이프 패널을 업데이트
        lifePanel.UpdateLife(nejiko.Life()); //---4.LifePanel 업데이트
    }

    int CalcScore ()
    {
        // 네지코의 주행 거리를 스코어로 한다
        return (int)nejiko.transform.position.z;
    }
}
