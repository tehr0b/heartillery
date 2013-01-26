#define DEBUG

using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

/*
 *  A static container for debug methods
 *  such as asserts, console logs, etc.
 */
public static class ArDebug {
	
	// Remove definition of "DEBUG" to strip these from the build
	
	[Conditional("DEBUG")]
    public static void Assert(bool condition)
    {
        if (!condition)
		{
			throw new Exception();
		}
    }
	
	[Conditional("DEBUG")]
    public static void Assert(bool condition, string msg)
    {
        if (!condition) 
		{
			throw new Exception(msg);
		}
    }
	
	/*
	 *  Call our own log functions instead of
	 *  calling Unity's directly, so that we 
	 *  can pass the messages through to
	 *  any logging or visualization we write.
	 */
	
	[Conditional("DEBUG")]
    public static void Log(string msg)
    {
        UnityEngine.Debug.Log(System.DateTime.Now + ": " + msg);
    }
	
	[Conditional("DEBUG")]
    public static void LogError(string msg)
    {
        UnityEngine.Debug.LogError(System.DateTime.Now + ": " + msg);
    }
	
	[Conditional("DEBUG")]
    public static void LogWarning(string msg)
    {
        UnityEngine.Debug.LogWarning(System.DateTime.Now + ": " + msg);
    }
}
