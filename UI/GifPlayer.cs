using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeavyCavStudios.Core.UI
{
    public class GifPlayer : MonoBehaviour
    {
        public RawImage rawImage; // The UI element to display the GIF
        public TextAsset gifFile; // GIF file loaded as a TextAsset
        public float frameDelay = 0.1f; // Delay between frames in seconds

        private List<Texture2D> gifFrames = new List<Texture2D>();

        void Start()
        {
            // Load the GIF and extract frames
            LoadGifFrames();

            // Play the GIF
            StartCoroutine(PlayGif());
        }

        void LoadGifFrames()
        {
            // Decode GIF frames using a library or manual frame extraction
            // Here, assume DecodeGif returns a list of Texture2D frames
            gifFrames = DecodeGif(gifFile.bytes); // Replace with actual GIF decoding logic
        }

        IEnumerator PlayGif()
        {
            int frameIndex = 0;
            while (true)
            {
                if (gifFrames.Count == 0)
                    yield break;

                rawImage.texture = gifFrames[frameIndex];
                frameIndex = (frameIndex + 1) % gifFrames.Count;
                yield return new WaitForSeconds(frameDelay);
            }
        }

        List<Texture2D> DecodeGif(byte[] gifData)
        {
            // Use a library like GifDecoder to decode the frames
            // Placeholder: Replace with the actual decoding code
            return new List<Texture2D>();
        }
    }
}