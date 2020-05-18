using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class Debug
        {
            private readonly DebugLCD debugLCD;

            public Debug(DebugLCD debugLCD)
            {
                this.debugLCD = debugLCD;
            }

            public void HandleException(Exception exception)
            {
                debugLCD.Log(exception);

                throw exception; //This is rethrown to prevent the script from resuming in space engineers.
            }
        }
    }
}
