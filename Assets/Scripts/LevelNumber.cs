using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _value;
    private void Awake()
    {
        _value.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
}
