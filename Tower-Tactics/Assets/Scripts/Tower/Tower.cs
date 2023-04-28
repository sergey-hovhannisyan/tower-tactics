using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject canvasToToggle;
    public GameObject rangeIndicator;

    private bool isTouchingTarget = false;
    private EventSystem eventSystem;
    private void Start()
    {
        eventSystem = EventSystem.current;
        canvasToToggle.SetActive(false);
        rangeIndicator.SetActive(false);
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = touch.position;
                System.Collections.Generic.List<RaycastResult> raycastResults = new System.Collections.Generic.List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                bool isTouchingUI = raycastResults.Count > 0;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Show the Canvas when touching the target GameObject
                        canvasToToggle.SetActive(true);
                        rangeIndicator.SetActive(true);
                    }
    
                    else if (canvasToToggle.activeSelf && !isTouchingUI)
                    {
                        // Hide the Canvas when touching anywhere else
                        canvasToToggle.SetActive(false);
                        rangeIndicator.SetActive(false);
                    }
                }
                else if (canvasToToggle.activeSelf)
                {
                    // Hide the Canvas when touching anywhere else
                    canvasToToggle.SetActive(false);
                    rangeIndicator.SetActive(false);
                }
            }
        }
    }
}