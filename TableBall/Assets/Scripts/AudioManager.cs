using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Clips Songs Ambience")]
    [SerializeField] AudioClip[] sceneMusicChangeArray;
    
    /*MORE CLIPS SONG*/
    [Header("Get AudioSource Component")]
    [SerializeField] AudioSource audioSource;
    public static AudioManager instance;

    public bool muteControl;

    private void Awake()
    {
        muteControl = false;
        
        AudioCheck();

        ObjectDontDestroyOnLoad();
    }

    
    void OnEnable() 
    {
        SceneManager.sceneLoaded += OnScreenLoaded;  
    }

    void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnScreenLoaded;
    }

    void OnScreenLoaded (Scene scene, LoadSceneMode mode) 
    {
        AudioClip thisSceneMusic = sceneMusicChangeArray[scene.buildIndex];
        if(thisSceneMusic)
        {
            audioSource.clip = thisSceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    //Mute Sound
    public void MuteMusic(bool isMute)
    {
        if(isMute != audioSource.mute)
        {
            audioSource.mute = isMute;
        }
    }


    //No delete Object
    void ObjectDontDestroyOnLoad()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);    
    }

    private void AudioCheck()
    {
        if (audioSource == null)
        {
            audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        }
    }
}
