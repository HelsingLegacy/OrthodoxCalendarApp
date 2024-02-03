using UnityEngine;

public class HudParent : MonoBehaviour
{
    public RectTransform RectParent() => 
        GetComponent<RectTransform>();
}
