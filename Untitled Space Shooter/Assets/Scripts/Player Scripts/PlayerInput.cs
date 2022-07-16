//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player Scripts/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player "",
            ""id"": ""98302df3-9df3-4be5-8e85-bd779a0861d7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""abc18231-7385-4265-a387-f5f91b99ff40"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseVector"",
                    ""type"": ""Value"",
                    ""id"": ""53761d46-903a-4322-90b1-a56b18f58e0d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""fcc1eb44-91df-49e7-9efd-fef1fb69b5ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""c9c4d6ad-8420-406e-a046-92f5e8d8f346"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightGun"",
                    ""type"": ""Button"",
                    ""id"": ""e43163d3-f3c2-411e-a163-19ab6eddc784"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""045c65f6-a2c2-4204-aae6-555d9c7c6581"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponSwap"",
                    ""type"": ""Value"",
                    ""id"": ""4830e5a3-c1eb-4157-b758-d7b9c3b7799e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""b66eb66a-7e68-406e-9d61-0bed9f7eea0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""7ebebe80-935b-4cac-a84c-08bb2de4f88d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""28171813-b6f0-4a3d-b1f2-2fa5d565007e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TertiaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""90bd5456-bb2a-4df1-b728-c76aec273415"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HolsterWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""b782931e-f0f2-4daf-8738-dd626bf37ca9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""49601d9e-90b8-4363-adb2-f34438a21fe2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""9431bb3f-ba30-487c-b049-c9ab725c84ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""880085e6-223d-4c21-b24c-f45f859bbd84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GrapplingHook"",
                    ""type"": ""Button"",
                    ""id"": ""69bc42f6-cbb8-4b01-82c1-89fa19886b31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bc92955e-92bf-4c18-91c7-0d67dff3e583"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""333a5aad-d233-4113-8bee-349571cda1ea"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""91a77d7c-dedd-4343-8ca9-d8433382274f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3108a3ec-8a73-44fd-af4c-585ce42414c7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4a0514a5-6aef-4fda-bea9-2301e87d6122"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""76a6cacd-302a-4b37-9e15-5d4c23209ac3"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6eecf618-42e4-47c0-bcc2-29fde087d158"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cf95fef-7825-4b2f-abe6-61c94271611d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7462a5ed-9062-40a1-a7fd-cc036f0e2514"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07b9f4d2-9d07-4863-b8d1-0868ede8c4ea"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""WeaponSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dc402ec-9cb4-4d67-9cd9-dcb962840284"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ca0a242-ca06-4682-8536-1fa2f6132e43"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseVector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7bcb172-375a-4953-9728-dc8934304875"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a455534f-d20d-4b20-9347-bb571686ac6f"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b15a4da9-7ae5-4059-87f0-d2a3c864761f"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TertiaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d54ab67-9363-486b-a7da-83afef8f9d8a"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HolsterWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27933c65-0c28-4333-905b-d40171cc8177"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26759073-1c2e-4e50-97f1-f6bf351b58e3"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adfd65cd-12ab-4720-9e0f-1b3d3a27d49c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fc4388c-9164-4a1c-a3e4-c459f554a376"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrapplingHook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""cc6dc287-8da7-4026-82eb-e44b396d5c23"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""85d4d9f4-896d-463c-bb7b-c17530b38d7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseVector"",
                    ""type"": ""Value"",
                    ""id"": ""53e1537a-26a8-4ea8-bdb6-bef1d16744bf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftMouse"",
                    ""type"": ""Button"",
                    ""id"": ""8fdce847-1037-4904-9417-52a4e3a83920"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""202fdda7-a09b-4463-8502-c2588b6441bd"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a3887fa-5498-4af4-b879-e3a9f0e3dbf0"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseVector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a6781cf-fb60-4746-95d8-5cc7af02eb2e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player 
        m_Player = asset.FindActionMap("Player ", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_MouseVector = m_Player.FindAction("MouseVector", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_RightGun = m_Player.FindAction("RightGun", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_WeaponSwap = m_Player.FindAction("WeaponSwap", throwIfNotFound: true);
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_PrimaryWeapon = m_Player.FindAction("PrimaryWeapon", throwIfNotFound: true);
        m_Player_SecondaryWeapon = m_Player.FindAction("SecondaryWeapon", throwIfNotFound: true);
        m_Player_TertiaryWeapon = m_Player.FindAction("TertiaryWeapon", throwIfNotFound: true);
        m_Player_HolsterWeapon = m_Player.FindAction("HolsterWeapon", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_GrapplingHook = m_Player.FindAction("GrapplingHook", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_Interact = m_Interaction.FindAction("Interact", throwIfNotFound: true);
        m_Interaction_MouseVector = m_Interaction.FindAction("MouseVector", throwIfNotFound: true);
        m_Interaction_LeftMouse = m_Interaction.FindAction("LeftMouse", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player 
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_MouseVector;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_RightGun;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_WeaponSwap;
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_PrimaryWeapon;
    private readonly InputAction m_Player_SecondaryWeapon;
    private readonly InputAction m_Player_TertiaryWeapon;
    private readonly InputAction m_Player_HolsterWeapon;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_GrapplingHook;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @MouseVector => m_Wrapper.m_Player_MouseVector;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @RightGun => m_Wrapper.m_Player_RightGun;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @WeaponSwap => m_Wrapper.m_Player_WeaponSwap;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @PrimaryWeapon => m_Wrapper.m_Player_PrimaryWeapon;
        public InputAction @SecondaryWeapon => m_Wrapper.m_Player_SecondaryWeapon;
        public InputAction @TertiaryWeapon => m_Wrapper.m_Player_TertiaryWeapon;
        public InputAction @HolsterWeapon => m_Wrapper.m_Player_HolsterWeapon;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @GrapplingHook => m_Wrapper.m_Player_GrapplingHook;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @MouseVector.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseVector;
                @MouseVector.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseVector;
                @MouseVector.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseVector;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @RightGun.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightGun;
                @RightGun.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightGun;
                @RightGun.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightGun;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @WeaponSwap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponSwap;
                @WeaponSwap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponSwap;
                @WeaponSwap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponSwap;
                @Crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @PrimaryWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryWeapon;
                @PrimaryWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryWeapon;
                @PrimaryWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryWeapon;
                @SecondaryWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryWeapon;
                @SecondaryWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryWeapon;
                @SecondaryWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryWeapon;
                @TertiaryWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTertiaryWeapon;
                @TertiaryWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTertiaryWeapon;
                @TertiaryWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTertiaryWeapon;
                @HolsterWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHolsterWeapon;
                @HolsterWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHolsterWeapon;
                @HolsterWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHolsterWeapon;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @GrapplingHook.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapplingHook;
                @GrapplingHook.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapplingHook;
                @GrapplingHook.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapplingHook;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MouseVector.started += instance.OnMouseVector;
                @MouseVector.performed += instance.OnMouseVector;
                @MouseVector.canceled += instance.OnMouseVector;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @RightGun.started += instance.OnRightGun;
                @RightGun.performed += instance.OnRightGun;
                @RightGun.canceled += instance.OnRightGun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @WeaponSwap.started += instance.OnWeaponSwap;
                @WeaponSwap.performed += instance.OnWeaponSwap;
                @WeaponSwap.canceled += instance.OnWeaponSwap;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @PrimaryWeapon.started += instance.OnPrimaryWeapon;
                @PrimaryWeapon.performed += instance.OnPrimaryWeapon;
                @PrimaryWeapon.canceled += instance.OnPrimaryWeapon;
                @SecondaryWeapon.started += instance.OnSecondaryWeapon;
                @SecondaryWeapon.performed += instance.OnSecondaryWeapon;
                @SecondaryWeapon.canceled += instance.OnSecondaryWeapon;
                @TertiaryWeapon.started += instance.OnTertiaryWeapon;
                @TertiaryWeapon.performed += instance.OnTertiaryWeapon;
                @TertiaryWeapon.canceled += instance.OnTertiaryWeapon;
                @HolsterWeapon.started += instance.OnHolsterWeapon;
                @HolsterWeapon.performed += instance.OnHolsterWeapon;
                @HolsterWeapon.canceled += instance.OnHolsterWeapon;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @GrapplingHook.started += instance.OnGrapplingHook;
                @GrapplingHook.performed += instance.OnGrapplingHook;
                @GrapplingHook.canceled += instance.OnGrapplingHook;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_Interact;
    private readonly InputAction m_Interaction_MouseVector;
    private readonly InputAction m_Interaction_LeftMouse;
    public struct InteractionActions
    {
        private @PlayerInput m_Wrapper;
        public InteractionActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Interaction_Interact;
        public InputAction @MouseVector => m_Wrapper.m_Interaction_MouseVector;
        public InputAction @LeftMouse => m_Wrapper.m_Interaction_LeftMouse;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @MouseVector.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnMouseVector;
                @MouseVector.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnMouseVector;
                @MouseVector.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnMouseVector;
                @LeftMouse.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnLeftMouse;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @MouseVector.started += instance.OnMouseVector;
                @MouseVector.performed += instance.OnMouseVector;
                @MouseVector.canceled += instance.OnMouseVector;
                @LeftMouse.started += instance.OnLeftMouse;
                @LeftMouse.performed += instance.OnLeftMouse;
                @LeftMouse.canceled += instance.OnLeftMouse;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouseVector(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnRightGun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnWeaponSwap(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnPrimaryWeapon(InputAction.CallbackContext context);
        void OnSecondaryWeapon(InputAction.CallbackContext context);
        void OnTertiaryWeapon(InputAction.CallbackContext context);
        void OnHolsterWeapon(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnGrapplingHook(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnMouseVector(InputAction.CallbackContext context);
        void OnLeftMouse(InputAction.CallbackContext context);
    }
}
