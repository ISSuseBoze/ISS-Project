using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimulationLog
{
    public static int kvant = 2;
    private static float startTime;
    private static List<float> keyLog;
    private static List<float> stressorLog;
    public static bool logStarted = false;
    public static void StartLog()
    {
        startTime = Time.time;
        keyLog = new List<float>();
        stressorLog = new List<float>();
        logStarted = true;
    }
    public static void LogKey(float timePressed)
    {
        if (logStarted)
        {
            Debug.Log("LOG KEY: " + (timePressed - startTime));
            keyLog.Add(timePressed);
        }

    }

    public static void LogStressor(float timePlayed)
    {
        if (logStarted)
        {
            stressorLog.Add(timePlayed);
        }

    }

    public static void StopLog()
    {
        logStarted = false;
    }
    public static string GetLogData()
    {
        var simulationTime = Time.time - startTime;
        var simulatorOutput = "END_TASK ";
        var stressorIndex = 0;
        var keyIndex = 0;
        for (var time = 0; time < simulationTime + 1; time++)
        {

            var keyCounter = 0;
            var stressorCounter = 0;



            var k = keyIndex;
            for (k = keyIndex; k < keyLog.Count; k++)
            {
                if (keyLog[k] - startTime >= time * kvant && keyLog[k] - startTime < (time + 1) * kvant)
                {
                    keyCounter++;
                }
                else
                {
                    break;
                }
            }
            keyIndex = k;

            var s = stressorIndex;
            for (s = stressorIndex; s < stressorLog.Count; s++)
            {
                if (stressorLog[s] - startTime >= time * kvant && stressorLog[s] - startTime < (time + 1) * kvant)
                {
                    stressorCounter++;
                }
                else
                {
                    break;
                }
            }
            stressorIndex = s;
            Debug.Log("key inex " + keyIndex);
            var simulatorRow = time + "," + keyCounter + "," + stressorCounter + ":";
            simulatorOutput += simulatorRow;
        }

        simulatorOutput += "TIME " + Mathf.CeilToInt(simulationTime);
        return simulatorOutput;
    }



}


