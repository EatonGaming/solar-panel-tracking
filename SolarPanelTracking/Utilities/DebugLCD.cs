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
        public class DebugLCD
        {
            private const string DefaultName = "Debug LCD";
            private const string NewlineCharacter = "\n";

            private readonly IMyGridTerminalSystem _myGridTerminalSystem;
            private readonly string _name;

            private IMyTextPanel _block;

            public DebugLCD(IMyGridTerminalSystem myGridTerminalSystem)
            {
                _name = DefaultName;
                _myGridTerminalSystem = myGridTerminalSystem;

                InitialiseDebugDisplay();
                DisplayInitialText();
            }

            private void DisplayInitialText()
            {
                if (_block == null)
                    return;

                _block.WriteText($"{About.ScriptName}\nDeveloped By: {About.ScriptAuthor}\nVersion: {About.ScriptVersion}");
                _block.WriteText($"\n-----------------------------------------------------", append: true);
                _block.WriteText("\n", append: true);
            }

            public void Log(string message, bool append = true)
            {
                var display = InitialiseDebugDisplay();
                var text = $"{message}{NewlineCharacter}";

                display?.WriteText(text, append);
            }

            public void Log(Exception exception)
            {
                Log("An exception occured during script execution.");

                Log(exception.ToString());
            }

            private IMyTextPanel InitialiseDebugDisplay()
            {
                if (_block != null)
                {
                    return _block;
                }
                else
                {
                    _block = _myGridTerminalSystem.GetBlockWithName(_name) as IMyTextPanel;
                }

                return _block;
            }
        }
    }
}
