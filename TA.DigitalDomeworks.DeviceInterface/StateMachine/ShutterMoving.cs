// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachine.cs  Last modified: 2018-03-17@01:03 by Tim Long

using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
{
    internal class ShutterMoving : IControllerState
    {
    public void OnEnter()
        {
        throw new System.NotImplementedException();
        }

    public void OnExit()
        {
        throw new System.NotImplementedException();
        }

    public void EncoderTickReceived(int encoderPosition)
        {
        throw new System.NotImplementedException();
        }

    public void ShutterCurrentReadingReceived(int motorCurrent)
        {
        throw new System.NotImplementedException();
        }

    public void StatusUpdateReceived(IHardwareStatus status)
        {
        throw new System.NotImplementedException();
        }

    public string Name { get; }
    }
}