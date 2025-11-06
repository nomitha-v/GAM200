using UnityEngine;
using System;
public enum SoundType //sfx
{
    DAYSTART,
    DAYCOMPLETE,
    DAYINCOMPLETE
}


[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;

    static SoundManager instance; //able to call from ANYWHERE

    public AudioSource audioSource;


    private void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //for sfx
    }
    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(SoundType sound, float volume = 1)//soundType(enum class), volume  
    {
        //for audio
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }




#if UNITY_EDITOR //only applies if youre in the unity editor | to prevent errors?
    private void OnEnable()
    {
        //setting names for soundList from the struc list
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }

    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [SerializeField] public string name;
    [SerializeField] private AudioClip[] sounds;

}
