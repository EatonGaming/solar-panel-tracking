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
        public static class About
        {
            private static readonly int _majorVersion = 0;
            private static readonly int _minorVersion = 0;
            private static readonly int _revision = 1;

            public static string ScriptName = "Solar Panel Tracking";
            public static string ScriptAuthor = "EatonGaming";
            public static string ScriptVersion = $"{_majorVersion}.{_minorVersion}.{_revision}";
        }
    }
}
