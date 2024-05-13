using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int playTime;
    private float lightUpdateTimer = 30;
    private const float LIGHT_UPDATE_PERIOD = 30;
    [SerializeField] private List<Light> lights;
    [SerializeField] private AnimationCurve lightIntensityCurve;

    private void Awake() {
        playTime = TimeSaveManager.Load();
    }

    void Update()
    {
        lightUpdateTimer += Time.deltaTime;

        if(lightUpdateTimer > LIGHT_UPDATE_PERIOD) {
            playTime += (int)LIGHT_UPDATE_PERIOD;
            int minute = playTime / 60; //in minutes
            lightUpdateTimer = 0;
            float timePeriod = (float)minute % 60; // 1 day equal 60 minutes
            timePeriod /= 60;

            foreach (var light in lights) {
                light.intensity = lightIntensityCurve.Evaluate(timePeriod);
                
            }
        }
    }
}
