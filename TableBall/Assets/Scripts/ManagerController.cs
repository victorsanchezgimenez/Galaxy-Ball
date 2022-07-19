using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ManagerController : MonoBehaviour
{
    [Header("Animators Main Menu")]
    [SerializeField] Animator animationTitle;
    [SerializeField] Animator animationPlayButton;
    [SerializeField] Animator animationOptionButton;
    [SerializeField] Animator animationTutorialButton;

    [Header("Animations WorldMenu")]
    [SerializeField] Animator animationWorldTitle;
    [SerializeField] Animator animationWorld1Button;
    [SerializeField] Animator animationWorld2Button;
    [SerializeField] Animator animationWorld3Button;
    [SerializeField] Animator animationWorld4Button;
    [SerializeField] Animator animationWorld5Button;
    [SerializeField] Animator animationWorld6Button;
    [SerializeField] Animator animationWorld7Button;
    [SerializeField] Animator animationWorld8Button;
    [SerializeField] Animator animationWorld9Button;
    [SerializeField] Animator animationWorld10Button;
    [SerializeField] Animator animationWorldBackButton;

    [Header("Animation Tutorial")]
    [SerializeField] Animator animationImageTutorial;
    [SerializeField] Animator animationTextTutorial;
    [SerializeField] Animator animationBackTutorialButton;

    [Header("Sound Settings")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioButtonSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] Button soundButton;
    [SerializeField] TMP_Text soundButtonText;

    [Header("Fade Screen")]
    [SerializeField] Animator fade;

    

    private void Awake() 
    {
        AudioCheck();
    }

    private void Start() 
    {
        AudioButtonUpdate();
    }

    private void Update() 
    {
        AudioCheck();    
    }

    void AudioCheck()
    {
        if(audioSource == null)
        {
            audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        }

        if(audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
    }

    void AudioButtonUpdate()
    {
        SoundButtonLook();
    }

    public void onClickPlayButton()
    {
        //Sound Button
        ButtonSoundEffect();
        

        //Out of scene
        animationTitle.SetBool("Out", true);
        animationPlayButton.SetBool("Out", true);
        animationOptionButton.SetBool("Out", true);
        animationTutorialButton.SetBool("Out", true);

        //In to scene
        animationWorldTitle.SetBool("In", true);
        animationWorld1Button.SetBool("In", true);
        animationWorld2Button.SetBool("In", true);
        animationWorld3Button.SetBool("In", true);
        animationWorld4Button.SetBool("In", true);
        animationWorld5Button.SetBool("In", true);
        animationWorld6Button.SetBool("In", true);
        animationWorld7Button.SetBool("In", true);
        animationWorld8Button.SetBool("In", true);
        animationWorld9Button.SetBool("In", true);
        animationWorld10Button.SetBool("In", true);
        animationWorldBackButton.SetBool("In", true);

    }

    public void onClickTutorialButton()
    {
        //Sound Button
        ButtonSoundEffect();
        
        //Out of scene
        animationTitle.SetBool("Out", true);
        animationPlayButton.SetBool("Out", true);
        animationOptionButton.SetBool("Out", true);
        animationTutorialButton.SetBool("Out", true);

        //In to scene
        animationImageTutorial.SetBool("In", true);
        animationTextTutorial.SetBool("In", true);
        animationBackTutorialButton.SetBool("In", true);

    }

    public void onClickTutorialBackButton()
    {
        //Sound Button
        ButtonSoundEffect();

        //In to scene
        animationTitle.SetBool("Out", false);
        animationPlayButton.SetBool("Out", false);
        animationOptionButton.SetBool("Out", false);
        animationTutorialButton.SetBool("Out", false);

        //Out of scene
        animationImageTutorial.SetBool("In", false);
        animationTextTutorial.SetBool("In", false);
        animationBackTutorialButton.SetBool("In", false);
    }


    public void OnClickSoundButton()
    {
        //Sound Button

        if (!audioManager.muteControl)
        {
            audioManager.muteControl = true;
        }
        else
        {
            audioManager.muteControl = false;
        }

        SoundButtonLook();

        ButtonSoundEffect();
    }

    private void ButtonSoundEffect()
    {
        if (!audioManager.muteControl)
        {
            audioButtonSource.PlayOneShot(audioClip, 0.8f);
        }
    }

    private void SoundButtonLook()
    {
        if (!audioManager.muteControl)
        {
            audioSource.mute = false;
            soundButton.image.color = new Color32(21, 255, 0, 255);
            soundButtonText.text = "SOUND ON";
            
        }
        else
        {
            audioSource.mute = true;
            soundButton.image.color = new Color32(255, 11, 0, 255);
            soundButtonText.text = "SOUND OFF";
        }
    }

    public void OnClickWorldBackButton()
    {
        ButtonSoundEffect();

        //In to scene
        animationTitle.SetBool("Out", false);
        animationPlayButton.SetBool("Out", false);
        animationOptionButton.SetBool("Out", false);
        animationTutorialButton.SetBool("Out", false);

        //Out of scene
        animationWorldTitle.SetBool("In", false);
        animationWorld1Button.SetBool("In", false);
        animationWorld2Button.SetBool("In", false);
        animationWorld3Button.SetBool("In", false);
        animationWorld4Button.SetBool("In", false);
        animationWorld5Button.SetBool("In", false);
        animationWorld6Button.SetBool("In", false);
        animationWorld7Button.SetBool("In", false);
        animationWorld8Button.SetBool("In", false);
        animationWorld9Button.SetBool("In", false);
        animationWorld10Button.SetBool("In", false);
        animationWorldBackButton.SetBool("In", false);

    }

    public void onClickWorldLoad(int levelIndex)
    {
        ButtonSoundEffect();
        fade.SetBool("In", true);
        StartCoroutine(WorldOne(levelIndex));
    }

    IEnumerator WorldOne(int level)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }

    public void onClickResetLevelsDebug()
    {
        PlayerPrefs.SetInt("levelsUnlocked", 1);
    }
}
