using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePos = Input.mousePosition;
        // mousePos.z = 10;
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Debug.DrawRay(transform.position, mousePos - transform.position, Color.red);

        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits something
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the ClickableObject script attached
                ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
                if (clickable != null)
                {
                    clickable.HandleClick(); // Call method to handle the click
                }
            }
        }
    }
}
