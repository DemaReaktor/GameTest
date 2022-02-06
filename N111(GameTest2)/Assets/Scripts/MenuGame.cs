using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuGame : MonoBehaviour
{
    private int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text youWinText;
    [SerializeField] private Button restart;
    [SerializeField] private Button enotherGame;
    [SerializeField] private Image image;
    public int Score {
        get => score;
        set {
            score += value;
            if (score < 0)
                score = 0;
            scoreText.text = "score: "+ score.ToString();
        }
    }
    protected virtual void Start() {
        score = 0;
        scoreText.text ="score: 0";
        gameOverText.enabled = youWinText.enabled = false;

    }
    protected void UpdateImage(Texture2D texture)
    {
        image.sprite = Sprite.Create(texture, new Rect(new Vector2(), new Vector2(100, 100)), new Vector2(0.5f, 0.5f));
    }
    protected virtual void Finish(bool isWin=false) {
        youWinText.enabled = isWin;
        gameOverText.enabled = !isWin;
    }
}
