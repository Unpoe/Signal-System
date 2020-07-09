using System.Collections.Generic;

namespace Signals
{
    public class ConcreteSignalParameters : ISignalParameters
    {
        private Stack<Dictionary<string, object>> parameterStack = new Stack<Dictionary<string, object>>();

        public ConcreteSignalParameters() {
        }

        public void AddParameter(string key, object value) {
            parameterStack.Peek()[key] = value;
        }

        public object GetParameter(string key) {
            return parameterStack.Peek()[key];
        }

        public bool HasParameter(string key) {
            return parameterStack.Peek().ContainsKey(key);
        }

        public void PushParameters() {
            parameterStack.Push(NewParameterMap());
        }

        public void PopParameters() {
            POOL.Recycle(parameterStack.Peek());
            parameterStack.Pop();
        }

        public bool HasParameters {
            get {
                return parameterStack.Count > 0;
            }
        }

        private static readonly Pool<Dictionary<string, object>> POOL = new Pool<Dictionary<string, object>>(() => new Dictionary<string, object>());

        private static Dictionary<string, object> NewParameterMap() {
            Dictionary<string, object> newInstance = POOL.Request();
            newInstance.Clear();
            return newInstance;
        }
    }
}
