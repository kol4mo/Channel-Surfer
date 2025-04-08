using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TvButton : MonoBehaviour
{
    [SerializeField] Material StaticMaterial;
    [SerializeField] Material ButtonMaterial;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] int materialIndex;
    [SerializeField] UnityEvent<Loader.scenes> OnClick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        meshRenderer.SetMaterials(new List<Material> { meshRenderer.material, StaticMaterial });
    }

    // Update is called once per frame
    void Update()
    {
       
    }

	private void OnMouseOver() {
		meshRenderer.SetMaterials(new List<Material> { meshRenderer.material, ButtonMaterial });
	}
	private void OnMouseExit() {
		meshRenderer.SetMaterials(new List<Material> { meshRenderer.material, StaticMaterial });
	}
	private void OnMouseDown() {
        OnClick.Invoke(Loader.scenes.MainMenu);

	}

	//Mouse Hover
	//Mouse Click
}
