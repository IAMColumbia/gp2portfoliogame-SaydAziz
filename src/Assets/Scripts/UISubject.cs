using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubject : Subject
{

    private List<IUIObserver> uiObservers = new List<IUIObserver>();

    public void AddUIObserver(IUIObserver observer)
    {
        uiObservers.Add(observer);
    }
    public void RemoveUIObserver(IUIObserver observer)
    {
        uiObservers.Remove(observer);
    }
    protected void NotifyUIObservers(int id, int content)
    {
        uiObservers.ForEach(observer => observer.NotifyUI(id, content));
    }
    protected void NotifyUIObservers(Trinket trinket)
    {
        uiObservers.ForEach(observer => observer.NotifyUI(trinket));
    }
}
