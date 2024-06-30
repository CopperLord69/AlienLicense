using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public UnityEvent OnVictory;
    public UnityEvent OnDefeat;
    private bool _isWon;
    private bool _isLost;


    private void Awake()
    {
        WinTrigger.OnWin += Win;
        Character.OnWakeUp += Defeat;
        PlayerMovesCounter.OnMovesOver += Defeat;
    }


    private void OnDestroy()
    {
        WinTrigger.OnWin -= Win;
        Character.OnWakeUp -= Defeat;
        PlayerMovesCounter.OnMovesOver -= Defeat;
    }

    public void AdsDestroyed()
    {
        AdsInitializer.Instance.DestroyAnnoying();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
        else
        {
            RestartLevel();
        }
    }

    private void Defeat()
    {
        if(_isWon)
        {
            return;
        }
        _isLost = true;
        OnDefeat?.Invoke();
    }

    private void Win()
    {
        if(_isLost)
        {
            return;
        }
        _isWon = true;
        OnVictory?.Invoke();
    }
}
