using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI kotScore;
    [SerializeField]
    private TextMeshProUGUI piesScore;

    private int kotScoreVal = 0;
    private int piesScoreVal = 0;

    public void AddKot(int value)
    {
        kotScoreVal += value;
        kotScore.text = kotScoreVal.ToString();
    }

    public void AddPies(int value)
    {
        piesScoreVal += value;
        piesScore.text = piesScoreVal.ToString();
    }
}
