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
    partial class Program : MyGridProgram
    {
        private readonly Debug _debug;
        private readonly DebugLCD _out;
        private readonly BlockCache _blockCache;

        public Program()
        {
            _out = new DebugLCD(GridTerminalSystem);
            _debug = new Debug(_out);
            _blockCache = new BlockCache(GridTerminalSystem, _out);
        }

        public void Save()
        {
            
        }

        public void Main(string argument, UpdateType updateSource)
        {
            SolarPanelTower tower1 = new SolarPanelTower(_out, _blockCache);
        }
    }
}
