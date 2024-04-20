using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Dialogue;

namespace Tests
{
    /// <summary>
    /// Tests for Doors (User story #4)
    /// </summary>
    public class DoorTest
    {
        private GameObject _player;
        private GameObject _doorObject;

        /// <summary>
        /// Load the scene and initialize test objects.
        /// </summary>
        private IEnumerator SetupSceneAndObjects()
        {
            var loadSceneOperation = SceneManager.LoadSceneAsync("BlankTestingScene");
            yield return new WaitUntil(() => loadSceneOperation.isDone);

            _player = GameObject.Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject))) as GameObject;
            _doorObject = GameObject.Instantiate(Resources.Load("Prefabs/Door", typeof(GameObject))) as GameObject;

            if (_player == null || _doorObject == null)
            {
                yield break;
            }
        }

        /// <summary>
        /// Test teardown - runs after every test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(_player);
            GameObject.Destroy(_doorObject);
        }

        /// <summary>
        /// Test AC 1: When the player moves onto an unlocked door, the corresponding scene is loaded.
        /// </summary>
        [UnityTest]
        public IEnumerator Door_TeleportAndLoadScene_WhenDoorIsUnlocked()
        {
            yield return SetupSceneAndObjects();

            Door door = _doorObject.GetComponent<Door>();

            door.SetEntranceScene("BlankTestingScene");
            door.SetDestinationScene("DoorTest");
            door.setUnlocked(true);

            _player.transform.position = door.transform.position;

            yield return new WaitForSeconds(.1f);

            SceneManager.LoadScene("DoorTest");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "DoorTest");

            Assert.AreEqual("DoorTest", SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Test AC 2: When the player moves onto a locked door, no scene is loaded.
        /// </summary>
        [UnityTest]
        public IEnumerator Door_NoAction_WhenDoorIsLocked()
        {
            yield return SetupSceneAndObjects();

            Door door = _doorObject.GetComponent<Door>();

            door.SetEntranceScene("BlankTestingScene");
            door.SetDestinationScene("BlankTestingScene");
            door.setUnlocked(true);

            _player.transform.position = door.transform.position;

            yield return new WaitForSeconds(.1f);

            Assert.AreEqual("BlankTestingScene", SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Test AC 3: When the player interacts with a locked door, a message is displayed stating the door is locked.
        /// </summary>
        [UnityTest]
        public IEnumerator Door_ReceiveMessage_WhenDoorIsLocked()
        {
            yield return SetupSceneAndObjects();

            Door door = _doorObject.GetComponent<Door>();

            door.SetEntranceScene("BlankTestingScene");
            door.SetDestinationScene("BlankTestingScene");
            door.setUnlocked(false);

            _player.transform.position = door.transform.position;

            yield return new WaitForSeconds(.1f);

            Assert.IsTrue(DialogueManager.GetDialogueManager().IsDialogueInProgress());
        }
    }
}