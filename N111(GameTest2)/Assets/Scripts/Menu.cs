using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Reflection;

public class Menu:MonoBehaviour

{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    private Component gameComponent;
    private System.Type gameType;
    private TextTranslator textTranslator;
    private readonly System.Type[] games = new System.Type[] {typeof(Bacteries)};

    void Start()
    {
        textTranslator = new TextTranslator(field:"Assets/languages/menu");
        newGame();
    }

    public void newGame() {
        if (gameComponent != null)
            Destroy(gameComponent);
        gameType = games[Random.Range(0, games.Length)];
        gameComponent = gameObject.AddComponent(gameType);
        ((MenuGame)gameComponent).image = image;
        ((MenuGame)gameComponent).End = End;
        ((MenuGame)gameComponent).ChangeScore = ChangeScore;
    }

    private void End(object obj, bool isWin)
    {
        newGame();
    }
    private void ChangeScore(object obj,int score) 
    {
        text.text = textTranslator.GetTranslatedText("Score")+": "+score.ToString();
    }
}
