using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] bool startGame;
        void Start()
        {
            if(startGame)
                SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }
}

