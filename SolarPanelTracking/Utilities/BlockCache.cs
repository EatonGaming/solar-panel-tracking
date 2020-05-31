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
using System.Runtime.Caching;

namespace IngameScript
{
    partial class Program
    {
        public class BlockCache
        {
            private readonly IMyGridTerminalSystem _myGridTerminalSystem;
            private readonly DebugLCD _out;
            private readonly Dictionary<string, IMyTerminalBlock> _cache = new Dictionary<string, IMyTerminalBlock>();

            public BlockCache(IMyGridTerminalSystem myGridTerminalSystem, DebugLCD debugLCD)
            {
                _myGridTerminalSystem = myGridTerminalSystem;
                _out = debugLCD;
            }

            public T GetBlockWithName<T>(string name)
            {
                IMyTerminalBlock blockToReturn = GetBlockFromCache(name);

                return (T) blockToReturn;
            }

            private IMyTerminalBlock GetBlockFromCache(string name)
            {
                IMyTerminalBlock blockToReturn;
                bool found = _cache.TryGetValue(name, out blockToReturn);

                if (!found)
                {
                    _out.Log($"Block '{name}' does not exist in cache, adding...");
                    blockToReturn = FindBlockByName(name);

                    if (blockToReturn != null)
                    {
                        _cache.Add(name, blockToReturn);
                    }
                }
                else
                {
                    _out.Log($"{name} was found in the cache.");
                }

                return blockToReturn;
            }

            private IMyTerminalBlock FindBlockByName(string name)
            {
                var block = _myGridTerminalSystem.GetBlockWithName(name);

                if (block != null)
                {
                    return block;
                }
                else
                {
                    _out.Log($"Unable to find block '{name}'.");
                    return null;
                }
            }
        }
    }
}
