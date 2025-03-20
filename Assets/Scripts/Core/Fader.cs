using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvaGroup;

        // Start is called before the first frame update
        void Start()
        {
            canvaGroup = GetComponent<CanvasGroup>();
        }


        public IEnumerator FadeOut(float time)
        {
            while (canvaGroup.alpha < 1f)
            {
                canvaGroup.alpha += Time.deltaTime / time;
                yield return null;
            }

        }

        public IEnumerator FadeIn(float time)
        {
            while (canvaGroup.alpha > 0f)
            {
                canvaGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

        }
    }


}

