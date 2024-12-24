using UnityEngine;

namespace Helpers
{
    public class CameraAdjuster : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;

        public void AdjustCameraToGrid(int gridWidth, int gridHeight)
        {
            if (gameCamera == null)
            {
                Debug.LogError("Camera is not assigned!");
                return;
            }
            
            float gridDiagonal = Mathf.Sqrt(gridWidth * gridWidth + gridHeight * gridHeight);
            
            float verticalFOV = gameCamera.fieldOfView;
            float horizontalFOV = Camera.VerticalToHorizontalFieldOfView(verticalFOV, gameCamera.aspect);
            
            float verticalFOVRad = verticalFOV * Mathf.Deg2Rad;
            float horizontalFOVRad = horizontalFOV * Mathf.Deg2Rad;
            
            float distanceForHeight = (gridHeight / 2f) / Mathf.Tan(verticalFOVRad / 2f);
            float distanceForWidth = (gridWidth / 2f) / Mathf.Tan(horizontalFOVRad / 2f);
            float requiredDistance = Mathf.Max(distanceForHeight, distanceForWidth);
            
            float tiltAngleRad = 32f * Mathf.Deg2Rad;
            requiredDistance /= Mathf.Cos(tiltAngleRad);
            
            Vector3 gridCenter = new Vector3(gridWidth / 2f, 0, gridHeight / 2f);
            Vector3 offset = new Vector3(-Mathf.Sin(23f * Mathf.Deg2Rad), Mathf.Sin(tiltAngleRad), -Mathf.Cos(23f * Mathf.Deg2Rad)) * requiredDistance;
            gameCamera.transform.position = gridCenter + offset;
            gameCamera.transform.LookAt(gridCenter);
        }
    }
}