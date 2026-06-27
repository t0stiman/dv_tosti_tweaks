using System;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;

namespace tosti_tweaks;

[EnableReloading]
internal static class Main
{
	private static UnityModManager.ModEntry myModEntry;
	private static Harmony harmony;
	public static Settings MySettings { get; private set; }

	//================================================================

	private static bool Load(UnityModManager.ModEntry modEntry)
	{
		try
		{
			myModEntry = modEntry;
			modEntry.OnUnload = OnUnload;
			
			MySettings = UnityModManager.ModSettings.Load<Settings>(modEntry);
			modEntry.OnGUI = entry => MySettings.Draw(entry);
			modEntry.OnSaveGUI = entry => MySettings.Save(entry);

			harmony = new Harmony(modEntry.Info.Id); 
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
		catch (Exception ex)
		{
			modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(modEntry.Info.Id);
			return false;
		}
		
		Log("loaded");

		return true;
	}

	private static bool OnUnload(UnityModManager.ModEntry modEntry)
	{
		harmony?.UnpatchAll(modEntry.Info.Id);
		return true;
	}

	// Logger Commands
	public static void Log(string message)
	{
		myModEntry.Logger.Log(message);
	}

	public static void Warning(string message)
	{
		myModEntry.Logger.Warning(message);
	}

	public static void Error(string message)
	{
		myModEntry.Logger.Error(message);
	}
	
	public static void Debug(object message)
	{
		if(!MySettings.EnableDebugLog) return;
		myModEntry.Logger.Log($"[DEBUG] {message}");
	}
}