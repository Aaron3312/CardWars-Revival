using UnityEngine;

// Keeps edge-anchored UI out of notches, camera cut-outs and rounded corners.
public static class SafeAreaUtil
{
	// Intersects a screen-space pixel rect with the platform's safe area. On devices without cut-outs
	// (and in the editor) the safe area is the whole screen, so the rect comes back unchanged.
	public static Rect Inset(Rect pixelRect)
	{
		Rect safeArea = Screen.safeArea;
		float xMin = Mathf.Max(pixelRect.xMin, safeArea.xMin);
		float yMin = Mathf.Max(pixelRect.yMin, safeArea.yMin);
		float xMax = Mathf.Min(pixelRect.xMax, safeArea.xMax);
		float yMax = Mathf.Min(pixelRect.yMax, safeArea.yMax);
		if (xMax <= xMin || yMax <= yMin)
		{
			return pixelRect;
		}
		return Rect.MinMaxRect(xMin, yMin, xMax, yMax);
	}
}
