using System;
using System.Collections;
using UnityEngine;

namespace HeavyCavStudios.Core.Coroutines
{
    /// <summary>
    /// Represents a debounced action with a value of type <typeparamref name="T"/>.
    /// Executes the action after a specified delay, canceling any previous calls made within that time frame.
    /// </summary>
    /// <typeparam name="T">The type of the value to be passed to the action.</typeparam>
    public class DebouncedWithValue<T>
    {
        float m_WaitInSeconds;
        Action<T> m_Handler;

        IEnumerator m_Debounced;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebouncedWithValue{T}"/> class.
        /// </summary>
        /// <param name="handler">The action to be executed after the delay.</param>
        /// <param name="waitTime">The delay time in seconds.</param>
        public DebouncedWithValue(Action<T> handler, float waitTime)
        {
            m_Handler = handler;
            m_WaitInSeconds = waitTime;
        }

        /// <summary>
        /// Executes the action after a delay, canceling any previous calls within the delay period.
        /// </summary>
        /// <param name="value">The value to pass to the action.</param>
        /// <param name="context">The MonoBehaviour context used to start the coroutine.</param>
        public void Debounce(T value, MonoBehaviour context)
        {
            if (m_Debounced != null)
            {
                context.StopCoroutine(m_Debounced);
            }

            m_Debounced = DebounceInner(value);
            context.StartCoroutine(m_Debounced);
        }

        IEnumerator DebounceInner(T value)
        {
            yield return new WaitForSeconds(m_WaitInSeconds);

            m_Handler(value);
        }
    }

    /// <summary>
    /// Represents a debounced action without any value.
    /// Executes the action after a specified delay, canceling any previous calls made within that time frame.
    /// </summary>
    public class DebouncedWithoutValue
    {
        float m_WaitInSeconds;
        Action m_Handler;
        IEnumerator m_Debounced;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebouncedWithoutValue"/> class.
        /// </summary>
        /// <param name="handler">The action to be executed after the delay.</param>
        /// <param name="waitTime">The delay time in seconds.</param>
        public DebouncedWithoutValue(Action handler, float waitTime)
        {
            m_Handler = handler;
            m_WaitInSeconds = waitTime;
        }

        /// <summary>
        /// Executes the action after a delay, canceling any previous calls within the delay period.
        /// </summary>
        /// <param name="context">The MonoBehaviour context used to start the coroutine.</param>
        public void Debounce(MonoBehaviour context)
        {
            if (m_Debounced != null)
            {
                context.StopCoroutine(m_Debounced);
            }

            m_Debounced = DebounceInner();
            context.StartCoroutine(m_Debounced);
        }

        IEnumerator DebounceInner()
        {
            yield return new WaitForSeconds(m_WaitInSeconds);

            m_Handler();
        }
    }
}
