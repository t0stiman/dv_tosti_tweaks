using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace tosti_tweaks.Patches;

[HarmonyPatch(typeof(StationController))]
[HarmonyPatch(nameof(StationController.GenerateTrackIdObject))]
public class StationController_GenerateTrackIdObject_Patch
{
	private static void Postfix(List<RailTrack> stationRailTracks)
	{
		if(!Main.MySettings.LowerTrackIDSigns) { return; }
		
		foreach (var track in stationRailTracks)
		{
			for (var i = 0; i < track.transform.childCount; i++)
			{
				var child = track.transform.GetChild(i);
				if(!child.name.StartsWith(RailTrack.TRACK_ID_GO_NAME)) continue;
				
				child.position -= Vector3.up * 1.6f;
			}
		}
	}
}