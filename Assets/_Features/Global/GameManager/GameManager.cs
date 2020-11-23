using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool startGame;
        [SerializeField] private GameEvent _onLose;
        void Start()
        {
            if(startGame)
                SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
        public void RaiseOnLose()=> _onLose.Raise();
        public void GameOver()
        {
            
            Debug.Log("YOU LOST");
        }
    }
}

