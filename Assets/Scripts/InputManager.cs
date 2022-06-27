using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : GenericSingleton<InputManager> {
    public event Action<Vector3> OnSwipeStarted;
    public event Action<Vector3> OnSwipe;
    public event Action<Vector3> OnSwipeEnded;
    public event CustomAction OnTap;

    public event Action<Vector3> OnHoldFinger;
    public event Action<Vector3> OnReleaseFinger;

    public delegate void CustomAction(Vector3 inputPos = default(Vector3));

    void Start() {
        if (EventSystem.current == null) {
            Debug.LogError("DEV ERROR: Event System doesn't exist in scene, input manager won't work without it");
        }
    }

    void Update() {
        HandleInput();
    }

    public void EnableInputManager() {
        enabled = true;
    }

    public void DisableInputManager() {
        enabled = false;
    }

    public void ClearAllEvents() {
        OnSwipe = null;
        OnSwipeEnded = null;
        OnSwipeStarted = null;
        OnTap = null;
    }

    private Vector3 startPosition;
    private bool isSwiping = false;
    private void HandleInput() {
        //Ignore input in case the player is tapping on a UI element
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0)) {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
                Debug.Log("Touch found 1");
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
                    Debug.Log("Touched the UI 1");
                    return;
                }
            }
            startPosition = Input.mousePosition;

            if (OnTap != null) {
                // Debug.Log("Tapped on screen");
                OnTap.Invoke(startPosition);
            }

        }
        else if (Input.GetMouseButton(0)) {
            if (Input.touchCount > 0) {
                Debug.Log("Touch found 2");
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Debug.Log("Touched the UI 2");
                    return;
                }
            }

            Vector3 currentPosition = Input.mousePosition;
            Vector3 movement = startPosition - currentPosition;

            if (OnHoldFinger != null) {
                OnHoldFinger.Invoke(currentPosition);
            }

            if (isSwiping == true && movement != Vector3.zero) {
                if (OnSwipe != null) {
                    OnSwipe.Invoke(currentPosition);
                }
            }
            else {
                if (isSwiping == false && OnSwipeStarted != null) {
                    isSwiping = true;
                    OnSwipeStarted.Invoke(startPosition);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("Touch found 3");
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Debug.Log("Touched the UI 3");
                    return;
                }
            }
            Vector3 currentPosition = Input.mousePosition;
            // Vector3 movement = startPosition - currentPosition;

            if (OnReleaseFinger != null)
            {
                OnReleaseFinger.Invoke(currentPosition);
            }

            if (OnSwipeEnded != null && isSwiping)
            {
                isSwiping = false;
                OnSwipeEnded.Invoke(currentPosition);
            }
        }
    }
}