using System.Collections.Generic;
using UnityEngine;

namespace Signals
{
    public class Signal {
        private readonly string name;
        public string Name {
            get {
                return name;
            }
        }

        private ConcreteSignalParameters parameters;

        public delegate void SignalListener(ISignalParameters parameters);
        private List<SignalListener> listenerList = new List<SignalListener>();

        public Signal(string name) {
            this.name = name;
            listenerList = new List<SignalListener>();
        }

        public void ClearParameters() {
            if (parameters == null) {
                parameters = new ConcreteSignalParameters();
            }

            parameters.PushParameters();
        }

        public void AddParameter(string key, object value) {
            // This will throw an error if ClearParameters() is not invoked prior to calling this method
            parameters.AddParameter(key, value);
        }

        public void AddListener(SignalListener listener) {
            listenerList.Add(listener);
        }

        public void RemoveListener(SignalListener listener) {
            listenerList.Remove(listener);
        }

        public void Dispatch() {
            try {
                if (listenerList.Count == 0) {
                    Debug.LogWarning("There are no listeners to the signal: " + name);
                }

                for (int i = 0; i < listenerList.Count; ++i) {
                    listenerList[i](parameters);
                }
            } finally {
                if (parameters != null && parameters.HasParameters) {
                    parameters.PopParameters();
                }
            }
        }
    }

}
