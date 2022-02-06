using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPutter : MonoBehaviour
{
    void Awake()
    {
        Normalize();
    }
    private void Normalize()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        if (transform.GetComponent<RectTransform>() != null)
        {
            transform.GetComponent<RectTransform>().position = new Vector2(
                (int)((float)transform.GetComponent<RectTransform>().position.x * screenSize.x / config.standartScreenSize.x),
                (int)((float)transform.GetComponent<RectTransform>().position.y * screenSize.y / config.standartScreenSize.y));
            transform.GetComponent<RectTransform>().localScale = new Vector2((float)screenSize.x / config.standartScreenSize.x,
                (float)screenSize.y / config.standartScreenSize.y);
            if (transform.GetComponent<Text>() != null)
                transform.GetComponent<Text>().fontSize = (int)((float)transform.GetComponent<Text>().fontSize * minimumChange(screenSize));
        }
    }
    private float minimumChange(Vector2 screenSize)
    {
        float changeX = screenSize.x / config.standartScreenSize.x;
        float changeY = screenSize.y / config.standartScreenSize.y;
        return changeX < changeY ? changeX : changeY;
    }
}
