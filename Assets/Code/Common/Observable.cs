using System;

namespace Code.Common
{
  public class Observable<T> : IReadOnlyObservable<T>
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

  public interface IReadOnlyObservable<out T>
  {
    public event Action<T> OnChanged;
    public T Value { get; }
  }
}