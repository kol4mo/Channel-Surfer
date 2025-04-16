using UnityEngine;
using UnityEngine.Animations;

public class playerGraphic1 : MonoBehaviour
{
    [SerializeField] Transform graphicTf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lookDirection(float horizontalInput) {
        if (horizontalInput < 0) {
            graphicTf.rotation = Quaternion.Euler(0, 180, 0);
        } else if (horizontalInput > 0) {
			graphicTf.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
