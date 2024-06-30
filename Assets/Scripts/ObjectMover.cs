using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public static event Action OnMoveEnd;

    [SerializeField]
    private float _movingSpeed = 10;

    private void Start()
    {
        MovableObject.OnMove += MoveToPosition;
    }

    private void OnDestroy()
    {
        MovableObject.OnMove -= MoveToPosition;
    }

    private void MoveToPosition(MovableObject obj, Vector3 position)
    {
        Move(obj, position).Forget();
    }

    private async UniTask Move(MovableObject obj, Vector3 position)
    {
        obj.enabled = false;
        var transform = obj.transform;
        while (Vector3.Distance(position, transform.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, _movingSpeed * Time.deltaTime);
            await UniTask.Yield();
        }
        obj.enabled = true;
        OnMoveEnd?.Invoke();
    }

}
