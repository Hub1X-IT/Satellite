using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public PlayerInputActions CurrentInputActions { get; private set; }


    public Vector2 MovementVectorNormalized => CurrentInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized;

    public Vector2 RotationVector => CurrentInputActions.PlayerWalking.Rotate.ReadValue<Vector2>();

    // public static Vector2 MouseDelta => PlayerInputActions.Computer.MouseDelta.ReadValue<Vector2>();

    public float MouseScroll => CurrentInputActions.CommandPrompt.MouseScroll.ReadValue<Vector2>().y;
    
    public event Action EscapeAction;
    public event Action OnPauseAction;

    public event Action OnInteractAction;

    public event Action OnFlashlightToggleAction;
    public event Action OnSmartphoneToggleAction;
    public event Action OnGuidebookToggleAction;
    public event Action OnDialogueSkipAction;

    public event Action OnNextDialogueSentenceAction;

    public event Action OnGuidebookChangePageLeftAction;
    public event Action OnGuidebookChangePageRightAction;

    public event Action OnLeftClickPerformedAction;
    public event Action OnReturnPerformedAction;

    public event Action<char> OnKeyboardInputAction;

    public event Action OnCommandSubmitAction;
    public event Action OnPreviousCommandAction;
    public event Action OnNextCommandAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(GameInput)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeInput();
    }

    private void OnDestroy()
    {
        RemoveInput();
    }

    private void InitializeInput()
    {
        CurrentInputActions = new();

        CurrentInputActions.All.Enable();
        CurrentInputActions.PlayerWalking.Enable();
        CurrentInputActions.Dialogue.Disable();
        CurrentInputActions.Guidebook.Disable();
        CurrentInputActions.Computer.Disable();
        CurrentInputActions.CommandPrompt.Disable();

        CurrentInputActions.All.Escape.performed += Escape_performed;

        CurrentInputActions.PlayerWalking.Interact.performed += Interact_performed;

        CurrentInputActions.PlayerWalking.FlashlightToggle.performed += FlashlightToggle_performed;
        CurrentInputActions.PlayerWalking.SmartphoneToggle.performed += SmartphoneToggle_performed;
        CurrentInputActions.PlayerWalking.GuidebookToggle.performed += GuidebookToggle_performed;

        CurrentInputActions.PlayerWalking.DialogueSkip.performed += DialogueSkip_performed;

        CurrentInputActions.Dialogue.NextSentence.performed += NextDialogueSentence_performed;

        CurrentInputActions.Guidebook.ChangePageRight.performed += GuidebookChangePageLeft_performed;
        CurrentInputActions.Guidebook.ChangePageLeft.performed += GuidebookChangePageRight_performed;

        CurrentInputActions.Computer.LeftClick.performed += LeftClick_performed;
        CurrentInputActions.Computer.Return.performed += Return_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        CurrentInputActions.CommandPrompt.CommandSubmit.performed += CommandSubmit_performed;
        CurrentInputActions.CommandPrompt.PreviousCommand.performed += PreviousCommand_preformed;
        CurrentInputActions.CommandPrompt.NextCommand.performed += NextCommand_preformed;
    }

    private void RemoveInput()
    {
        CurrentInputActions.All.Escape.performed -= Escape_performed;

        CurrentInputActions.PlayerWalking.Interact.performed -= Interact_performed;

        CurrentInputActions.PlayerWalking.FlashlightToggle.performed -= FlashlightToggle_performed;
        CurrentInputActions.PlayerWalking.SmartphoneToggle.performed -= SmartphoneToggle_performed;
        CurrentInputActions.PlayerWalking.GuidebookToggle.performed -= GuidebookToggle_performed;

        CurrentInputActions.Dialogue.NextSentence.performed -= NextDialogueSentence_performed;

        CurrentInputActions.Guidebook.ChangePageRight.performed -= GuidebookChangePageLeft_performed;
        CurrentInputActions.Guidebook.ChangePageLeft.performed -= GuidebookChangePageRight_performed;

        CurrentInputActions.Computer.LeftClick.performed -= LeftClick_performed;
        CurrentInputActions.Computer.Return.performed -= Return_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        CurrentInputActions.CommandPrompt.CommandSubmit.performed -= CommandSubmit_performed;
        CurrentInputActions.CommandPrompt.PreviousCommand.performed -= PreviousCommand_preformed;
        CurrentInputActions.CommandPrompt.NextCommand.performed -= NextCommand_preformed;

        CurrentInputActions.Dispose();
    }

    private void Keyboard_onTextInput(char c)
    {
        if (CurrentInputActions.CommandPrompt.enabled)
        {
            OnKeyboardInputAction?.Invoke(c);
        }
    }

    private void Escape_performed(InputAction.CallbackContext _)
    {
        if (EscapeAction != null)
        {
            EscapeAction.Invoke();
        }
        else
        {
            OnPauseAction?.Invoke();
        }
    }

    private void Interact_performed(InputAction.CallbackContext _) => OnInteractAction?.Invoke();

    private void FlashlightToggle_performed(InputAction.CallbackContext _) => OnFlashlightToggleAction?.Invoke();
    private void SmartphoneToggle_performed(InputAction.CallbackContext _) => OnSmartphoneToggleAction?.Invoke();
    private void GuidebookToggle_performed(InputAction.CallbackContext _) => OnGuidebookToggleAction?.Invoke();
    private void DialogueSkip_performed(InputAction.CallbackContext _) => OnDialogueSkipAction?.Invoke();

    private void NextDialogueSentence_performed(InputAction.CallbackContext _) => OnNextDialogueSentenceAction?.Invoke();

    private void GuidebookChangePageLeft_performed(InputAction.CallbackContext _) => OnGuidebookChangePageLeftAction?.Invoke();
    private void GuidebookChangePageRight_performed(InputAction.CallbackContext _) => OnGuidebookChangePageRightAction?.Invoke();

    private void LeftClick_performed(InputAction.CallbackContext _) => OnLeftClickPerformedAction?.Invoke();
    private void Return_performed(InputAction.CallbackContext _) => OnReturnPerformedAction?.Invoke();

    private void CommandSubmit_performed(InputAction.CallbackContext _) => OnCommandSubmitAction?.Invoke();
    private void PreviousCommand_preformed(InputAction.CallbackContext _) => OnPreviousCommandAction?.Invoke();
    private void NextCommand_preformed(InputAction.CallbackContext _) => OnNextCommandAction?.Invoke();


    public void SetMousePosition(Vector2 position)
    {
        Mouse.current.WarpCursorPosition(position);
    }
}