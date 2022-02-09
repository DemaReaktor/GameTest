using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuGame : MonoBehaviour
{
    private int score;
    private bool isPause;
    public Image image;
    public EventHandler<int> ChangeScore;
    public EventHandler ChangeImage;
    public EventHandler<bool> ChangePauseStatus;
    public EventHandler<bool> End;

    public int Score {
        get => score;
        set {
            int past = score;
            score = value;
            if (score < 0)
                score = 0;
            if(score!=past)
                ChangeScore?.Invoke(this, score);
        }
    }
    public bool IsPause { get => isPause; }
    protected virtual void Start() {
        score = 0;
        isPause = false;
    }
    public virtual void Pause() {
        isPause = !isPause;
        ChangePauseStatus?.Invoke(this, isPause); 
    }
    protected void UpdateImage(Texture2D texture)
    {
        image.sprite = Sprite.Create(texture, new Rect(new Vector2(), new Vector2(100, 100)), new Vector2(0.5f, 0.5f));
        ChangeImage?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void Finish(bool isWin=false) {
        End?.Invoke(this, isWin);
    }
}
