using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TurnMessenger : MonoBehaviour
{
    public TMP_Text Text;
    public GameObject RibbonObject;
    public Vector3 ScaleVector;

    public void Awake()
    {
        ScaleVector = this.transform.localScale;
    }

    public void Show(string text)
    {
        Text.text = text;
        RibbonObject.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(RibbonObject.transform.DOScale(ScaleVector, 0.4f));
        seq.Append(RibbonObject.transform.DOScale(ScaleVector, 0.8f));
        seq.Append(RibbonObject.transform.DOScale(0, 0.4f));
        seq.OnComplete(() => { RibbonObject.SetActive(false); });
    }
}
