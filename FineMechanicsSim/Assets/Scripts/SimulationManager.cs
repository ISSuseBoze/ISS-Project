using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public NoseTaskScript noseTaskscript;
    private TaskModel curentTask;
    public GameState gameState = GameState.Stopepd;
    public StressorManager stressorManager;
    private float simulationStartedOn;
    public SocketScript communicator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetMessage(string msg)
    {

        var indexTask = msg.LastIndexOf("NEW_TASK");
        var indexStart = msg.LastIndexOf("START");
        var indexHalt = msg.LastIndexOf("HALT");


        if (indexTask >= 0 && gameState == GameState.Stopepd)
        {
            var index = msg.IndexOf('{');
            var cmd = msg.Substring(0, index).Trim();
            curentTask = JsonUtility.FromJson<TaskModel>(msg.Substring(index).Trim());
        }

        if (indexStart >= 0 && curentTask != null && gameState == GameState.Stopepd)
        {
            simulationStartedOn = Time.time;
            SimulationLog.StartLog();
            noseTaskscript.startTask(curentTask.Transformations);
            stressorManager.StartStressors(curentTask.StressorOccurrence);
            gameState = GameState.Started;
        }

        if (indexHalt >= 0)
        {
            noseTaskscript.stopTask();
            stressorManager.StopStressors();
            gameState = GameState.Stopepd;
        }

    }

    public void EndSimulation()
    {
        if (Time.time - simulationStartedOn > 3 && gameState == GameState.Started)
        {
            SimulationLog.StopLog();
            gameState = GameState.Stopepd;
            noseTaskscript.stopTask();
            stressorManager.StopStressors();
            SendLogData();
        }

    }

    private void SendLogData()
    {
        var result = SimulationLog.GetLogData();
        communicator.SendToServer(result);

    }
}
public enum GameState
{
    Started,
    Stopepd
}
