using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int _nextLevelId = 1;

    public void StartGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_nextLevelId);
    }
}
