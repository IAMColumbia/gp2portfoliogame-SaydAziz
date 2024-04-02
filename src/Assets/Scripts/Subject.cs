using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    protected void NotifyObservers(int id)
    {
        observers.ForEach(observer => observer.OnNotify(id));
    }
}
