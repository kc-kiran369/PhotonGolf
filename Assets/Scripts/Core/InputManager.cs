using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
     public UnityEvent OnLeftMousePressed;
     public UnityEvent OnLeftMouseReleased;
     public UnityEvent OnRightMousePressed;
     public UnityEvent<Vector2> OnMouseDragged;

     private InputActions _actions;

     private void Awake()
     {
         _actions = new InputActions();

         _actions.Ball.OnBallPress.performed += OnLeftMousePress;
         _actions.Ball.OnBallRelease.performed += OnLeftMouseRelease;
         _actions.Ball.OnMouseDrag.performed += OnMouseDrag;
         _actions.Ball.OnRightMouseClick.performed += OnRightMousePress;
     }

     private void OnLeftMousePress(InputAction.CallbackContext context)
     {
         OnLeftMousePressed.Invoke();
     }

     private void OnLeftMouseRelease(InputAction.CallbackContext context)
     {
         OnLeftMouseReleased.Invoke();
     }

     private void OnMouseDrag(InputAction.CallbackContext context)
     {
         OnMouseDragged.Invoke(context.ReadValue<Vector2>());
     }

     private void OnRightMousePress(InputAction.CallbackContext context)
     {
         OnRightMousePressed.Invoke();
     }

     private void OnEnable()
     {
         EnableControls();
     }

     private void OnDisable()
     {
         DisableControls();
     }

     public void EnableControls()
     {
         _actions.Enable();
     }

     public void DisableControls()
     {
         _actions.Disable();
     }
}