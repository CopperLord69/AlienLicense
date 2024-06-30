using TMPro;
using UnityEngine;

public class MovesSetter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _value;
    [SerializeField]
    private PlayerMovesCounter _counter;

    private void Awake()
    {
        PlayerMovesCounter.OnMovesChanged += WriteValue;
    }

    private void Start()
    {
        WriteValue();
    }

    private void OnDestroy()
    {
        PlayerMovesCounter.OnMovesChanged -= WriteValue;
    }

    private void WriteValue()
    {
        _value.text = _counter.Moves.ToString();
    }
}
