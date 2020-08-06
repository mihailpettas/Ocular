using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour {

    public Image rawImage = null;
    public List<Texture> textures = new List<Texture>();
    public GameObject globe = null;

    public int texture_index_1 = 0;
    public int texture_index_2 = 0;
    public int texture_index_3 = 0;

    Renderer rend;
    void Start() {
        rend = globe.GetComponent<Renderer>();
		
		style.fontSize = 15;
		style.fontStyle = FontStyle.Bold;
    }
	
	public float hSliderValue = 0.53F;
	
	
	public float hSliderValue1x = 0.53F;
	public float hSliderValue2x = 0.53F;
	public float hSliderValue3x = 0.53F;
	public float hSliderValue1y = 0.53F;
	public float hSliderValue2y = 0.53F;
	public float hSliderValue3y = 0.53F;

    Color mainColor;
    Color HLColor;

    float red = 1F, green = 1F, blue =  1F, alpha = 1F;
    float redHL = 1F, greenHL = 1F, blueHL = 1F, alphaHL = 0.88F;

    void OnGUI() {

        GUI.Label(new Rect(25, 5, Screen.width - 200, 20), "Value slider", style);

        hSliderValue = GUI.HorizontalSlider(new Rect(25, 25, Screen.width - 200, 30), hSliderValue, 0.0F, 1.0F);


        if (GUI.Button(new Rect(25, 50, 100, 100), textures[texture_index_1]))
        {
            if (Input.GetMouseButtonUp(0))
            {
                texture_index_1 = (texture_index_1 + 1) >= textures.Count ? 0 : texture_index_1 + 1;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                texture_index_1 = (texture_index_1 - 1) >= 0 ? texture_index_1 - 1 : textures.Count -1;
            }
        }
            

        hSliderValue1x = GUI.HorizontalSlider(new Rect(25, 176, 100, 20), hSliderValue1x, -0.10F, 0.10F);
		GUI.Label(new Rect(25, 151, 100, 100), "X speed",style);
		
		hSliderValue1y = GUI.HorizontalSlider(new Rect(25, 221, 100, 20), hSliderValue1y, -0.10F, 0.10F);
		GUI.Label(new Rect(25, 196, 100, 100), "Y speed",style);
		
        if (GUI.Button(new Rect(150, 50, 100, 100),  textures[texture_index_2]))
        {
            if (Input.GetMouseButtonUp(0))
            {
                texture_index_2 = (texture_index_2 + 1) >= textures.Count ? 0 : texture_index_2 + 1;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                texture_index_2 = (texture_index_2 - 1) >= 0 ? texture_index_2 - 1 : textures.Count - 1;
            }
        }

        hSliderValue2x = GUI.HorizontalSlider(new Rect(150, 176, 100, 20), hSliderValue2x,  -0.10F, 0.10F);
		GUI.Label(new Rect(150, 151, 100, 100), "X speed",style);
		
		hSliderValue2y = GUI.HorizontalSlider(new Rect(150, 221, 100, 20), hSliderValue2y,  -0.10F, 0.10F);
		GUI.Label(new Rect(150, 196, 100, 100), "Y speed",style);
		
		if (GUI.Button(new Rect(275, 50, 100, 100),  textures[texture_index_3]))
        {
            if (Input.GetMouseButtonUp(0))
            {
                texture_index_3 = (texture_index_3 + 1) >= textures.Count ? 0 : texture_index_3 + 1;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                texture_index_3 = (texture_index_3 - 1) >= 0 ? texture_index_3 - 1 : textures.Count - 1;
            }
        }

        hSliderValue3x = GUI.HorizontalSlider(new Rect(275, 176, 100, 20), hSliderValue3x,  -0.10F, 0.10F);
		GUI.Label(new Rect(275, 151, 100, 100), "X speed",style);
		
		hSliderValue3y = GUI.HorizontalSlider(new Rect(275, 221, 100, 20), hSliderValue3y,  -0.10F, 0.10F);
		GUI.Label(new Rect(275, 196, 100, 100), "Y speed",style);
		
		
		red = GUI.HorizontalSlider(new Rect(25, 301, 100, 20), red, 0.0F, 1F);
		GUI.Label(new Rect(25, 286, 100, 100), "Main Red",style);

        green = GUI.HorizontalSlider(new Rect(25, 341, 100, 20), green, 0.0F, 1F);
        GUI.Label(new Rect(25, 326, 100, 100), "Main Green", style);

        blue = GUI.HorizontalSlider(new Rect(25, 381, 100, 20), blue, 0.0F, 1F);
        GUI.Label(new Rect(25, 366, 100, 100), "Main Blue", style);

        alpha = GUI.HorizontalSlider(new Rect(25, 421, 100, 20), alpha, 0.0F, 1F);
        GUI.Label(new Rect(25, 406, 100, 100), "Main Alpha", style);

        redHL = GUI.HorizontalSlider(new Rect(150, 301, 100, 20), redHL, 0.0F, 1F);
        GUI.Label(new Rect(150, 286, 100, 100), "HotLine Red", style);

        greenHL = GUI.HorizontalSlider(new Rect(150, 341, 100, 20), greenHL, 0.0F, 1F);
        GUI.Label(new Rect(150, 326, 100, 100), "HotLine Green", style);

        blueHL = GUI.HorizontalSlider(new Rect(150, 381, 100, 20), blueHL, 0.0F, 1F);
        GUI.Label(new Rect(150, 366, 100, 100), "HotLineBlue", style);

        alphaHL = GUI.HorizontalSlider(new Rect(150, 421, 100, 20), alphaHL, 0.0F, 1F);
        GUI.Label(new Rect(150, 406, 100, 100), "HotLine Alpha", style);

        mainColor = new Color(red, green, blue, alpha);
        HLColor = new Color(redHL, greenHL, blueHL, alphaHL);

        DrawQuad(new Rect(25, 250, 100, 20), mainColor);
        DrawQuad(new Rect(150, 250, 100, 20), HLColor);

        UpdateMaterial(rawImage.material);
        UpdateMaterial(rend.material);
    }

    void UpdateMaterial(Material material)
    {
        material.SetTexture("_MainTex", textures[texture_index_1]);
        material.SetTexture("_MainTex2", textures[texture_index_2]);
        material.SetTexture("_MainTex3", textures[texture_index_3]);

        //this is where progress is set
        material.SetFloat("_Progress", hSliderValue);

        material.SetFloat("_LAYER_1_SCROLL_Y", hSliderValue1y);
        material.SetFloat("_LAYER_1_SCROLL_X", hSliderValue1x);
        material.SetFloat("_LAYER_2_SCROLL_Y", hSliderValue2y);
        material.SetFloat("_LAYER_2_SCROLL_X", hSliderValue2x);
        material.SetFloat("_LAYER_3_SCROLL_Y", hSliderValue3y);
        material.SetFloat("_LAYER_3_SCROLL_X", hSliderValue3x);

        material.SetColor("_Color", mainColor);
        material.SetColor("_HotlineColor", HLColor);
    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    private GUIStyle style = new GUIStyle();
}
