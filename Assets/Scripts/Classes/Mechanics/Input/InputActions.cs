//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Input/InputActions.inputactions
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

namespace Shard.Input
{
    public partial class @InputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7de28bc6-888d-4eca-bd3f-40d99acd987f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""506ea27e-1793-4651-9d68-c3d2598314a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""860f361a-d943-42cc-a535-4936e4c37d29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ReCall"",
                    ""type"": ""Button"",
                    ""id"": ""d65b957d-c381-44f9-b5d3-f9be48cc7a55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""c436d846-d79c-49dd-88cd-622a8fa5396c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""421c135c-5ba4-459d-9264-414383613ce6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Program"",
                    ""type"": ""Button"",
                    ""id"": ""cd44c5e2-2bfc-439e-9202-f6e1004a5674"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""054fce41-c651-4e70-b2ae-8f08c777aa8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ReturnToCheckpoint"",
                    ""type"": ""Button"",
                    ""id"": ""7473dc43-3fa8-46a5-b335-f27abf757230"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""a948e656-5862-4a7d-835c-7eb6fb998af7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""93858679-1da7-4e82-b6be-da33336c7c1a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""64182fa7-cdb6-4d2e-8a25-723a4a857602"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c611128b-40d7-4d12-b754-ff4ac9a4c25c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7d3e90ad-c3b3-4563-8aa9-02226687b207"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d53062a-de12-43a8-be8c-b6e4ef232fd2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fafb2f4e-763c-4dc1-af9a-88b569c231c2"",
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
                    ""id"": ""1669f3fc-15b0-4b3d-9e06-5397f224692f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReCall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d0c2b5c-b1c9-4469-abd1-e2cc6e62ee4e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7dbf0c2-01e5-4a8e-9de5-80bc93642008"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""490b2366-4c51-409d-9c73-c36492e45cff"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Program"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f1e9867-4cc5-4067-b991-01d210607fb5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67412186-9de4-4647-8794-858ddac53a78"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReturnToCheckpoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69290ca4-ecd9-445a-9d8d-5603f1ad02e8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CommandConsole"",
            ""id"": ""2b5b6980-3122-4d1d-9acb-bd4d1c250c85"",
            ""actions"": [
                {
                    ""name"": ""PreviousCommand"",
                    ""type"": ""Button"",
                    ""id"": ""fd2af30a-7490-4b18-a221-630b2d781312"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextCommand"",
                    ""type"": ""Button"",
                    ""id"": ""b8c511d1-ffd0-4fb5-ab8b-1456ca8c640e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ddb7f732-c486-4c92-949e-917b69c1db0d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousCommand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16b9d9f3-4d52-4104-ae7c-7a7fde18ea51"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextCommand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
            m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
            m_Player_ReCall = m_Player.FindAction("ReCall", throwIfNotFound: true);
            m_Player_Grab = m_Player.FindAction("Grab", throwIfNotFound: true);
            m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
            m_Player_Program = m_Player.FindAction("Program", throwIfNotFound: true);
            m_Player_Drop = m_Player.FindAction("Drop", throwIfNotFound: true);
            m_Player_ReturnToCheckpoint = m_Player.FindAction("ReturnToCheckpoint", throwIfNotFound: true);
            m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
            // CommandConsole
            m_CommandConsole = asset.FindActionMap("CommandConsole", throwIfNotFound: true);
            m_CommandConsole_PreviousCommand = m_CommandConsole.FindAction("PreviousCommand", throwIfNotFound: true);
            m_CommandConsole_NextCommand = m_CommandConsole.FindAction("NextCommand", throwIfNotFound: true);
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
        private readonly InputAction m_Player_Movement;
        private readonly InputAction m_Player_Jump;
        private readonly InputAction m_Player_ReCall;
        private readonly InputAction m_Player_Grab;
        private readonly InputAction m_Player_Crouch;
        private readonly InputAction m_Player_Program;
        private readonly InputAction m_Player_Drop;
        private readonly InputAction m_Player_ReturnToCheckpoint;
        private readonly InputAction m_Player_Pause;
        public struct PlayerActions
        {
            private @InputActions m_Wrapper;
            public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Player_Movement;
            public InputAction @Jump => m_Wrapper.m_Player_Jump;
            public InputAction @ReCall => m_Wrapper.m_Player_ReCall;
            public InputAction @Grab => m_Wrapper.m_Player_Grab;
            public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
            public InputAction @Program => m_Wrapper.m_Player_Program;
            public InputAction @Drop => m_Wrapper.m_Player_Drop;
            public InputAction @ReturnToCheckpoint => m_Wrapper.m_Player_ReturnToCheckpoint;
            public InputAction @Pause => m_Wrapper.m_Player_Pause;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @ReCall.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReCall;
                    @ReCall.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReCall;
                    @ReCall.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReCall;
                    @Grab.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrab;
                    @Grab.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrab;
                    @Grab.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrab;
                    @Crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                    @Crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                    @Crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                    @Program.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProgram;
                    @Program.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProgram;
                    @Program.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProgram;
                    @Drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                    @Drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                    @Drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                    @ReturnToCheckpoint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReturnToCheckpoint;
                    @ReturnToCheckpoint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReturnToCheckpoint;
                    @ReturnToCheckpoint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReturnToCheckpoint;
                    @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @ReCall.started += instance.OnReCall;
                    @ReCall.performed += instance.OnReCall;
                    @ReCall.canceled += instance.OnReCall;
                    @Grab.started += instance.OnGrab;
                    @Grab.performed += instance.OnGrab;
                    @Grab.canceled += instance.OnGrab;
                    @Crouch.started += instance.OnCrouch;
                    @Crouch.performed += instance.OnCrouch;
                    @Crouch.canceled += instance.OnCrouch;
                    @Program.started += instance.OnProgram;
                    @Program.performed += instance.OnProgram;
                    @Program.canceled += instance.OnProgram;
                    @Drop.started += instance.OnDrop;
                    @Drop.performed += instance.OnDrop;
                    @Drop.canceled += instance.OnDrop;
                    @ReturnToCheckpoint.started += instance.OnReturnToCheckpoint;
                    @ReturnToCheckpoint.performed += instance.OnReturnToCheckpoint;
                    @ReturnToCheckpoint.canceled += instance.OnReturnToCheckpoint;
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // CommandConsole
        private readonly InputActionMap m_CommandConsole;
        private ICommandConsoleActions m_CommandConsoleActionsCallbackInterface;
        private readonly InputAction m_CommandConsole_PreviousCommand;
        private readonly InputAction m_CommandConsole_NextCommand;
        public struct CommandConsoleActions
        {
            private @InputActions m_Wrapper;
            public CommandConsoleActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @PreviousCommand => m_Wrapper.m_CommandConsole_PreviousCommand;
            public InputAction @NextCommand => m_Wrapper.m_CommandConsole_NextCommand;
            public InputActionMap Get() { return m_Wrapper.m_CommandConsole; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CommandConsoleActions set) { return set.Get(); }
            public void SetCallbacks(ICommandConsoleActions instance)
            {
                if (m_Wrapper.m_CommandConsoleActionsCallbackInterface != null)
                {
                    @PreviousCommand.started -= m_Wrapper.m_CommandConsoleActionsCallbackInterface.OnPreviousCommand;
                    @PreviousCommand.performed -= m_Wrapper.m_CommandConsoleActionsCallbackInterface.OnPreviousCommand;
                    @PreviousCommand.canceled -= m_Wrapper.m_CommandConsoleActionsCallbackInterface.OnPreviousCommand;
                    @NextCommand.started -= m_Wrapper.m_CommandConsoleActionsCallbackInterface.OnNextCommand;
                    @NextCommand.performed -= m_Wrapper.m_CommandConsoleActionsCallbackInterface.OnNextCommand;
                    @NextCommand.canceled -= m_Wrapper.m_CommandConsoleActionsCallbackInterface.OnNextCommand;
                }
                m_Wrapper.m_CommandConsoleActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PreviousCommand.started += instance.OnPreviousCommand;
                    @PreviousCommand.performed += instance.OnPreviousCommand;
                    @PreviousCommand.canceled += instance.OnPreviousCommand;
                    @NextCommand.started += instance.OnNextCommand;
                    @NextCommand.performed += instance.OnNextCommand;
                    @NextCommand.canceled += instance.OnNextCommand;
                }
            }
        }
        public CommandConsoleActions @CommandConsole => new CommandConsoleActions(this);
        public interface IPlayerActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnReCall(InputAction.CallbackContext context);
            void OnGrab(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
            void OnProgram(InputAction.CallbackContext context);
            void OnDrop(InputAction.CallbackContext context);
            void OnReturnToCheckpoint(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
        }
        public interface ICommandConsoleActions
        {
            void OnPreviousCommand(InputAction.CallbackContext context);
            void OnNextCommand(InputAction.CallbackContext context);
        }
    }
}
