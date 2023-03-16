using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class StartPopUpMessage
{
    public static void Message(string message, Color color)
    {
        GameObject go = Object.Instantiate(AssetsHandler.i.popUpPrefab,GameManager.i.UICanvas.transform);
        go.GetComponent<TMP_Text>().text = message;
        go.GetComponent<TMP_Text>().color = color;
    }
}
