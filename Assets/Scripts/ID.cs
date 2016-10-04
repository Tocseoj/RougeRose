using UnityEngine;
using System.Collections;

public static class Helpers {
	public static bool NullCheck<T>(ref T obj) { if (obj == null) {Debug.LogError("NULL FOUND."); return true;} return false; }
}
