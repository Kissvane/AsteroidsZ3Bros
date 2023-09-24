using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapPosition : MonoBehaviour
{
    [SerializeField] private SpriteRenderer myRenderer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private Transform myTransform;
    private bool isWrappingX;
    private bool isWrappingY;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    bool IsInsideScreen()
    {
        float top = myCollider.offset.y + (myCollider.bounds.size.y / 2f);
        float btm = myCollider.offset.y - (myCollider.bounds.size.y / 2f);
        float left = myCollider.offset.x - (myCollider.bounds.size.x / 2f);
        float right = myCollider.offset.x + (myCollider.bounds.size.x / 2f);

        Vector3 topLeft = myTransform.TransformPoint(new Vector3(left, top, 0f));
        Vector3 topRight = myTransform.TransformPoint(new Vector3(right, top, 0f));
        Vector3 btmLeft = myTransform.TransformPoint(new Vector3(left, btm, 0f));
        Vector3 btmRight = myTransform.TransformPoint(new Vector3(right, btm, 0f));

        return !IsOutsideScreen(topLeft) || !IsOutsideScreen(topRight) 
            || !IsOutsideScreen(btmLeft) || !IsOutsideScreen(btmRight);
    }

    private void LateUpdate()
    {
        Wrap();
    }

    private bool IsOutsideScreen(Vector3 position)
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }

    void Wrap()
    {
        if (IsInsideScreen())
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
        if (isWrappingX && isWrappingY)
        {
            return;
        }
        var viewportPosition = mainCamera.WorldToViewportPoint(myTransform.position);
        var newPosition = myTransform.position;
        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }
        myTransform.position = newPosition;
    }
}
