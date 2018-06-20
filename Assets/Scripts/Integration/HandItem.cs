using UnityEngine;

public class HandItem : MonoBehaviour
{
    public HandItemStatus Status { get; set; }
    public ClientSideCard ReferencedCard { get; set; }

    public Vector3 GetMyWorldPosition()
    {
        //Debug.Log("my world position: " + this.gameObject.transform.position);
        //return transform.TransformPoint(this.gameObject.transform.position);
        return transform.position;
    }
}

public enum HandItemStatus
{
    Empty = 0,
    Filled = 1,
    WaitingToBePlayed = 2
}
