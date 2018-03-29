// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: IAscomDriver.cs  Last modified: 2018-03-29@02:55 by Tim Long

namespace TA.PostSharp.Aspects
    {
    public interface IAscomDriver
        {
        bool Connected { get; }
        }
    }