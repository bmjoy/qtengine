//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Action_Single p_default_Trigger;
        
        private static SteamVR_Action_Boolean p_default_PadNorthTouch;
        
        private static SteamVR_Action_Boolean p_default_PadEastTouch;
        
        private static SteamVR_Action_Boolean p_default_PadSouthTouch;
        
        private static SteamVR_Action_Boolean p_default_PadWestTouch;
        
        private static SteamVR_Action_Boolean p_default_PadNorthClick;
        
        private static SteamVR_Action_Boolean p_default_PadEastClick;
        
        private static SteamVR_Action_Boolean p_default_PadSouthClick;
        
        private static SteamVR_Action_Boolean p_default_PadWestClick;
        
        private static SteamVR_Action_Boolean p_default_PadCenterTouch;
        
        private static SteamVR_Action_Boolean p_default_PadCenterClick;
        
        private static SteamVR_Action_Vector2 p_default_PadPosition;
        
        private static SteamVR_Action_Boolean p_default_Grip;
        
        private static SteamVR_Action_Boolean p_default_MenuClick;
        
        private static SteamVR_Action_Boolean p_default_JoystickClick;
        
        private static SteamVR_Action_Vector2 p_default_JoystickPosition;
        
        private static SteamVR_Action_Pose p_default_Pose;
        
        private static SteamVR_Action_Vibration p_default_Haptic;
        
        public static SteamVR_Action_Single default_Trigger
        {
            get
            {
                return SteamVR_Actions.p_default_Trigger.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadNorthTouch
        {
            get
            {
                return SteamVR_Actions.p_default_PadNorthTouch.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadEastTouch
        {
            get
            {
                return SteamVR_Actions.p_default_PadEastTouch.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadSouthTouch
        {
            get
            {
                return SteamVR_Actions.p_default_PadSouthTouch.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadWestTouch
        {
            get
            {
                return SteamVR_Actions.p_default_PadWestTouch.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadNorthClick
        {
            get
            {
                return SteamVR_Actions.p_default_PadNorthClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadEastClick
        {
            get
            {
                return SteamVR_Actions.p_default_PadEastClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadSouthClick
        {
            get
            {
                return SteamVR_Actions.p_default_PadSouthClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadWestClick
        {
            get
            {
                return SteamVR_Actions.p_default_PadWestClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadCenterTouch
        {
            get
            {
                return SteamVR_Actions.p_default_PadCenterTouch.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_PadCenterClick
        {
            get
            {
                return SteamVR_Actions.p_default_PadCenterClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vector2 default_PadPosition
        {
            get
            {
                return SteamVR_Actions.p_default_PadPosition.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Grip
        {
            get
            {
                return SteamVR_Actions.p_default_Grip.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_MenuClick
        {
            get
            {
                return SteamVR_Actions.p_default_MenuClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_JoystickClick
        {
            get
            {
                return SteamVR_Actions.p_default_JoystickClick.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vector2 default_JoystickPosition
        {
            get
            {
                return SteamVR_Actions.p_default_JoystickPosition.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Pose default_Pose
        {
            get
            {
                return SteamVR_Actions.p_default_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Vibration default_Haptic
        {
            get
            {
                return SteamVR_Actions.p_default_Haptic.GetCopy<SteamVR_Action_Vibration>();
            }
        }
        
        private static void InitializeActionArrays()
        {
            Valve.VR.SteamVR_Input.actions = new Valve.VR.SteamVR_Action[] {
                    SteamVR_Actions.default_Trigger,
                    SteamVR_Actions.default_PadNorthTouch,
                    SteamVR_Actions.default_PadEastTouch,
                    SteamVR_Actions.default_PadSouthTouch,
                    SteamVR_Actions.default_PadWestTouch,
                    SteamVR_Actions.default_PadNorthClick,
                    SteamVR_Actions.default_PadEastClick,
                    SteamVR_Actions.default_PadSouthClick,
                    SteamVR_Actions.default_PadWestClick,
                    SteamVR_Actions.default_PadCenterTouch,
                    SteamVR_Actions.default_PadCenterClick,
                    SteamVR_Actions.default_PadPosition,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_MenuClick,
                    SteamVR_Actions.default_JoystickClick,
                    SteamVR_Actions.default_JoystickPosition,
                    SteamVR_Actions.default_Pose,
                    SteamVR_Actions.default_Haptic};
            Valve.VR.SteamVR_Input.actionsIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_Trigger,
                    SteamVR_Actions.default_PadNorthTouch,
                    SteamVR_Actions.default_PadEastTouch,
                    SteamVR_Actions.default_PadSouthTouch,
                    SteamVR_Actions.default_PadWestTouch,
                    SteamVR_Actions.default_PadNorthClick,
                    SteamVR_Actions.default_PadEastClick,
                    SteamVR_Actions.default_PadSouthClick,
                    SteamVR_Actions.default_PadWestClick,
                    SteamVR_Actions.default_PadCenterTouch,
                    SteamVR_Actions.default_PadCenterClick,
                    SteamVR_Actions.default_PadPosition,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_MenuClick,
                    SteamVR_Actions.default_JoystickClick,
                    SteamVR_Actions.default_JoystickPosition,
                    SteamVR_Actions.default_Pose};
            Valve.VR.SteamVR_Input.actionsOut = new Valve.VR.ISteamVR_Action_Out[] {
                    SteamVR_Actions.default_Haptic};
            Valve.VR.SteamVR_Input.actionsVibration = new Valve.VR.SteamVR_Action_Vibration[] {
                    SteamVR_Actions.default_Haptic};
            Valve.VR.SteamVR_Input.actionsPose = new Valve.VR.SteamVR_Action_Pose[] {
                    SteamVR_Actions.default_Pose};
            Valve.VR.SteamVR_Input.actionsBoolean = new Valve.VR.SteamVR_Action_Boolean[] {
                    SteamVR_Actions.default_PadNorthTouch,
                    SteamVR_Actions.default_PadEastTouch,
                    SteamVR_Actions.default_PadSouthTouch,
                    SteamVR_Actions.default_PadWestTouch,
                    SteamVR_Actions.default_PadNorthClick,
                    SteamVR_Actions.default_PadEastClick,
                    SteamVR_Actions.default_PadSouthClick,
                    SteamVR_Actions.default_PadWestClick,
                    SteamVR_Actions.default_PadCenterTouch,
                    SteamVR_Actions.default_PadCenterClick,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_MenuClick,
                    SteamVR_Actions.default_JoystickClick};
            Valve.VR.SteamVR_Input.actionsSingle = new Valve.VR.SteamVR_Action_Single[] {
                    SteamVR_Actions.default_Trigger};
            Valve.VR.SteamVR_Input.actionsVector2 = new Valve.VR.SteamVR_Action_Vector2[] {
                    SteamVR_Actions.default_PadPosition,
                    SteamVR_Actions.default_JoystickPosition};
            Valve.VR.SteamVR_Input.actionsVector3 = new Valve.VR.SteamVR_Action_Vector3[0];
            Valve.VR.SteamVR_Input.actionsSkeleton = new Valve.VR.SteamVR_Action_Skeleton[0];
            Valve.VR.SteamVR_Input.actionsNonPoseNonSkeletonIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_Trigger,
                    SteamVR_Actions.default_PadNorthTouch,
                    SteamVR_Actions.default_PadEastTouch,
                    SteamVR_Actions.default_PadSouthTouch,
                    SteamVR_Actions.default_PadWestTouch,
                    SteamVR_Actions.default_PadNorthClick,
                    SteamVR_Actions.default_PadEastClick,
                    SteamVR_Actions.default_PadSouthClick,
                    SteamVR_Actions.default_PadWestClick,
                    SteamVR_Actions.default_PadCenterTouch,
                    SteamVR_Actions.default_PadCenterClick,
                    SteamVR_Actions.default_PadPosition,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_MenuClick,
                    SteamVR_Actions.default_JoystickClick,
                    SteamVR_Actions.default_JoystickPosition};
        }
        
        private static void PreInitActions()
        {
            SteamVR_Actions.p_default_Trigger = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/default/in/Trigger")));
            SteamVR_Actions.p_default_PadNorthTouch = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadNorthTouch")));
            SteamVR_Actions.p_default_PadEastTouch = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadEastTouch")));
            SteamVR_Actions.p_default_PadSouthTouch = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadSouthTouch")));
            SteamVR_Actions.p_default_PadWestTouch = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadWestTouch")));
            SteamVR_Actions.p_default_PadNorthClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadNorthClick")));
            SteamVR_Actions.p_default_PadEastClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadEastClick")));
            SteamVR_Actions.p_default_PadSouthClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadSouthClick")));
            SteamVR_Actions.p_default_PadWestClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadWestClick")));
            SteamVR_Actions.p_default_PadCenterTouch = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadCenterTouch")));
            SteamVR_Actions.p_default_PadCenterClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/PadCenterClick")));
            SteamVR_Actions.p_default_PadPosition = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/default/in/PadPosition")));
            SteamVR_Actions.p_default_Grip = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Grip")));
            SteamVR_Actions.p_default_MenuClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/MenuClick")));
            SteamVR_Actions.p_default_JoystickClick = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/JoystickClick")));
            SteamVR_Actions.p_default_JoystickPosition = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/default/in/JoystickPosition")));
            SteamVR_Actions.p_default_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/default/in/Pose")));
            SteamVR_Actions.p_default_Haptic = ((SteamVR_Action_Vibration)(SteamVR_Action.Create<SteamVR_Action_Vibration>("/actions/default/out/Haptic")));
        }
    }
}
