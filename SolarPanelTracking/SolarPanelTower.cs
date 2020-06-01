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
using System.Text.RegularExpressions;

namespace IngameScript
{
    partial class Program
    {
        public class SolarPanelTower
        {
            private const float DefaultTorque = 1f;
            private const float DefaultVelocity = 1f;
            private const float NoTargetAngle = -1f;
            private const string CurrentAngleRegexPattern = @".*?: (<angle>\d+)°";

            private readonly DebugLCD _out;
            private readonly BlockCache _blockCache;

            private IMyMotorStator _rotorZ;
            private IMyMotorStator _rotorY;

            private SolarPanelState _state = SolarPanelState.Idle;
            private float _rotorZTargetAngle = NoTargetAngle;
            private float _rotorYTargetAngle = NoTargetAngle;

            public SolarPanelTower(DebugLCD debugLCD, BlockCache blockCache)
            {
                _out = debugLCD;
                _blockCache = blockCache;

                Initialise();
                ResetPositions();
            }

            public void CheckForUpdates()
            {
                switch (_state)
                {
                    case SolarPanelState.Idle:
                        break;
                    case SolarPanelState.Moving:
                        CheckMovementProgress();
                        break;
                }
            }

            private void CheckMovementProgress()
            {
                _out.Log("Checking progress...");

                bool rotorZComplete = GetCurrentAngle(_rotorZ) != _rotorZTargetAngle;
                if (rotorZComplete)
                {
                    _rotorZTargetAngle = NoTargetAngle;
                    _out.Log("Rotor Z movement completed.");
                }

                bool rotorYComplete = GetCurrentAngle(_rotorY) != _rotorYTargetAngle;
                if (rotorYComplete)
                {
                    _rotorYTargetAngle = NoTargetAngle;
                    _out.Log("Rotor Y movement completed.");
                }

                if (rotorZComplete && rotorYComplete)
                {
                    _state = SolarPanelState.Idle;
                    _out.Log("All movements complete.");
                }
            }

            private float GetCurrentAngle(IMyMotorStator rotorZ)
            {
                IMyPistonBase myPistonBase = (IMyPistonBase)rotorZ;
                string detailedInfo = myPistonBase.DetailedInfo;
                _out.Log($"Detailed Info: {detailedInfo}");

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(CurrentAngleRegexPattern);
                System.Text.RegularExpressions.Match match = regex.Match(detailedInfo);

                if (match.Success)
                {
                    string extractedAngle = match.Groups["angle"].Value;
                    _out.Log($"Angle {extractedAngle} found!");
                    return float.Parse(extractedAngle);
                }
                else
                {
                    _out.Log("Unable to find angle.");
                    return 0f;
                }
            }

            private void ResetPositions()
            {
                _out.Log("Resetting motor positions...");

                _rotorZTargetAngle = MoveRotorToPosition(_rotorZ, 0);
                _rotorYTargetAngle = MoveRotorToPosition(_rotorY, 0);
            }

            private float MoveRotorToPosition(IMyMotorStator rotor, float targetAngle)
            {
                _out.Log($"Moving {rotor.CustomName} to position {targetAngle}...");
                _state = SolarPanelState.Moving;

                float initialAngle = rotor.Angle;
                float angleToRotate = targetAngle - initialAngle;

                if (angleToRotate == 0)
                {
                    return NoTargetAngle;
                }

                rotor.LowerLimitDeg = targetAngle;
                rotor.UpperLimitDeg = targetAngle;

                float velocity = (angleToRotate > 0) ? DefaultVelocity : -DefaultVelocity;
                rotor.TargetVelocityRPM = velocity;

                return targetAngle;
            }

            private void Initialise()
            {
                _rotorZ = _blockCache.GetBlockWithName<IMyMotorStator>("Rotor Z");
                _rotorY = _blockCache.GetBlockWithName<IMyMotorStator>("Rotor Y");

                _out.Log("Solar Panel 1 Initialised.");
            }
        }

        public enum SolarPanelState
        {
            Idle,
            Moving
        }
    }
}
