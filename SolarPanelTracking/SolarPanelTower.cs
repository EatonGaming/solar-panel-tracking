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
        public class SolarPanelTower
        {
            private const float DefaultTorque = 5.0f;
            private const float DefaultVelocity = 5.0f;

            private readonly DebugLCD _out;
            private readonly BlockCache _blockCache;

            private IMyMotorStator _rotorZ;
            private IMyMotorStator _rotorY;

            public SolarPanelTower(DebugLCD debugLCD, BlockCache blockCache)
            {
                _out = debugLCD;
                _blockCache = blockCache;

                Initialise();
                ResetPositions();
            }

            private void ResetPositions()
            {
                _out.Log("Resetting motor positions...");

                MoveRotorToPosition(_rotorZ, 0);
                MoveRotorToPosition(_rotorY, 0);
            }

            private void MoveRotorToPosition(IMyMotorStator rotor, float targetAngle)
            {
                _out.Log($"Moving {rotor.CustomName} to position {targetAngle}...");

                float initialAngle = rotor.Angle;
                float angleToRotate = targetAngle - initialAngle;

                if (angleToRotate == 0)
                {
                    return;
                }

                rotor.LowerLimitDeg = targetAngle;
                rotor.UpperLimitDeg = targetAngle;

                float velocity = (angleToRotate > 0) ? DefaultVelocity : -DefaultVelocity;
                rotor.TargetVelocityRPM = velocity;

                _out.Log($"{rotor.CustomName} movement completed.");
            }

            private void Initialise()
            {
                _rotorZ = _blockCache.GetBlockWithName<IMyMotorStator>("Rotor Z");
                _rotorY = _blockCache.GetBlockWithName<IMyMotorStator>("Rotor Y");

                _out.Log("Solar Panel 1 Initialised.");
            }
        }
    }
}
