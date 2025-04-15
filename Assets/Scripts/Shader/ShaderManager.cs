using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public enum ShaderSettings {
        Default,
        Outline
    }

    [SerializeField] ShaderSettings shader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.SetQualityLevel((int)shader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
