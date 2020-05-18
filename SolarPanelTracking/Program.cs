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

        public Program()
        {
            _out = new DebugLCD(GridTerminalSystem);
            _debug = new Debug(_out);
        }

        public void Save()
        {
            
        }

        public void Main(string argument, UpdateType updateSource)
        {
            var rotorZ = GridTerminalSystem.GetBlockWithName("Rotor Z");
            var rotorY = GridTerminalSystem.GetBlockWithName("Rotor Y");

            if (rotorZ != null)
            {
                _out.Log("Found Rotor Z!");
            }
            else
            {
                _out.Log("Unable to find rotor z.");
            }

            if (rotorY != null)
            {
                _out.Log("Found Rotor Y!");
            }
            else
            {
                _out.Log("Unable to find rotor y.");
            }
        }
    }
}
