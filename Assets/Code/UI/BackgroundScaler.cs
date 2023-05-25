using UnityEngine;

namespace Code.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundScaler : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Camera mainCamera;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main;
            ScaleBackground();
        }

        private void ScaleBackground()
        {
            if (spriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer component not found!");
                return;
            }

            if (mainCamera == null)
            {
                Debug.LogWarning("Main camera not found!");
                return;
            }

            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            float screenHeight = 2f * mainCamera.orthographicSize;
            float screenWidth = screenHeight * mainCamera.aspect;

            Vector3 newScale = transform.localScale;
            newScale.x = screenWidth / spriteWidth;
            newScale.y = screenHeight / spriteHeight;

            transform.localScale = newScale + Vector3.one;
        }
    }
}