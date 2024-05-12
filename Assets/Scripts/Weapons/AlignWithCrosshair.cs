using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Weapons
{
    [DisallowMultipleComponent]
    public class AlignWithCrosshair : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_crosshair = null;
        [SerializeField]
        private LayerMask m_rayMask;

        private UI_InGameInteraction m_uiController = null;
        private bool m_isMovementFinished = true;
        private Vector3 m_startCrosshairPosition = Vector3.zero;
        private Vector3 m_initialCrosshairPosition = Vector3.zero;
        private Vector3 m_offsetCrosshairPosition = Vector3.zero;

        private void Awake()
        {
            m_uiController = FindAnyObjectByType<UI_InGameInteraction>();
            m_startCrosshairPosition = m_crosshair.position;
            m_initialCrosshairPosition = m_crosshair.position;

        }

        private void Update()
        {
            if (m_isMovementFinished)
            {
                CalculateCrosshairMovement();
            }

            MoveCrosshair();
            Ray ray = Camera.main.ScreenPointToRay(m_crosshair.position);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, Time.deltaTime);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, m_rayMask))
            {
                transform.LookAt(hitInfo.point);
            }
        }

        private void CalculateCrosshairMovement()
        {
            m_isMovementFinished = false;
            if (m_uiController.DrunkProgressBarValue.value > 90)
            {
                m_offsetCrosshairPosition = m_initialCrosshairPosition - m_startCrosshairPosition;
                return;
            }

            float range = (m_uiController.DrunkProgressBarValue.value - 100) * 10;

            float randomX = Random.Range(-range, range);
            float randomY = Random.Range(-range, range);

            m_offsetCrosshairPosition = new Vector3(randomX, randomY, 0);
        }

        private void MoveCrosshair()
        {
            Vector3 newCrosshairPosition = Vector3.Lerp(m_startCrosshairPosition, m_startCrosshairPosition + m_offsetCrosshairPosition, Time.deltaTime);

            if (Vector3.Distance(m_crosshair.position, newCrosshairPosition) < 0.1f)
            {
                m_startCrosshairPosition = newCrosshairPosition;
                m_isMovementFinished = true;
            }

            m_crosshair.position = newCrosshairPosition;
        }

        private void OnDrawGizmos()
        {
            if (m_crosshair == null)
                return;

            Gizmos.color = Color.magenta;
            Ray ray = Camera.main.ScreenPointToRay(m_crosshair.position);
            Gizmos.DrawRay(ray);
        }
    }
}
