using UnityEngine;

//[ExecuteInEditMode()]
public class BloquearPosicionRT : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 startPosition;

    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = transform.position;
        Debug.Log("This is my start position: " + startPosition);
    }

    private void LateUpdate()
    {
        if (startPosition != null)
        {
            rectTransform.position = startPosition;
            Debug.Log("This is my current start position: " + startPosition);
            Debug.Log("This is my current transform position: " + transform.position);
        }
    }
}