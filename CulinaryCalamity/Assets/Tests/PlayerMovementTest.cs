using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Dialogue;
using Inventory;
using Items;
using TMPro;

namespace Tests
{
    /// <summary>
    /// Tests for player movement (user story #1)
    /// </summary>
    public class PlayerMovementTest
    {
        private InputTestFixture _input = new InputTestFixture();

        private GameObject _player;

        private Player _playerScript;


        /// <summary>
        /// Test setup - runs before every test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("BlankTestingScene");
            _input.Setup();
            // register action asset controls
            InputSystem.RegisterLayout(playerActions);
            // create player prefab 
            _player = GameObject.Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject))) as GameObject;
            _playerScript = _player.GetComponent<Player>();

        }
        /// <summary>
        /// Test teardown - runs after every test
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _input.TearDown();
            if (_player != null)
            {
                GameObject.Destroy(_player);
            }
        }

        #region KEYBOARD_TESTS 
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Upwards movement using the (w) key on the keyboard
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveUp_UsingKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            // Get position of player before movement
            float previousYPosition = _player.transform.position.y;

            // press and release the "w" key
            _input.Press(keyboard.wKey);
            yield return new WaitForSeconds(1f);
            _input.Release(keyboard.wKey);

            // Get position of player after movement
            float currentYPosition = _player.transform.position.y;

            // Verify that the player has moved up
            Assert.Greater(currentYPosition, previousYPosition);
        }
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Downwards movement using the (s) key on the keyboard
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveDown_UsingKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            // player position prior to input
            float previousYPosition = _player.transform.position.y;

            // press and release the "s" key
            _input.Press(keyboard.sKey);
            yield return new WaitForSeconds(1f);
            _input.Release(keyboard.sKey);

            // player position post input
            float currentYPosition = _player.transform.position.y;

            // Verify that the player has moved down
            Assert.Less(currentYPosition, previousYPosition);
        }
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Left movement using the (a) key on the keyboard
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveLeft_UsingKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            // player position prior to input
            float previousXposition = _player.transform.position.x;

            // press and release the "a" key
            _input.Press(keyboard.aKey);
            yield return new WaitForSeconds(1f);
            _input.Release(keyboard.aKey);

            // player position post input
            float currentXPosition = _player.transform.position.x;

            // Verify that the player has moved left
            Assert.Less(currentXPosition, previousXposition);
        }
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Right movement using the (d) key on the keyboard
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveRight_UsingKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            // player position prior to input
            float previousXposition = _player.transform.position.x;

            // press and release the "d" key
            _input.Press(keyboard.dKey);
            yield return new WaitForSeconds(1f);
            _input.Release(keyboard.dKey);

            // player position post input
            float currentXPosition = _player.transform.position.x;

            // Verify that the player has moved right
            Assert.Greater(currentXPosition, previousXposition);
        }

        /// <summary>
        /// Test AC 2: "Movement control will be limited to up, down, left, right"
        /// 
        /// NOT WORKING: Need to see if we can press two keys at the same time. Currently, the second key press is taking over. 
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_DiagonalMovementsProhibited_UsingKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            // player position prior to input
            float previousXposition = _player.transform.position.x;
            float previousYPosition = _player.transform.position.y;

            // press and release the "d" key
            _input.Press(keyboard.dKey);
            _input.Press(keyboard.wKey);
            yield return new WaitForSeconds(1f);
            _input.Release(keyboard.wKey);
            _input.Release(keyboard.dKey);

            // player position post input
            float currentXPosition = _player.transform.position.x;
            float currentYPosition = _player.transform.position.y;

            // Verify that the player has only moved to the right
            Assert.AreEqual(currentYPosition, previousYPosition);
            Assert.Greater(currentXPosition, previousXposition);

        }
        /// <summary>
        /// Test AC 3: Movement can be impeded by solid objects.
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator Movement_SolidObjectsStopMovement_UsingKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            GameObject.Instantiate(Resources.Load("Prefabs/CollisionTestingObstacle"), new Vector3(_player.transform.position.x + 5, _player.transform.position.y, _player.transform.position.z), Quaternion.identity);
            float unreachableXPosition = _player.transform.position.x + 5;
            // Move
            _input.Press(keyboard.dKey);
            yield return new WaitForSeconds(1f);
            _input.Release(keyboard.dKey);

            float currentXPosition = _player.transform.position.x;

            // Verify the player couldn't move past the solid object
            Assert.Less(currentXPosition, unreachableXPosition);
        }
        #endregion


        #region  GAMEPAD_TESTS
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Upwards movement by pushing up on the left joystick
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveUp_UsingGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            // Get position of player prior to movement
            float previousYPosition = _player.transform.position.y;

            // push joystick up then stop
            _input.Move(gamepad.leftStick, new Vector2(0, 1));
            yield return new WaitForSeconds(1f);
            _input.Move(gamepad.leftStick, new Vector2(0, 0));

            // Get position of player post movement
            float currentYPosition = _player.transform.position.y;

            // Verify that the player has moved up
            Assert.Greater(currentYPosition, previousYPosition);
        }

        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Downwards movement by pushing down on the left joystick
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveDown_UsingGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            // Get position of player prior to movement
            float previousYPosition = _player.transform.position.y;

            // push joystick down then stop
            _input.Move(gamepad.leftStick, new Vector2(0, -1));
            yield return new WaitForSeconds(1f);
            _input.Move(gamepad.leftStick, new Vector2(0, 0));

            // Get position of player post movement
            float currentYPosition = _player.transform.position.y;

            // Verify that the player has moved down
            Assert.Less(currentYPosition, previousYPosition);
        }
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Left movement by pushing left on the left joystick
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveLeft_UsingGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            // Get position of player prior to movement
            float previousXPosition = _player.transform.position.x;

            // push joystick left then stop
            _input.Move(gamepad.leftStick, new Vector2(-1, 0));
            yield return new WaitForSeconds(1f);
            _input.Move(gamepad.leftStick, new Vector2(0, 0));

            // Get position of player post movement
            float currentXPosition = _player.transform.position.x;

            // Verify that the player has moved left
            Assert.Less(currentXPosition, previousXPosition);
        }
        /// <summary>
        /// Test AC 1: "Using predefined controls, input from the player will move the character around designated areas within the game map"
        /// Right movement by pushing right on the left joystick
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_CanMoveRight_UsingGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            // Get position of player prior to movement
            float previousXPosition = _player.transform.position.x;

            // push joystick left then stop
            _input.Move(gamepad.leftStick, new Vector2(1, 0));
            yield return new WaitForSeconds(1f);
            _input.Move(gamepad.leftStick, new Vector2(0, 0));

            // Get position of player post movement
            float currentXPosition = _player.transform.position.x;

            // Verify that the player has moved right
            Assert.Greater(currentXPosition, previousXPosition);
        }

        /// <summary>
        /// Test AC 2: "Movement control will be limited to up, down, left, right"
        /// </summary>
        [UnityTest]
        public IEnumerator Movement_DiagonalMovementsProhibited_UsingGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            // player position prior to input
            float previousXposition = _player.transform.position.x;
            float previousYPosition = _player.transform.position.y;

            //move left joystick
            _input.Move(gamepad.leftStick, new Vector2(1, 1));
            yield return new WaitForSeconds(1f);
            _input.Move(gamepad.leftStick, new Vector2(0, 0));

            // player position post input
            float currentXPosition = _player.transform.position.x;
            float currentYPosition = _player.transform.position.y;

            // Verify that the player has only moved to the right
            Assert.AreEqual(currentYPosition, previousYPosition);
            Assert.Greater(currentXPosition, previousXposition);
        }

        /// <summary>
        /// Test AC 3: Movement can be impeded by solid objects.
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator Movement_SolidObjectsStopMovement_UsingGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            GameObject.Instantiate(Resources.Load("Prefabs/CollisionTestingObstacle"), new Vector3(_player.transform.position.x + 5, _player.transform.position.y, _player.transform.position.z), Quaternion.identity);
            float unreachableXPosition = _player.transform.position.x + 5;
            // Move
            _input.Move(gamepad.leftStick, new Vector2(1, 0));
            yield return new WaitForSeconds(1f);
            _input.Move(gamepad.leftStick, new Vector2(0, 0));

            float currentXPosition = _player.transform.position.x;

            // Verify the player couldn't move past the solid object
            Assert.Less(currentXPosition, unreachableXPosition);
        }

        #endregion

        const string playerActions = @"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""Standard"",
            ""id"": ""ac22a5a9-73f1-4282-a693-9a252f7f3457"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""93efb6b9-3796-4204-b1f8-afe6022ad41c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""438ed283-107c-41dc-b650-6c27807b5b1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""83053dd5-6c0a-4f3d-a5c3-aa88df4d3b3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""d97a1c75-3c5b-4d3e-b49d-613740cb5bf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3d5ddb06-15b8-411d-bbb8-14e7e866692b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""OpenQuestMenu"",
                    ""type"": ""Button"",
                    ""id"": ""173e573d-b361-449e-841f-0e6bbd1cb8e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b27011b7-f8eb-4efe-9ba7-56475e4f2acc"",
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
                    ""id"": ""ca229b57-6783-4238-b057-dd2251f67407"",
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
                    ""id"": ""78075821-971e-41bb-af67-8c1cbc24311d"",
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
                    ""id"": ""93883926-fc0b-4268-9b2f-20d217bff06e"",
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
                    ""id"": ""5ab2cfc9-0358-443f-a21a-d464d59f4811"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c881d62e-7e8b-4623-9276-10c61a1a590a"",
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
                    ""id"": ""5384241f-48a5-45e2-86ad-4e54dd1dce11"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""76201e6b-da35-4b5c-a121-06be521d0932"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b0eacbbd-949e-46c1-9a59-67fb0e8d8d77"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""64aa4757-cf8e-4267-bc99-0b3cbdee6091"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""27001d97-03b2-4339-9a51-dad380cf4c60"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cf61557-b4a1-4555-bbc0-53a8561fef98"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""916384c6-bc6d-44f0-8c87-f2ddbc1905e9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7882b58a-809e-417f-9755-8500bd117772"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98988988-b046-4259-83a9-4e8f5be91f23"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db7e8761-3d0d-4b88-a703-94caf5d52966"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c36b4eff-5bfe-4d1f-b10d-548e2d2736e0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9509d4a-f5aa-4aed-aba1-48d628f0681e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3f5a4fa-1b10-4f2b-88e0-d07c95c4c3f6"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenQuestMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""254e73df-fb0d-4864-816e-b84aea9e73fd"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""389836e8-943e-4915-a24f-63e56a0e39a9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""bdc1c4b3-3ff4-474c-b76d-0667ea2747fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""04bbd1dc-46c6-4f68-ad6f-cac5d1810b9d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ce1ba370-4fbf-4288-a2fe-9bb812668cdc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""24b38b0a-3142-4e7a-953d-1c38689d7bde"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""926f0aed-2fb7-436f-b5b8-4095e8c903c5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8934b30d-43de-4be1-8c08-1f5e6f8f05c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""311b2992-7b9c-4086-95aa-7421b27af318"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f4613ac0-5ea7-4257-b73f-fcd6e36d4821"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""11918031-f49c-4eaf-9321-a406f79fa0dc"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""377fa953-048f-4748-a49b-83fc7f9ac2b2"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cf71b961-4835-45d0-8aa9-a7c9ce9ce2c4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""56178b59-ab6d-4e9a-8877-d564eea0196e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8179fe4c-f7ac-4974-a3a0-2b594213755a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""daf6e556-ef23-47da-89c9-0d5a22ef3e2d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8eaf59d9-b9e7-419a-8aa6-5dc6b405901b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5e308ea8-ea63-4c6a-9846-bcc16b64999a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e2bc1fe8-7fc6-4b58-8b07-1994ff8d78d8"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f1ddc7b-b921-48c2-86cb-b36a6064b541"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42d559ab-6743-4022-bca7-a9f5583252c1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7c5cff0-fcb7-4b77-8c3b-26f851f06afb"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c06f754b-0ed5-4cfe-a2c6-cdc2be9463f6"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b3f0dc15-14a2-4bdc-851f-e260e22d692e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""470d8627-03a7-458d-b17f-37dc467f92e6"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""40486099-00b0-4f26-b4c0-ce61ed0118ac"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""78ffbd84-844c-4856-9de2-ea2e3285d4f3"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""56af39c1-17aa-4687-abd3-544224cb48cb"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f2ba75fc-0f0c-48b8-995d-f265e3444734"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aff8fec8-b17a-4194-827b-7b62f77f54f0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d86d16b3-003d-4f5b-81a4-112c2ffc0ede"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9882f2e2-98fb-48ed-b5ef-7d24b2c45d77"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""cbdca17f-a5aa-4079-825f-06e77694afd3"",
            ""actions"": [
                {
                    ""name"": ""AdvanceDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""71ee8dac-8b02-4284-b2d8-1b4008e37474"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a379c157-a3bb-41f9-b854-d99960735bfb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1e3ea96-ace4-4da1-9877-586ddd65b4b1"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}";
    }
}
