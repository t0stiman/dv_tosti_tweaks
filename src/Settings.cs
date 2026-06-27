using UnityEngine;
using UnityModManagerNet;

namespace tosti_tweaks;

public class Settings: UnityModManager.ModSettings
{
	public bool LowerTrackIDSigns = true;
	
	public bool EnableDebugLog = false;
	
	public void Draw(UnityModManager.ModEntry _)
	{
		LowerTrackIDSigns = GUILayout.Toggle(LowerTrackIDSigns, "Lower track ID signs");
			
		EnableDebugLog = GUILayout.Toggle(EnableDebugLog, "EnableDebugLog");
	}

	public override void Save(UnityModManager.ModEntry modEntry)
	{
		Save(this, modEntry);
	}
}