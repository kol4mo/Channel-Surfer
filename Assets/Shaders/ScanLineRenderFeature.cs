using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScanlineRenderFeature : ScriptableRendererFeature {
	[System.Serializable]
	public class ScanlineSettings {
		public Material scanlineMaterial;
		[Range(0f, 1f)]
		public float effectStrength = 0.5f;
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
	}

	public ScanlineSettings settings = new ScanlineSettings();
	private ScanlineRenderPass scanlinePass;

	// Shader property IDs for efficiency
	private static readonly int StrengthID = Shader.PropertyToID("_Strength");

	public override void Create() {
		scanlinePass = new ScanlineRenderPass("Scanline Effect");
		scanlinePass.renderPassEvent = settings.renderPassEvent;
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
		if (settings.scanlineMaterial == null) {
			Debug.LogWarning("Scanline material is not assigned in the Render Feature!");
			return;
		}

		// Configure the pass with current settings
		scanlinePass.Setup(settings.scanlineMaterial, settings.effectStrength);

		// Add our pass to the renderer
		renderer.EnqueuePass(scanlinePass);
	}

	// Cleanup any allocated resources
	protected override void Dispose(bool disposing) {
		scanlinePass?.Dispose();
	}

	class ScanlineRenderPass : ScriptableRenderPass {
		private Material scanlineMaterial;
		private float effectStrength;
		private string profilerTag;
		private RTHandle tempRenderTarget;

		// Shader property IDs for efficiency
		private static readonly int MainTexID = Shader.PropertyToID("_MainTex");
		private static readonly int StrengthID = Shader.PropertyToID("_Strength");
		private static readonly int TempTargetID = Shader.PropertyToID("_TempScanlineTarget");

		public ScanlineRenderPass(string tag) {
			profilerTag = tag;
		}

		public void Setup(Material material, float strength) {
			this.scanlineMaterial = material;
			this.effectStrength = strength;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) {
			// Get the camera target descriptor
			RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
			descriptor.depthBufferBits = 0; // We don't need depth for post-processing

			// Allocate the temporary render texture using RTHandle
			RenderingUtils.ReAllocateIfNeeded(ref tempRenderTarget, descriptor,
				name: "_TempScanlineTarget", filterMode: FilterMode.Bilinear);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
			if (scanlineMaterial == null) {
				Debug.LogError("Scanline material is null in Render Pass Execute!");
				return;
			}

			CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

			// Get camera color target
			RTHandle cameraColorTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;

			// Update material properties
			scanlineMaterial.SetFloat(StrengthID, effectStrength);

			// Blit from camera target to temp texture
			Blitter.BlitCameraTexture(cmd, cameraColorTarget, tempRenderTarget);

			// Blit from temp texture back to camera target with effect material
			Blitter.BlitCameraTexture(cmd, tempRenderTarget, cameraColorTarget, scanlineMaterial, 0);

			// Execute the command buffer
			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd) {
			// Nothing to clean up as RTHandle management is handled by the RenderingUtils
		}

		public void Dispose() {
			// Release allocated RTHandle when the feature is disposed
			tempRenderTarget?.Release();
		}
	}
}