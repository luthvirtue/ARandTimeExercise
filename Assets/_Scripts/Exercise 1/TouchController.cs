using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public float maxSwipeLength = 1000f;

    public Transform objectTransform;
    private ObjectRotation objectRotation;

    public Camera currentCamera;
    public float maxZoom = 80f;
    public float minZoom = 30f;

    private readonly float minScale = 0.5f, maxScale = 2.0f;

    private void Start()
    {
        objectRotation = GetComponent<ObjectRotation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
                //Debug.Log("Start: " + startTouchPosition);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                float swipeRange = Mathf.Clamp(Mathf.Abs(endTouchPosition.x - startTouchPosition.x), 0, maxSwipeLength);

                if (endTouchPosition.x < startTouchPosition.x)
                {
                    //Debug.Log("Swipe Left: " + swipeRange);
                    objectRotation.SetRotationSpeed(swipeRange / maxSwipeLength);
                }
                else if (endTouchPosition.x > startTouchPosition.x)
                {
                    //Debug.Log("Swipe Right: " + swipeRange);
                    objectRotation.SetRotationSpeed(-swipeRange / maxSwipeLength);
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            ResizeObject(difference);
            //ZoomCamera(difference); //Vuforia cant zoom
        }
    }

    public void ResizeObject(float increment)
    {
        objectTransform.localScale = Vector3.one * Mathf.Clamp(increment * Time.deltaTime, minScale, maxScale);
    }

    public void ZoomCamera(float increment)
    {
        currentCamera.fieldOfView = Mathf.Clamp(currentCamera.fieldOfView - increment, minZoom, maxZoom);
    }
}
