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
        instance.transform.SetParent(this.transform);
        instance.transform.localPosition = new Vector3(0, 0, 0);
        instance.transform.localScale = new Vector3(1, 1, 1);
        (messageItem as MessageItem).DisplayMessage(message);
    }
}
