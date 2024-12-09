using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HeavyCavStudios.Core.UI
{
    public class GifPlayer : MonoBehaviour
    {
        [SerializeField]
        Texture2D m_GifTexture; // The Texture2D asset to display
        [FormerlySerializedAs("rawImage")]
        [SerializeField]
        RawImage m_RawImage; // RawImage to display the GIF
        [SerializeField]
        float m_FrameDelay = 0.1f; // Delay between frames in seconds

        [SerializeField]
        List<Texture2D> m_GifFrames = new List<Texture2D>();
        [SerializeField]
        int m_CurrentFrame = 0;
        [SerializeField]
        bool m_IsPlaying = false;

        void Start()
        {
            if (m_GifTexture != null)
            {
                LoadGifFrames();
            }
        }

        public void LoadGifFrames()
        {
            // Clear existing frames
            m_GifFrames.Clear();

            if (m_GifTexture == null)
            {
                Debug.LogError("Please assign a valid Texture2D asset.");
                return;
            }

            // Extract frames from the GIF Texture2D
            // Assuming the GIF is split into a grid of frames (e.g., a sprite sheet)
            int frameWidth = m_GifTexture.width / 4; // Example: 4 frames horizontally
            int frameHeight = m_GifTexture.height / 4; // Example: 4 frames vertically

            for (int y = 0; y < m_GifTexture.height; y += frameHeight)
            {
                for (int x = 0; x < m_GifTexture.width; x += frameWidth)
                {
                    Texture2D frame = new Texture2D(frameWidth, frameHeight);
                    frame.SetPixels(m_GifTexture.GetPixels(x, y, frameWidth, frameHeight));
                    frame.Apply();
                    m_GifFrames.Add(frame);
                }
            }

            Debug.Log($"Loaded {m_GifFrames.Count} frames from the GIF Texture2D.");
        }

        public void PlayGif()
        {
            if (!m_IsPlaying && m_GifFrames.Count > 0)
            {
                m_IsPlaying = true;
                StartCoroutine(PlayGifCoroutine());
            }
        }

        public void PauseGif()
        {
            m_IsPlaying = false;
            StopCoroutine(PlayGifCoroutine());
        }

        private IEnumerator PlayGifCoroutine()
        {
            while (m_IsPlaying)
            {
                if (m_GifFrames.Count == 0)
                {
                    Debug.LogError("No frames loaded. Please load frames first.");
                    yield break;
                }

                // Update the RawImage texture with the current frame
                m_RawImage.texture = m_GifFrames[m_CurrentFrame];

                // Advance to the next frame
                m_CurrentFrame = (m_CurrentFrame + 1) % m_GifFrames.Count;

                yield return new WaitForSeconds(m_FrameDelay);
            }
        }
    }
}