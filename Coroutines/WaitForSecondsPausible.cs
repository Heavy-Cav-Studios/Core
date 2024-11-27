using System;
using UnityEngine;

namespace HeavyCavStudios.Core.Coroutines
{
    /// <summary>
    /// A custom yield instruction that pauses the wait time based on a condition.
    /// </summary>
    /// <remarks>
    /// This class provides a way to pause waiting based on a given condition. The wait time countdown will stop while the condition is true.
    /// </remarks>
    public class WaitForSecondsPausable : CustomYieldInstruction
    {
        private float m_WaitTime;
        private Func<bool> m_IsPaused;

        /// <summary>
        /// Gets a value indicating whether the wait operation should continue.
        /// </summary>
        public override bool keepWaiting
        {
            get
            {
                if (m_IsPaused())
                {
                    return true; // Keep waiting if paused
                }

                m_WaitTime -= Time.deltaTime;
                return m_WaitTime > 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitForSecondsPausable"/> class.
        /// </summary>
        /// <param name="time">The wait time in seconds.</param>
        /// <param name="pauseCondition">A function that determines whether the wait should be paused.</param>
        public WaitForSecondsPausable(float time, Func<bool> pauseCondition)
        {
            m_WaitTime = time;
            m_IsPaused = pauseCondition;
        }
    }
}
