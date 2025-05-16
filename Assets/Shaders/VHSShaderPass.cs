using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VHSGlitchRenderFeature : ScriptableRendererFeature {
	[System.Serializable]
	public class VHSSettings {
		public Material glitchMaterial;
		[Range(0f, 1f)]
		public float intensity = 1f;
	}

	public VHSSettings settings = new VHSSettings();
	private VHSGlitchRenderPass glitchPass;

	public override void Create() {
		glitchPass = new VHSGlitchRenderPass(settings);
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
		if (settings.glitchMaterial == null) {
			Debug.LogWarning("VHS Glitch Effect: Material is not assigned!");
			return;
		}

		// Just enqueue the pass
		renderer.EnqueuePass(glitchPass);
	}

	class VHSGlitchRenderPass : ScriptableRenderPass {
		private VHSSettings settings;
		private RTHandle tempRT;
		private Material blitMaterial;
		private string profilerTag = "VHS Glitch Pass";

		// IDs for shader properties
		private int mainTexId = Shader.PropertyToID("_MainTex");
		private int timeSpeedId = Shader.PropertyToID("_TimeSpeed");
		private int scanlineIntensityId = Shader.PropertyToID("_ScanlineIntensity");
		private int noiseStrengthId = Shader.PropertyToID("_NoiseStrength");

		public VHSGlitchRenderPass(VHSSettings settings) {
			this.settings = settings;
			renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

			// Create a copy of the material to avoid modifying the original
			if (settings.glitchMaterial != null) {
				blitMaterial = new Material(settings.glitchMaterial);
			}
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) {
			// This is called before Configure
			// No need to configure target here
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor) {
			// Make sure our temporary RTHandle is created and configured properly
			var desc = cameraTextureDescriptor;
			desc.depthBufferBits = 0; // We don't need depth for this effect

			if (tempRT == null) {
				tempRT = RTHandles.Alloc(desc, name: "_TempVHSEffect");
				Debug.Log("RTHandle allocated");
			}
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
			if (blitMaterial == null) {
				Debug.LogError("VHS Glitch Effect: Material is not valid!");
				return;
			}

			CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

			// Get the source texture (camera target)
			var source = renderingData.cameraData.renderer.cameraColorTargetHandle;

			// Update shader properties
			blitMaterial.SetFloat(timeSpeedId, Time.time);

			// Using the direct CommandBuffer.Blit instead of Blitter.BlitCameraTexture
			// This avoids some RTHandle-related issues
			cmd.SetGlobalTexture(mainTexId, source);
			cmd.Blit(source, tempRT.nameID, blitMaterial, 0);
			cmd.Blit(tempRT.nameID, source);

			// Execute the command buffer and release it
			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd) {
			// No cleanup needed during camera rendering
		}

		public void Cleanup() {
			// Release resources
			if (tempRT != null) {
				tempRT.Release();
				tempRT = null;
			}

			if (blitMaterial != null) {
				Object.DestroyImmediate(blitMaterial);
				blitMaterial = null;
			}
		}
	}

	protected override void Dispose(bool disposing) {
		// Clean up resources when the feature is removed
		if (glitchPass != null) {
			glitchPass.Cleanup();
		}
		base.Dispose(disposing);
	}
}