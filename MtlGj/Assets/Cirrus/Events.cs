using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cirrus.Events
{
    public delegate void ArgumentsEvents<T>(T value, params object[] args);

    public delegate void Event();

    public delegate void Event<T>(T value);

    public delegate void Event<T, D>(T value1, D value2);

    public delegate void Event<T, D, S>(T value1, D value2, S value3);

    [System.Serializable]
    public class ObservableValue<T>
    {
        public Event<T> OnValueChangedHandler;
    
        [SerializeField]
        private T _previous;

        public T Previous => _previous;

        [SerializeField]
        private T _value;

        public ObservableValue()
        {

        }

        public ObservableValue(T value)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _previous = _value;
                    _value = value;
                    OnValueChangedHandler?.Invoke(value);
                }
            }
        }

        public void Set(T value, bool forceNotification = true)
        {
            _previous = _value;
            _value = value;
            if(forceNotification) OnValueChangedHandler?.Invoke(value);
        }

    }


    public class ObservableCollection<T>
    {
        private BindingList<T> _collection = new BindingList<T>();

        public Event<ObservableCollection<T>> OnValueChangedHandler;

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingList<T> Ts
        {
            get { return _collection; }
            set
            {
                if (value != _collection)
                {
                    Ts = value;
                    if (Ts != null)
                    {
                        _collection.ListChanged += (sender, args) => OnValueChangedHandler?.Invoke(this);
                    }
                }
            }
        }

        public ObservableCollection()
        {
            _collection.ListChanged += (sender, args) => OnValueChangedHandler?.Invoke(this);
        }
    }

    [System.Serializable]
    public class ObservableInt : ObservableValue<int> { }

    [System.Serializable]
    public class ObservableBool : ObservableValue<bool> { }

    [System.Serializable]
    public class ObservableString : ObservableValue<string> { }
}