using UnityEngine.Events;

public class ReturnEvent<T> : UnityEvent<T> { }

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }