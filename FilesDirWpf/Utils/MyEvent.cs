namespace FilesDirWpf.Utils;

public static class MyEvent
{
    #region ParamChanged

    public delegate void ParamChanged();
    public static event ParamChanged? OnParamChanged;
    public static void InvokeParamChanged() => OnParamChanged?.Invoke();
    
    #endregion
}