using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectionPageViewManager : MonoBehaviour
{
    public RectTransform Card1;
    public RectTransform Card2;
    public RectTransform Card3;
    public RectTransform Card4;
    public RectTransform Card5;
    public RectTransform Card6;
    public RectTransform Card7;
    public RectTransform Card8;

    public GameObject EmptySlotPrefab;
    public GameObject OwnedPrefab;
    public GameObject NotOwnedPrefab;

    public Button ButtonRight;
    public Button ButtonLeft;

    public TextAsset CollectionJsonSource;

    private int CurrentPage = 0;

    public View OwnerView;

    public Action<int, int> OnChangePageClicked;
    private int CurrentLowerCardId;
    private int CurrentHigherCardId;
    private int PageSize;
    // Use this for initialization
    void Start()
    {
        ButtonRight.onClick.AddListener(OnClickRightButton);
        ButtonRight.onClick.AddListener(OnClickLeftButton);
        CurrentLowerCardId = 0;
        CurrentHigherCardId = 7;
        HideButton(ButtonLeft);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickRightButton()
    {

    }

    public void OnClickLeftButton()
    {

    }

    public void HideButton(Button button)
    {
        button.gameObject.SetActive(false);
    }

    public void ShowButton(Button button)
    {
        button.gameObject.SetActive(true);
    }
}
