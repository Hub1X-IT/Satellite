using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameInput
{
    public static PlayerInputActions CurrentInputActions { get; private set; }


    public static Vector2 MovementVectorNormalized => CurrentInputActions.PlayerWalking.Move.ReadValue<Vector2>().normalized;

    public static Vector2 RotationVector => CurrentInputActions.PlayerWalking.Rotate.ReadValue<Vector2>();

    // public static Vector2 MouseDelta => PlayerInputActions.Computer.MouseDelta.ReadValue<Vector2>();

    public static float MouseScroll => CurrentInputActions.CommandPrompt.MouseScroll.ReadValue<Vector2>().y;


    public static event Action OnPauseAction;

    public static event Action OnInteractAction;

    public static event Action OnFlashlightToggleAction;
    public static event Action OnSmartphoneToggleAction;
    public static event Action OnGuidebookToggleAction;

    public static event Action OnNextDialogueSentenceAction;

    public static event Action OnGuidebookChangePageLeftAction;
    public static event Action OnGuidebookChangePageRightAction;

    public static event Action OnComputerExitAction;
    public static event Action OnLeftClickPerformedAction;
    public static event Action OnReturnPerformedAction;

    public static event Action<char> OnKeyboardInputAction;

    public static event Action OnCommandSubmitAction;
    public static event Action OnPreviousCommandAction;
    public static event Action OnNextCommandAction;


    public static void InitializeInput()
    {
        CurrentInputActions = new();

        CurrentInputActions.All.Enable();
        CurrentInputActions.PlayerWalking.Enable();
        CurrentInputActions.Dialogue.Disable();
        CurrentInputActions.Guidebook.Disable();
        CurrentInputActions.Computer.Disable();
        CurrentInputActions.CommandPrompt.Disable();

        CurrentInputActions.All.Pause.performed += Pause_performed;

        CurrentInputActions.PlayerWalking.Interact.performed += Interact_performed;

        CurrentInputActions.PlayerWalking.FlashlightToggle.performed += FlashlightToggle_performed;
        CurrentInputActions.PlayerWalking.SmartphoneToggle.performed += SmartphoneToggle_performed;
        CurrentInputActions.PlayerWalking.GuidebookToggle.performed += GuidebookToggle_performed;

        CurrentInputActions.Dialogue.NextSentence.performed += NextDialogueSentence_performed;

        CurrentInputActions.Guidebook.ChangePageRight.performed += GuidebookChangePageLeft_performed;
        CurrentInputActions.Guidebook.ChangePageLeft.performed += GuidebookChangePageRight_performed;

        CurrentInputActions.Computer.Exit.performed += ComputerExit_performed;
        CurrentInputActions.Computer.LeftClick.performed += LeftClick_performed;
        CurrentInputActions.Computer.Return.performed += Return_performed;

        Keyboard.current.onTextInput += Keyboard_onTextInput;

        CurrentInputActions.CommandPrompt.CommandSubmit.performed += CommandSubmit_performed;
        CurrentInputActions.CommandPrompt.PreviousCommand.performed += PreviousCommand_preformed;
        CurrentInputActions.CommandPrompt.NextCommand.performed += NextCommand_preformed;
    }

    public static void RemoveInput()
    {
        CurrentInputActions.All.Pause.performed -= Pause_performed;

        CurrentInputActions.PlayerWalking.Interact.performed -= Interact_performed;

        CurrentInputActions.PlayerWalking.FlashlightToggle.performed -= FlashlightToggle_performed;
        CurrentInputActions.PlayerWalking.SmartphoneToggle.performed -= SmartphoneToggle_performed;
        CurrentInputActions.PlayerWalking.GuidebookToggle.performed -= GuidebookToggle_performed;

        CurrentInputActions.Dialogue.NextSentence.performed -= NextDialogueSentence_performed;

        CurrentInputActions.Guidebook.ChangePageRight.performed -= GuidebookChangePageLeft_performed;
        CurrentInputActions.Guidebook.ChangePageLeft.performed -= GuidebookChangePageRight_performed;

        CurrentInputActions.Computer.Exit.performed -= ComputerExit_performed;
        CurrentInputActions.Computer.LeftClick.performed -= LeftClick_performed;
        CurrentInputActions.Computer.Return.performed -= Return_performed;

        Keyboard.current.onTextInput -= Keyboard_onTextInput;

        CurrentInputActions.CommandPrompt.CommandSubmit.performed -= CommandSubmit_performed;
        CurrentInputActions.CommandPrompt.PreviousCommand.performed -= PreviousCommand_preformed;
        CurrentInputActions.CommandPrompt.NextCommand.performed -= NextCommand_preformed;

        CurrentInputActions.Dispose();

        OnPauseAction = null;
        OnInteractAction = null;
        OnFlashlightToggleAction = null;
        OnSmartphoneToggleAction = null;
        OnGuidebookToggleAction = null;
        OnNextDialogueSentenceAction = null;
        OnGuidebookChangePageLeftAction = null;
        OnGuidebookChangePageRightAction = null;
        OnComputerExitAction = null;
        OnLeftClickPerformedAction = null;
        OnKeyboardInputAction = null;
        OnReturnPerformedAction = null;
        OnCommandSubmitAction = null;
        OnPreviousCommandAction = null;
        OnNextCommandAction = null;
    }

    private static void Keyboard_onTextInput(char c)
    {
        if (CurrentInputActions.CommandPrompt.enabled)
        {
            OnKeyboardInputAction?.Invoke(c);
        }
    }

    private static void Pause_performed(InputAction.CallbackContext _) => OnPauseAction?.Invoke();

    private static void Interact_performed(InputAction.CallbackContext _) => OnInteractAction?.Invoke();

    private static void FlashlightToggle_performed(InputAction.CallbackContext _) => OnFlashlightToggleAction?.Invoke();
    private static void SmartphoneToggle_performed(InputAction.CallbackContext _) => OnSmartphoneToggleAction?.Invoke();
    private static void GuidebookToggle_performed(InputAction.CallbackContext _) => OnGuidebookToggleAction?.Invoke();

    private static void NextDialogueSentence_performed(InputAction.CallbackContext _) => OnNextDialogueSentenceAction?.Invoke();

    private static void GuidebookChangePageLeft_performed(InputAction.CallbackContext _) => OnGuidebookChangePageLeftAction?.Invoke();
    private static void GuidebookChangePageRight_performed(InputAction.CallbackContext _) => OnGuidebookChangePageRightAction?.Invoke();

    private static void ComputerExit_performed(InputAction.CallbackContext _) => OnComputerExitAction?.Invoke();
    private static void LeftClick_performed(InputAction.CallbackContext _) => OnLeftClickPerformedAction?.Invoke();
    private static void Return_performed(InputAction.CallbackContext _) => OnReturnPerformedAction?.Invoke();

    private static void CommandSubmit_performed(InputAction.CallbackContext _) => OnCommandSubmitAction?.Invoke();
    private static void PreviousCommand_preformed(InputAction.CallbackContext _) => OnPreviousCommandAction?.Invoke();
    private static void NextCommand_preformed(InputAction.CallbackContext _) => OnNextCommandAction?.Invoke();


    public static void SetMousePosition(Vector2 position)
    {
        Mouse.current.WarpCursorPosition(position);
    }
}