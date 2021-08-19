using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHandler : MonoBehaviour
{
    public Animator anime;
    InputHandler inputHandler;
    ThirdPersonController thirdPerson;

    public void Initialize()
    {
        anime = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        thirdPerson = GetComponentInParent<ThirdPersonController>();
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        #region Vertical
        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
        #endregion

        #region Horizontal
        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion

        anime.SetFloat("Vertical", v, 0.1f, Time.deltaTime);
        anime.SetFloat("Horizontal", h, 0.1f, Time.deltaTime);
    }

    public void PlayAnimation(string animationName, float timerToGoBack)
    {
        thirdPerson.isMoving = true;
        anime.SetBool(animationName, true);
        StartCoroutine(AnimationTimer(animationName, timerToGoBack));
    }

    IEnumerator AnimationTimer(string a,float t)
    {
        yield return new WaitForSeconds(t);
        anime.SetBool(a, false);
        thirdPerson.isMoving = false;
    }

    public void PlayDeathAnimation()
    {
        thirdPerson.isMoving = true;
        anime.SetBool("Death", true);
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }

}
