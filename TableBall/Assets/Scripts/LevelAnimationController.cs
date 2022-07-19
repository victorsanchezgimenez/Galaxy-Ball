using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelAnimationController : MonoBehaviour
{
    [Header("Animation Fade Initialize")]
    [SerializeField] Animator fadeAnimation;

    [Header("Text Current Level Canvas")]
    [SerializeField] TMP_Text textLevel;

    [Header("Animation Start CountDown And TextController")]
    [SerializeField] Animator countDownAnimation;
    [SerializeField] TMP_Text textCountDown;

    [Header("PlayerControlls")]
    [SerializeField] TableMovement playerController;

    [Header("Timer text")]
    [SerializeField] TMP_Text textTimer;
    public float timerLeft;
    public bool timerOn;

    [Header("GameOver Animation")]
    [SerializeField] Animator gameOverAnimation;
    [SerializeField] Animator buttonRestartAnimation;
    [SerializeField] Animator buttonLeaveAnimation;
    [SerializeField] Image gameOverAndMenuFade;

    [Header("Pause Animation")]
    [SerializeField] Button menuButton;
    [SerializeField] Animator pauseAnimation;
    [SerializeField] Animator buttonContinueAnimation;

    [Header("Completed Animation")]
    [SerializeField] Animator completedAnimation;
    [SerializeField] Animator nextButtonAnimation;

    [Header("Success/Destruction Effects")]
    [SerializeField] ParticleSystem successEffect;
    [SerializeField] ParticleSystem destructionEffect;
    private bool winEffectControler = false;

    [Header("AudioEffect")]
    [SerializeField] AudioSource audioManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;



    private int currentLevel;
     

    void Start()
    {
        
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        gameOverAndMenuFade.enabled = false;

        CurrentLevelText();
        FadeAndCountDownAnimation();
    }

    private void CheckAudioManager()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        CheckAudioManager();
        TimerController();
    }

    private void TimerController()
    {
        if (timerOn)
        {
            textTimer.enabled = true;
            textTimer.text = "Timer: " + (int)timerLeft;
            if (timerLeft > 0)
            {
                timerLeft -= Time.deltaTime;
            }
            else
            {
                timerLeft = 0;
                timerOn = false;
                playerController.enabled = false;
                gameOverAndMenuFade.enabled = true;
                gameOverAnimation.SetBool("Run", true);
                buttonRestartAnimation.SetBool("In", true);
                buttonLeaveAnimation.SetBool("In", true);
                menuButton.interactable = false;
                destructionEffect.Play();
                //Lose sound effect
                AudioEffectPlay(1);

            }

        }
    }

    private void FadeAndCountDownAnimation()
    {
        StartCoroutine(CountDownAndFade());
    }

    IEnumerator CountDownAndFade()
    {
        gameOverAndMenuFade.enabled = true;
        textTimer.enabled = false;
        playerController.enabled = false;
        textCountDown.text = "";
        fadeAnimation.SetBool("Out", true);
        yield return new WaitForSeconds(0.5f);
        textCountDown.text = "3";
        countDownAnimation.SetBool("Run", true);
        yield return new WaitForSeconds(1f);
        textCountDown.text = "2";
        yield return new WaitForSeconds(1f);
        textCountDown.text = "1";
        yield return new WaitForSeconds(1f);
        textCountDown.text = "GO";
        playerController.enabled = true;
        yield return new WaitForSeconds(0.5f);
        //Start countdown timer
        timerOn = true;
        gameOverAndMenuFade.enabled = false;
        menuButton.interactable = true;

    }

    private void CurrentLevelText()
    {
        textLevel.text = "LEVEL " + currentLevel;
    }

    public void OnClickMenuButton()
    {
        //Button sound effect
        AudioEffectPlay(2);
        menuButton.interactable = false;
        playerController.enabled = false;
        timerOn = false;
        gameOverAndMenuFade.enabled = true;
        pauseAnimation.SetBool("In", true);
        buttonRestartAnimation.SetBool("In", true);
        buttonLeaveAnimation.SetBool("In", true);
        buttonContinueAnimation.SetBool("In", true);

    }
    
    public void OnClickContinueButton()
    {
        //Button sound effect
        AudioEffectPlay(2);
        StartCoroutine(BackToGame());
    }

    IEnumerator BackToGame()
    {
        pauseAnimation.SetBool("In", false);
        buttonRestartAnimation.SetBool("In", false);
        buttonLeaveAnimation.SetBool("In", false);
        buttonContinueAnimation.SetBool("In", false);
        yield return new WaitForSeconds(1f);
        menuButton.interactable = true;
        playerController.enabled = true;
        timerOn = true;
        gameOverAndMenuFade.enabled = false;
    }

    public void OnClickRestartButton()
    {
        //Button sound effect
        AudioEffectPlay(2);
        StartCoroutine(ExitLevel(currentLevel));

    }

    public void OnClickExitButton(int level)
    {
        //Button sound effect
        AudioEffectPlay(2);
        StartCoroutine(ExitLevel(level));
    }

    IEnumerator ExitLevel(int level)
    {
        fadeAnimation.SetBool("Out", false);
        fadeAnimation.SetBool("In", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    
    }


    public void startAnimationWin()
    {
        //Win sound and effect
        if(!winEffectControler)
        {
            successEffect.Play();
            AudioEffectPlay(0);
            winEffectControler = true;
        }
        menuButton.interactable = false;
        playerController.enabled = false;
        timerOn = false;
        gameOverAndMenuFade.enabled = true;
        completedAnimation.SetBool("In", true);
        nextButtonAnimation.SetBool("In", true);
        buttonLeaveAnimation.SetBool("In", true);
        Pass();

    }

    
    public void Pass()
    {
        if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            Debug.Log(PlayerPrefs.GetInt("levelsUnlocked"));
        }
    }

    public void onClickNextButton()
    {
        AudioEffectPlay(2);
        if(currentLevel != 10)
        {
            StartCoroutine(ExitLevel(currentLevel + 1));

        }
        else
        {
            StartCoroutine(ExitLevel(0));

        }
    }

    private void AudioEffectPlay(int audioClip)
    {
        if (!audioManager.mute)
        {
            audioSource.PlayOneShot(audioClips[audioClip], 0.8f);
        }
    }
}
