using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    //ͨ��Audio Source ����������

    private void Start()
    {
      
    }

    public void PlayBgm(string path)
    {
        AudioClip ac = Resources.Load<AudioClip>(path);
        audioSource .clip = ac;
        audioSource.Play();
    }  
     public void StopBgm()
       {
            if (audioSource.isPlaying)   //������ڲ��ŵĻ�,ֹͣ
            {
                audioSource.Stop();
            }
       }

    
    public void PlayClip(string path,float volume = 1)
    {
        AudioClip ac = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(ac,transform.position,volume);
    }
}
