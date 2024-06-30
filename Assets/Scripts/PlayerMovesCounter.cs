using System;
using UnityEngine;

public class PlayerMovesCounter : MonoBehaviour
{
    public static event Action OnMovesChanged;
    public static event Action OnMovesOver;

    [field: SerializeField]
    public int Moves { get; private set; }

    private void Awake()
    {
        ObjectMover.OnMoveEnd += SubstractMove;
        AdsInitializer.OnAdCompleted += TryAddMove;
    }

    

    private void OnDestroy()
    {
        ObjectMover.OnMoveEnd -= SubstractMove;
    }

    private void TryAddMove(AdsInitializer.Ad ad)
    {
        if(ad == AdsInitializer.Ad.Rewarded)
        {
            Moves += 1;
            OnMovesChanged?.Invoke();
        }
    }

    private void SubstractMove()
    {
        Moves -= 1;
        OnMovesChanged?.Invoke();
        if(Moves < 1)
        {
            OnMovesOver?.Invoke();
        }
    }
}
