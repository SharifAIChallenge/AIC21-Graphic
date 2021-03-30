﻿using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
class BuildSettings
{
    static BuildSettings()
    {
//WebGL
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm;
        PlayerSettings.WebGL.threadsSupport = false;
        PlayerSettings.WebGL.memorySize = 999;
    }
}