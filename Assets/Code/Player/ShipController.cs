using Code.Player.Configs;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class ShipController : MonoBehaviour
    {
        [Inject] private PlayerActionConfig playerActionConfig;
        [Inject] private Joystick joystick;

        private Camera _mainCamera;
        private float _screenWidth;
        private float _screenHeight;

        private void Start()
        {
            _mainCamera = Camera.main;
            _screenHeight = 2f * _mainCamera.orthographicSize;
            _screenWidth = _screenHeight * _mainCamera.aspect;
        }

        private void Update()
        {
           /* var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");*/
           
           var moveHorizontal = joystick.Direction.x;
           var moveVertical = joystick.Direction.y;

           var movement = new Vector3(moveHorizontal, moveVertical, 0f).normalized * playerActionConfig.Speed;
           var newPosition = transform.position + movement * Time.deltaTime;
           var smoothPosition = Vector3.Lerp(transform.position, newPosition, playerActionConfig.Inertia);

           if (Mathf.Abs(smoothPosition.x - _mainCamera.transform.position.x) > _screenWidth / 2f)
           {
               smoothPosition.x = -smoothPosition.x;
           }

           if (Mathf.Abs(smoothPosition.y - _mainCamera.transform.position.y) > _screenHeight / 2f)
           {
               smoothPosition.y = -smoothPosition.y;
           }

           transform.position = smoothPosition;

           if (movement != Vector3.zero)
           {
               var angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
               transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           }
        }
    }
}