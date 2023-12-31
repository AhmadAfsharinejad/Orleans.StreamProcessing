﻿using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.HttpListener;

internal sealed class HttpListenerGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.HttpListener);
    public Type GrainInterface => typeof(IHttpListenerGrain);
}