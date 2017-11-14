using System;

namespace WiLinq.LinqProvider
{
    [Flags]
    public enum NewWorkItemOptions
    {
        Nothing = 0,
        FillAreaPath = 0b1,
        FillIterationPath = 0b10,
        FillAreaAndIterationPath = FillAreaPath | FillIterationPath
    }
}