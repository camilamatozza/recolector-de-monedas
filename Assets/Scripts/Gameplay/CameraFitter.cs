using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraFitter : MonoBehaviour
{
    [SerializeField] private Vector2 worldSize = new Vector2(16f, 9f);

    private void Update()
    {
        Camera cam = GetComponent<Camera>();
        float aspect = (float)Screen.width / Screen.height;
        float byHeight = worldSize.y / 2f;
        float byWidth = worldSize.x / (2f * aspect);
        cam.orthographicSize = Mathf.Max(byHeight, byWidth);
    }
}