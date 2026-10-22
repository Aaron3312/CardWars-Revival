using UnityEngine;

// Stand-ins for the legacy particle API Unity removed (ParticleEmitter, ParticleAnimator, ParticleRenderer).
// No scene or prefab contains these components any more, so GetComponent lookups always return null
// and the code paths that use them are inert.
public class ParticleEmitter : Behaviour
{
	public bool emit;

	public bool useWorldSpace;

	public int particleCount;

	public float minEmission;

	public float maxEmission;

	public Particle[] particles;

	public void Emit(int count)
	{
	}
}

public class ParticleAnimator : Behaviour
{
	public bool autodestruct;
}

public class ParticleRenderer : Renderer
{
}

public struct Particle
{
	public Vector3 position;

	public float energy;

	public float size;
}
