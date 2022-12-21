using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    //private PostProcessVolume _postProcessVolume;
    //private Vignette _Vignette;
    private PostProcessProfile m_Profile;

    private PostProcessVolume m_Volume;

    private Vignette _Vignette;

    // Start is called before the first frame update
    void Start()
    {
        //var volume = target.GetComponent<Volume>();
        _Vignette = ScriptableObject.CreateInstance<Vignette>();
        _Vignette.enabled.Override(true);
        _Vignette.intensity.overrideState = true;
        _Vignette.rounded.overrideState = true;

        _Vignette.intensity.value = 1;
        print("INIT VG VAL:" + _Vignette.intensity.value);

        _Vignette.rounded.value = true;

        m_Volume =
            PostProcessManager
                .instance
                .QuickVolume(gameObject.layer, 100f, _Vignette);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void WhenPickUp(int count)
    {
        if (count == 5)
        {
            _Vignette.rounded.value = false;
            _Vignette.intensity.value = 1;
        }
        else
        {
            if (!_Vignette.rounded.value)
            {
                //if ELLIPSE vignette
                _Vignette.intensity.value = 1 - (0.01f * count);
            }
            else
            {
                _Vignette.intensity.value = 1 - (0.05f * count);
            }
            //print("Vignette Intensity: "+ _Vignette.intensity.value );
        }
    }

    public float ModifyVignette(float num, bool rounded)
    {
        float vigNum = _Vignette.intensity.value;
        _Vignette.rounded.value = rounded;

        _Vignette.intensity.value = num;
        return vigNum;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
