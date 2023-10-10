using UnityEngine;

public class CrossfadeController : MonoBehaviour
{
    #region Variables

        #region Singleton

            public static CrossfadeController instance = null;

        #endregion

        #region Components

            private Animator animator;

        #endregion

    #endregion

    #region Built-in Methods

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            animator = GetComponent<Animator>();
        }

    #endregion

    #region Custom Methods

        public void FadeIn(float crossfadeLength)
        {
            animator.speed = 1 / crossfadeLength;
            animator.Play("Fade-In");
        }

        public void FadeOut(float crossfadeLength)
        {
            animator.speed = 1 / crossfadeLength;
            animator.Play("Fade-Out");
        }

        private void CrossfadeComplete()
        {
            SceneLoader.instance.crossfadeComplete = true;  
        }

    #endregion
}