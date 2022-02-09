using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Photon.Pun;
//using UnityEngine.Rendering.PostProcessing;

public class PostProcessingControl : MonoBehaviour
{
    //postprocessing volume
    private Volume postprocessing;
    private AudioSource audio;
    public AudioClip[] audioclips;
    private bool filmgrainbool,filmgrainend,entershining,sfx = false;
    WhiteBalance whitebalance;
    ColorCurves colorcurves;
    FilmGrain filmgrain;
    ChromaticAberration chromatic;
    
    // Start is called before the first frame update
    void Start()
    {
        postprocessing = this.gameObject.GetComponent<Volume>();
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //玩家1
        if(outsidetheschoolPUN.ServerConnect.PlayerID == 1)
        {
            //引爆蒸飯室
            if(PuzzleObjController.SteamBoxSwitch == true)
            {
                PostProcessingP1();
            }   
        }
        //玩家2
        else if(outsidetheschoolPUN.ServerConnect.PlayerID == 2)
        {
            //進入二樓範圍內
            if(Trigger.Enter2ndFloor == true)
            {
                PostProcessingP2();
            }
        }
    }

    //P1濾鏡
    void PostProcessingP1()
    {
        if(postprocessing.profile.TryGet(out filmgrain) && postprocessing.profile.TryGet(out colorcurves) && postprocessing.profile.TryGet(out chromatic))
        {
            if(filmgrain.intensity.value < 1f && filmgrainbool == false && filmgrainend == false && colorcurves.active == false && GameData.GameData.Save2 == false)
            {
                if(sfx == false)
                {
                    sfx = true;
                    audio.clip = audioclips[0];
                    audio.Play();
                }
                
                StartCoroutine(P1FilmGraintimer());
                filmgrainbool = true;
            }
            else if(HP.currentHP !=0 && filmgrain.intensity.value == 1f && filmgrainend == true && colorcurves.active == false || (GameData.GameData.Save2 == true && colorcurves.active == false))
            {
                audio.clip = audioclips[1];
                audio.volume = 0.3f;
                audio.loop = true;
                audio.Play();
                filmgrain.intensity.value = 0.01f;
                colorcurves.active = true;
            }
        }
    }

    //P2濾鏡
    void PostProcessingP2()
    {
        if(postprocessing.profile.TryGet(out filmgrain))
        {
            if(filmgrain.intensity.value < 1f && filmgrainbool == false && filmgrainend == false && GameData.GameData.Save2 == false)
            {
                StartCoroutine(P2FilmGraintimer());
                filmgrainbool = true;
            }
            else if(HP.currentHP !=0 && filmgrain.intensity.value == 1f && filmgrainend == true || (GameData.GameData.Save2 == true))
            {
                filmgrain.intensity.value = 0.01f;
            }
        }
    }

    //P1雜訊
    IEnumerator P1FilmGraintimer()
    {
        yield return new WaitForSeconds(0.01f);
        
        chromatic.intensity.value += 0.006f;
        filmgrain.intensity.value += 0.006f;

        if(filmgrain.intensity.value == 1f)
        {
            filmgrainend = true;
        }

        filmgrainbool = false;
    }

    //P2雜訊
    IEnumerator P2FilmGraintimer()
    {
        yield return new WaitForSeconds(0.1f);
        filmgrain.intensity.value += 0.075f;
        
        if(filmgrain.intensity.value == 1f)
        {
            filmgrainend = true;
        }
        
        filmgrainbool = false;
    }
}