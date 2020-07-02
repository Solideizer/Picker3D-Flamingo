﻿using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text coins;

    private Collector collectorScript;
    public int score;

    private void Awake()
    {
        collectorScript = FindObjectOfType<Collector>();
    }

    private void Start()
    {
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        int score1, score2;
        //text.text = score + " / 10";
        //Debug.Log(other.name);

        score++;
        score1 = score;
        coins.text = (score1 * 10).ToString() + " Coins";
        text.text = score1 + " / 10";
        if (score1 >= 10)
        {
            collectorScript.passFlag = true;
        }

        if (other.CompareTag("secondCheckpoint"))
        {
            score2 = score;
            coins.text = ((score1 + score2) * 10).ToString() + " Coins";
            text.text = score2 + " / 10";

            if (score2 >= 10)
            {
                collectorScript.passFlag = true;
            }
        }
    }
}