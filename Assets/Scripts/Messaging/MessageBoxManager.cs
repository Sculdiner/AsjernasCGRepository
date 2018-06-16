using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBoxManager : MonoBehaviour {

    public GameObject MessageItemPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMessage(string message)
    {
        var instance = (GameObject)Instantiate(MessageItemPrefab);
        var messageItem = instance.GetComponent<MessageItem>();
        messageItem.gameObject.transform.SetParent(this.gameObject.transform);
        messageItem.gameObject.transform.localScale = new Vector3(1, 1, 1);
        (messageItem as MessageItem).DisplayMessage(message);
    }
}
