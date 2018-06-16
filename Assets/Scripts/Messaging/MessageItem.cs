using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MessageItem : MonoBehaviour {

    public TextMeshProUGUI MessageText;

    public void DisplayMessage(string message)
    {
        MessageText.text = message;
        this.gameObject.SetActive(true);
        this.gameObject.transform.DOMoveY(400, 1.5f).SetEase(Ease.OutQuint, 0.5f, 0.1f).OnComplete(TweenCallback);
    }

    public void TweenCallback()
    {
        GameObject.DestroyImmediate(this.gameObject);
    }
}
