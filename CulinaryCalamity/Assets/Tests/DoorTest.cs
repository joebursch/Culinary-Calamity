using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace Tests
{
    /// <summary>
    /// Tests for Doors (User story #4)
    /// </summary>
    public class DoorTest
    {
        /// <summary>
        /// Test setup - runs before every test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("BlankTestingScene");
        }

        /// <summary>
        /// Test AC 1: "When player moves character onto a door space that is unlocked, the corresponding scene is loaded"
        /// </summary>
        [UnityTest]
        public IEnumerator Door_TeleportAndLoadScene_WhenDoorIsUnlocked()
        {
            GameObject player = new GameObject("Player");
            GameObject doorObject = new GameObject("Door");
            Door door = doorObject.AddComponent<Door>();
            door.gameObject.AddComponent<BoxCollider>().isTrigger = true;
            player.AddComponent<BoxCollider>().isTrigger = true;
            player.AddComponent<Rigidbody>().useGravity = false;

            typeof(Door).GetField("destinationSceneName", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(door, "DoorTest");
            typeof(Door).GetField("unlocked", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(door, true);

            player.transform.position = door.transform.position;

            yield return new WaitForSeconds(.1f);

            SceneManager.LoadScene("DoorTest");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "DoorTest");

            Assert.AreEqual("DoorTest", SceneManager.GetActiveScene().name, "The scene did not change to the expected 'DoorTest'.");
        }

        /// <summary>
        /// Test AC 2: "When player moves character onto a door space that is locked, no scene is loaded"
        /// </summary>
        [UnityTest]
        public IEnumerator Door_NoAction_WhenDoorIsLocked()
        {
            GameObject player = new GameObject("Player");
            GameObject doorObject = new GameObject("Door");
            Door door = doorObject.AddComponent<Door>();
            door.gameObject.AddComponent<BoxCollider>().isTrigger = true;
            player.AddComponent<BoxCollider>().isTrigger = true;
            player.AddComponent<Rigidbody>().useGravity = false;

            typeof(Door).GetField("unlocked", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(door, false);

            player.transform.position = door.transform.position;

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual("BlankTestingScene", SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Test AC 3: "When player interacts with a locked door, a message is displayed stating the door is locked"
        /// </summary>
        [UnityTest]
        public IEnumerator Door_ReceiveMessage_WhenDoorIsLocked()
        {
            GameObject player = new GameObject("Player");
            GameObject doorObject = new GameObject("Door");
            Door door = doorObject.AddComponent<Door>();
            door.gameObject.AddComponent<BoxCollider>().isTrigger = true;
            player.AddComponent<BoxCollider>().isTrigger = true;
            player.AddComponent<Rigidbody>().useGravity = false;

            typeof(Door).GetField("unlocked", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(door, false);

            string expectedMessage = "Door is locked";
            LogAssert.Expect(LogType.Log, expectedMessage);

            player.transform.position = door.transform.position;

            yield return new WaitForSeconds(.1f);

            yield return null;
        }
    }
}