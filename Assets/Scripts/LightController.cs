using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float eachFadeTime = 86400;

    private Light lightSource;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();

        StartCoroutine(fadeInAndOutRepeat(lightSource, eachFadeTime));
    }

    //Fade in and out forever
    IEnumerator fadeInAndOutRepeat(Light lightToFade, float duration)
    {
        while (true)
        {
            yield return fadeInAndOut(lightToFade, false, duration);

            yield return fadeInAndOut(lightToFade, true, duration);
        }
    }

    IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(fadeIn ? 0.1f : 0.5f);

        float minLuminosity = 0; // min intensity
        float maxLuminosity = 1; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return waitForXSec;
        }
    }
}
