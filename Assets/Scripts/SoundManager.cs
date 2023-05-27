using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region SINGLETON
    
    private static SoundManager _instance = null;
    
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
                
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(SoundManager).ToString();
                    _instance = newObjectInstance.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    #endregion
    
    #region VARIABLE
    
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();
    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();
    
    #endregion
    
    #region ACCESSEUR
    
    public List<AudioSource> AudioSources
    {
        get => _audioSources;
        set => _audioSources = value;
    }
    
    public List<AudioClip> AudioClips
    {
        get => _audioClips;
        set => _audioClips = value;
    }
    
    #endregion
    
    #region FUNCTION
    
    public void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        _audioSources.Add(audioSource);
        _audioClips.Add(audioClip);
    }
    
    #endregion

}
