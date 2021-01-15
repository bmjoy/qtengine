using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncAnimation : BaseQTObjectComponent {

    /* Server */
    public Animator animator;
    public bool syncParameters;

    public Dictionary<string, object> lastParameters;

    public override void handleObjectSpawn() {
        handleClientObjectSpawn();
        handleServerObjectSpawn();
    }

    public override void handleServerObjectSpawn() {
        if (obj.objectType != BaseQTObject.type.SERVER) { return; }

        lastParameters = new Dictionary<string, object>();
        InvokeRepeating("sync", 0f, ServerSettings.instance.syncRate);
    }

    public void sync() {
        if (syncParameters) {
            foreach (AnimatorControllerParameter parameter in animator.parameters) {
                object latestValue = QTUtils.getParameterValueFromAnimator(animator, parameter);

                if (lastParameters.ContainsKey(parameter.name) == false) {
                    lastParameters.Add(parameter.name, latestValue);

                    onParameterChanged(parameter.name, lastParameters[parameter.name]);
                } else if (lastParameters[parameter.name].Equals(latestValue) == false) {
                    lastParameters[parameter.name] = latestValue;

                    onParameterChanged(parameter.name, lastParameters[parameter.name]);
                }
            }
        }
    }

    public void syncAllParameters() {
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            object latestValue = QTUtils.getParameterValueFromAnimator(animator, parameter);

            onParameterChanged(parameter.name, latestValue);
        }
    }

    public void onParameterChanged(string parameterName, object parameterValue) {
        if (parameterValue == null) {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "Setting to null -> " + parameterName);
            return;
        }

        AnimationParameterInfoMessage message = QTUtils.getSyncAnimationMessage(parameterName, parameterValue);
        message.objectID = obj.objectID;
        message.index = index;
        WorkerServerManager.instance.sendMessageToAllReady(message);

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Sending sync of value(" + parameterName + "=" + parameterValue + ")");
    }

}
