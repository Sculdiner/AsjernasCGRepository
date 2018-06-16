using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageItem : MonoBehaviour {

    public TextMeshProUGUI MessageText;

    public void DisplayMessage(string message)
    {
        MessageText.text = message;
        this.gameObject.SetActive(true);
    }
}
