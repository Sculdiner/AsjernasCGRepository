using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    public GameObject RotatingPartButtonObject;
    private bool AllowFlip = false;
    private bool passSideOn = false;
    public void Awake()
    {
        FlipToWait();
    }
    public void Press()
    {
        if (passSideOn)
        {
            FlipToWait();
        }
        else
        {
            FlipToPass();
        }
    }

    public void FlipToPass()
    {
        AllowFlip = true;
        var rotationx = RotatingPartButtonObject.transform.rotation.eulerAngles.x;
        RotatingPartButtonObject.transform.Rotate(new Vector3(180, 0, 0));//DORotate(new Vector3(rotationx + 180, 0, 0), 0.7f);//.SetEase(Ease.InBounce);
        passSideOn = true;
    }

    public void FlipToWait()
    {
        AllowFlip = false;
        var rotationx = RotatingPartButtonObject.transform.rotation.eulerAngles.x;
        RotatingPartButtonObject.transform.Rotate(new Vector3(180, 0, 0)); //DORotate(new Vector3(rotationx + 180, 0, 0), 0.7f);//.SetEase(Ease.InBounce);
        passSideOn = false;
    }

    public void OnMouseDown()
    {
        var localPosition = RotatingPartButtonObject.transform.localPosition;
        RotatingPartButtonObject.transform.DOLocalMove(new Vector3(localPosition.x, 0f, localPosition.z), 0.3f);
    }

    public void OnMouseUp()
    {
        var localPosition = RotatingPartButtonObject.transform.localPosition;
        RotatingPartButtonObject.transform.DOLocalMove(new Vector3(localPosition.x, 0.5f, localPosition.z), 0.3f);
        if (AllowFlip)
        {
            FlipToWait();
            BoardManager.Instance.Pass();
        }
    }
}
