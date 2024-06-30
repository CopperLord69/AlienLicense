using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class MovableObject : MonoBehaviour
{
    public static event Action<MovableObject, Vector3> OnMove;

    private Vector2 _startDragPosition;
    private Vector3 _moveDirection;
    [SerializeField]
    private Vector3 _extents = new Vector3(0, .5f, 0);
    private Transform _transform;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Vector3 _gridSize;


    private void Awake()
    {
        _transform = transform;
    }

    public void StartDrag(BaseEventData data)
    {
        if (!enabled)
        {
            return;
        }
        _startDragPosition = ((PointerEventData)data).position;
    }

    public void EndDrag(BaseEventData data)
    {
        if (!enabled)
        {
            return;
        }
        var endPosition = ((PointerEventData)data).position;
        var direction = endPosition - _startDragPosition;
        direction.Normalize();
        direction.x = Mathf.Round(direction.x);
        direction.y = Mathf.Round(direction.y);
        if (direction.x == direction.y)
        {
            direction.y = 0;
        }
        _moveDirection.x = direction.x;
        _moveDirection.z = direction.y;
        if (Physics.BoxCast(_transform.position, _extents, _moveDirection, out RaycastHit hit, _transform.rotation, float.PositiveInfinity, _layerMask, QueryTriggerInteraction.Ignore))
        {
            //ExtDebug.DrawBoxCastOnHit(_transform.position, _extents, _transform.rotation, _moveDirection, hit.distance, Color.green);
            //Debug.Log(hit.collider.gameObject);
            var extents = _moveDirection.x == 0 ? (_transform.rotation * _extents).z : (_transform.rotation * _extents).x;
            var endPoint = hit.point - _moveDirection * Mathf.Abs(extents);
            if (_moveDirection.x != 0)
            {
                endPoint.z = _transform.position.z;
            }
            if (_moveDirection.z != 0)
            {
                endPoint.x = _transform.position.x;
            }
            endPoint.x = Mathf.Round(endPoint.x / _gridSize.x) * _gridSize.x;
            endPoint.y = _transform.position.y;
            endPoint.z = Mathf.Round(endPoint.z / _gridSize.z) * _gridSize.z;
            if (endPoint != _transform.position)
            {
                OnMove?.Invoke(this, endPoint);
            }
        }
        //else
        //{
        //    ExtDebug.DrawBoxCastBox(_transform.position, _extents, _transform.rotation, _moveDirection, 100, Color.red);
        //}
    }
}
