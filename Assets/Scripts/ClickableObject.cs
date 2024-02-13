using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public void HandleClick()
    {
        Debug.Log("Mouse clicked on: " + gameObject.name);
        // Handle click event here
    }
}
