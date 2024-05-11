using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Game Event", menuName = "ScriptableObjects/Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> Listeners = new List<GameEventListener>();
    
    public void Raise()
    {
        for (int i = this.Listeners.Count - 1; i >= 0; i--)
        {
            this.Listeners[i].OnEventRaised();
        }
    }
    
    public void RegisterListener(GameEventListener listener)
    {
        this.Listeners.Add(listener);
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        this.Listeners.Remove(listener);
    }
}
