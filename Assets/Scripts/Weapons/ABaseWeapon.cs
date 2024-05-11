using System.Collections;
using UnityEngine;

namespace Weapons
{
    [DisallowMultipleComponent]
    public abstract class ABaseWeapon : MonoBehaviour
    {
        public event System.Action<ABaseWeapon> OnActionStarted
        {
            add
            {
                m_onActionStarted -= value;
                m_onActionStarted += value;
            }
            remove
            {
                m_onActionStarted -= value;
            }
        }

        public event System.Action<ABaseWeapon> OnActionFinished
        {
            add
            {
                m_onActionFinished -= value;
                m_onActionFinished += value;
            }
            remove
            {
                m_onActionFinished -= value;
            }
        }

        public event System.Action<ABaseWeapon> OnReloadStarted
        {
            add
            {
                m_onReloadStarted -= value;
                m_onReloadStarted += value;
            }
            remove
            {
                m_onReloadStarted -= value;
            }
        }

        public event System.Action<ABaseWeapon> OnReloadFinished
        {
            add
            {
                m_onReloadFinished -= value;
                m_onReloadFinished += value;
            }
            remove
            {
                m_onReloadFinished -= value;
            }
        }

        private event System.Action<ABaseWeapon> m_onActionStarted;
        private event System.Action<ABaseWeapon> m_onActionFinished;
        private event System.Action<ABaseWeapon> m_onReloadStarted;
        private event System.Action<ABaseWeapon> m_onReloadFinished;

        private Coroutine m_reloadCoroutine = null;
        private Coroutine m_actionCorutine = null;

        public abstract void FullRestore();

        public void StartReload()
        {
            if (m_reloadCoroutine != null || m_actionCorutine != null)
                return;

            m_reloadCoroutine = StartCoroutine(PerformReload());
            m_onReloadStarted?.Invoke(this);
        }

        public void StartAction()
        {
            if (m_actionCorutine != null || m_reloadCoroutine != null)
                return;

            m_actionCorutine = StartCoroutine(PerformAction());
            m_onActionStarted?.Invoke(this);
        }

        protected void ReloadFinished()
        {
            m_reloadCoroutine = null;
            m_onReloadFinished?.Invoke(this);
        }

        protected void ActionFinished()
        {
            m_actionCorutine = null;
            m_onActionFinished?.Invoke(this);
        }

        protected abstract IEnumerator PerformReload();
        protected abstract IEnumerator PerformAction();
    }
}
