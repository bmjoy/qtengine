using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNetworking : BaseBehaviour {

    public enum componentState { STOPPED, RUNNING };
    [HideInInspector]
    public componentState state = componentState.STOPPED;

}
