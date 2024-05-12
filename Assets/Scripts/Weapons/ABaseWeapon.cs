using System.Collections;
using UnityEngine;

namespace Weapons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public abstract class ABaseWeapon : MonoBehaviour
    {
        public event System.Action<ABaseWeapon> OnPrimaryActionStarted
        {
            add
            {
                m_onPrimaryActionStarted -= value;
                m_onPrimaryActionStarted += value;
            }
            remove
            {
                m_onPrimaryActionStarted -= value;
            }
        }

        public event System.Action<ABaseWeapon> OnPrimaryActionFinished
        {
            add
            {
                m_onPrimaryActionFinished -= value;
                m_onPrimaryActionFinished += value;
            }
            remove
            {
                m_onPrimaryActionFinished -= value;
            }
        }

        public event System.Action<ABaseWeapon> OnSecondaryActionStarted
        {
            add
            {
                m_onSecondaryActionStarted -= value;
                m_onSecondaryActionStarted += value;
            }
            remove
            {
                m_onSecondaryActionStarted -= value;
            }
        }

        public event System.Action<ABaseWeapon> OnSecondaryActionFinished
        {
            add
            {
                m_onSecondaryActionFinished -= value;
                m_onSecondaryActionFinished += value;
            }
            remove
            {
                m_onSecondaryActionFinished -= value;
            }
        }

        public bool IsInUse => m_primaryActionCorutine != null || m_secondaryActionCoroutine != null;

        [SerializeField]
        protected AudioClip m_primaryActionClip = null;
        [SerializeField]
        protected AudioClip m_secondaryActionClip = null;

        private event System.Action<ABaseWeapon> m_onPrimaryActionStarted;
        private event System.Action<ABaseWeapon> m_onPrimaryActionFinished;
        private event System.Action<ABaseWeapon> m_onSecondaryActionStarted;
        private event System.Action<ABaseWeapon> m_onSecondaryActionFinished;

        private Coroutine m_secondaryActionCoroutine = null;
        private Coroutine m_primaryActionCorutine = null;

        protected AudioSource m_audioSource = null;
        protected Animator m_animator = null;

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_animator = GetComponentInChildren<Animator>();
        }

        public abstract void FullRestore();
        public virtual void Reset()
        {
            if (m_primaryActionCorutine != null)
            {
                StopCoroutine(m_primaryActionCorutine);
                m_primaryActionCorutine = null;
            }

            if (m_secondaryActionCoroutine != null)
            {
                StopCoroutine(m_secondaryActionCoroutine);
                m_secondaryActionCoroutine = null;
            }
        }

        public void StartSecondaryAction()
        {
            if (m_secondaryActionCoroutine != null || m_primaryActionCorutine != null)
                return;

            m_secondaryActionCoroutine = StartCoroutine(PerformSecondaryAction());
            m_onSecondaryActionStarted?.Invoke(this);
        }

        public void StartPrimaryAction()
        {
            if (m_primaryActionCorutine != null || m_secondaryActionCoroutine != null)
                return;

            m_primaryActionCorutine = StartCoroutine(PerformPrimaryAction());
            m_onPrimaryActionStarted?.Invoke(this);
        }

        protected void SecondaryActionFinished()
        {
            m_secondaryActionCoroutine = null;
            m_onSecondaryActionFinished?.Invoke(this);
        }

        protected void PrimaryActionFinished()
        {
            m_primaryActionCorutine = null;
            m_onPrimaryActionFinished?.Invoke(this);
        }

        protected abstract IEnumerator PerformSecondaryAction();
        protected abstract IEnumerator PerformPrimaryAction();
    }
}
