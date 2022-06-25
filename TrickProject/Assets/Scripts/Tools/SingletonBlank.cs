using UnityEngine;

public class SingletonBlank<T> where T:SingletonBlank<T>,new()
{
    public static T Instance
    {
        get { return Inner.inner; }
    }

    private class Inner
    {
        internal static T inner = new T();
    }
}
