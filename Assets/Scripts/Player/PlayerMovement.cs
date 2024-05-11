using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public Vector2 MovementInput { get; set; } = Vector2.zero;
        public Vector2 RotationInput { get; set; } = Vector2.zero;

        [SerializeField]
        private float m_movementSpeed = 5.0f;
        [SerializeField]
        private float m_rotationSpeed = 60.0f;

        [SerializeField]
        private Transform m_pitchTransform = null;

        private Rigidbody m_rigidbody = null;
        private Camera m_camera = null;

        private void Awake()
        {
            m_camera = Camera.main;
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 velocity = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1)) * MovementInput.y
                + Vector3.Scale(m_camera.transform.right, new Vector3(1, 0, 1)) * MovementInput.x;
            velocity = velocity.normalized * m_movementSpeed;
            m_rigidbody.velocity = velocity;

            Vector3 pitchRotation = m_pitchTransform.localEulerAngles;
            pitchRotation.x += RotationInput.y * m_rotationSpeed * Time.deltaTime;

            m_pitchTransform.localEulerAngles = pitchRotation;

            Vector3 yawRotation = transform.localEulerAngles;
            yawRotation.y += RotationInput.x * m_rotationSpeed * Time.deltaTime;

            transform.localEulerAngles = yawRotation;
        }

        public void OnMove(InputAction.CallbackContext _context)
        {
            MovementInput = _context.ReadValue<Vector2>();
        }

        public void OnRotate(InputAction.CallbackContext _context)
        {
            RotationInput = _context.ReadValue<Vector2>();
        }
    }
}