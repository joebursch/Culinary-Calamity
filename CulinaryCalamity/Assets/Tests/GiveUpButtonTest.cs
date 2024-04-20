using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary>
    /// Tests for "Give Up" button (user story #99)
    /// </summary>
    public class MiniGameManagerTests
    {
        private GameObject _player;
        private Player _playerScript;

        /// <summary>
        /// Test setup - runs before every test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MiniGame");
            _player = GameObject.Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject))) as GameObject;
            _playerScript = _player.GetComponent<Player>();
        }

        /// <summary>
        /// Test teardown - runs after every test
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (_player != null)
            {
                GameObject.Destroy(_player);
            }
        }

        /// <summary>
        /// Test AC 1: " "give up" button that returns the player to the restaurant scene, effectively ending the mini-game"
        /// </summary>
        [UnityTest]
        public IEnumerator PressingGiveUp_ReturnsToRestaurantScene()
        {
            GameObject giveUpButton = GameObject.Find("giveUpButton");
            giveUpButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            yield return new WaitForSeconds(1.0f);

            Assert.AreEqual("Restaurant", SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Test AC 2: "If the player gives up, the player does not earn any gold."
        /// </summary>
        [UnityTest]
        public IEnumerator PlayerDoesNotEarnGoldUponGivingUp()
        {
            int initialGold = _playerScript.GetGoldAmount();
            GameObject giveUpButton = GameObject.Find("giveUpButton");
            giveUpButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            yield return new WaitForSeconds(1.0f);

            Assert.AreEqual(initialGold, _playerScript.GetGoldAmount());
        }

        /// <summary>
        /// Test AC 3: "The player object must be reactivated upon loading the restaurant scene."
        /// </summary>
        [UnityTest]
        public IEnumerator PlayerObjectReactivatedInRestaurantScene()
        {
            GameObject giveUpButton = GameObject.Find("giveUpButton");
            giveUpButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            yield return new WaitForSeconds(3.0f);

            Assert.IsNotNull(_player);
            Assert.IsTrue(_player.activeSelf);
        }
    }
}
