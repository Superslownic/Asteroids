using System;

namespace Code
{
  public class Observable<T>
  {
    private T _value;
    
    public T Value
    {
      get => _value;
      
      set
      {
        _value = value;
        OnChanged?.Invoke(_value);
      }
    }

    public event Action<T> OnChanged;

    public Observable()
    {
    }

    public Observable(T value)
    {
      _value = value;
    }
  }
}