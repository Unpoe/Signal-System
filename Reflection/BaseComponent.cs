using System;
using System.Reflection;
using UnityEngine;
using Signals;

public abstract class BaseComponent : MonoBehaviour
{
    protected virtual void OnEnable() {
        RegisterSignalHandlers(true);
    }

    protected virtual void OnDisable() {
        RegisterSignalHandlers(false);
    }

    private void RegisterSignalHandlers(bool mode) {
        MemberInfo[] members = GetType().GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach(MemberInfo m in members) {
            if(!(m is MethodInfo)) {
                continue;
            }

            Attribute[] attribs = Attribute.GetCustomAttributes(m, false);
            foreach (Attribute a in attribs) {
                if (a is SignalHandlerAttribute) {
                    SignalHandlerAttribute signalAttribute = a as SignalHandlerAttribute;
                    Signal signal = GameSignals.GetSignalByName(signalAttribute.SignalName);
                    Signal.SignalListener listener = (
                        x => {
                            MethodInfo methodInfo = m as MethodInfo;
                            methodInfo.Invoke(this, new object[] { x });
                        }
                    );

                    if (mode) {
                        signal.AddListener(listener);
                    } else {
                        signal.RemoveListener(listener);
                    }
                }
            }
        }
    }
}
