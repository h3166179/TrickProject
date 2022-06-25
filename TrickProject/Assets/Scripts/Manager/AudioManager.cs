using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public enum AudioType { MASTER,BGM,SHORT,USUAL};
    private string path = "Audio/Music/";
    [SerializeField]private AudioMixer masterMixer;//Audio混合器
    private AudioSource bgmSource;//背景音乐源
    private Dictionary<string,AudioSource> usualSourcesDic=new Dictionary<string, AudioSource>();

    private void Start()
    {
        masterMixer = Resources.Load<AudioMixer>("Audio/AudioMixer/Mixer");
        if (masterMixer == null)
            Debug.Log("AudioMixer Loding Failed");

        Init();
    }

    private void Init()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        PropertyAudio(bgmSource, AudioType.BGM,true);
        
    }

    //音频属性初始化函数
    private void PropertyAudio(AudioSource audioSource,AudioType audioType,bool isLoop)
    {
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = GetAudioMixerGroup(audioType);
        audioSource.loop = isLoop;
    }

    /// <summary>
    /// 音量设置(主音量、BGM、非常用音效、常用音效)
    /// </summary>
    /// <param name="volume"></param>
    public void SetMasterVolume(float volume,AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.MASTER:
                masterMixer.SetFloat("Master", volume);
                break;
            case AudioType.BGM:
                masterMixer.SetFloat("Bgm", volume);
                break;
            case AudioType.SHORT:
                masterMixer.SetFloat("ShortSounds", volume);
                break;
            case AudioType.USUAL:
                masterMixer.SetFloat("UsualSounds", volume);
                break;
        }
    }

    /// <summary>
    /// 获取混合器
    /// </summary>
    /// <param name="audioType"></param>
    /// <returns></returns>
    private AudioMixerGroup GetAudioMixerGroup(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.MASTER:
                return masterMixer.FindMatchingGroups("Master")[0];
                break;
            case AudioType.BGM:
                return masterMixer.FindMatchingGroups("Bgm")[0];
                break;
            case AudioType.SHORT:
                return masterMixer.FindMatchingGroups("ShortSounds")[0];
                break;
            case AudioType.USUAL:
                return masterMixer.FindMatchingGroups("UsualSounds")[0];
                break;
        }
        Debug.Log("Not Find AudioType"); 
        return null;
    }

    /// <summary>
    /// 设置BGM AudioClip 并播放
    /// </summary>
    /// <param name="name"></param>
    public void SetBgmAudioClip(string name)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path+ name);
        bgmSource.clip = audioClip;
        bgmSource.Play();
    }

    /// <summary>
    /// 非常用音频播放(播放完毕立即销毁，无需保存)
    /// </summary>
    /// <param name="name"></param>
    public void ShortSoundsPlay(string name)
    {
        AudioSource audioSource= gameObject.AddComponent<AudioSource>();
        PropertyAudio(audioSource, AudioType.SHORT, false);
        audioSource.clip = Resources.Load<AudioClip>(path+name);
        StartCoroutine(AudioCallBack(audioSource));
        audioSource.Play();
        
    }


    /// <summary>
    /// 常用音频播放(并将创建的音频存储在常用音频表里，下次调用无需重创)
    /// 相同音频不会叠加播放 后续有需求可拓展
    /// </summary>
    /// <param name="name"></param>
    public void UsualSoundsPlay(string name)
    {
        if(usualSourcesDic.ContainsKey(name))
        {
            usualSourcesDic[name].Play();
        }
        else
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            PropertyAudio(audioSource, AudioType.USUAL, false);
            audioSource.clip = Resources.Load<AudioClip>(path + name);
            usualSourcesDic.Add(name, audioSource);
            audioSource.Play();
        }
    }

    /// <summary>
    /// 音频播放完毕，回调销毁
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator AudioCallBack(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource);
    }

}
