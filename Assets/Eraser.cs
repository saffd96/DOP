using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private SpriteMask spriteMask;
    [SerializeField] private Camera spriteCam;

    public void AssignScreenAsMask()
    {
        var height = Screen.height;
        var width = Screen.width;
        var depth = 1;

        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0,0,width,height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        spriteCam.targetTexture = renderTexture;
        spriteCam.Render();

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        spriteCam.targetTexture = null;
        RenderTexture.active = currentRenderTexture;
        Destroy(renderTexture);

        Sprite sprite = Sprite.Create(texture, rect, new Vector2(.5f,.5f), Screen.height/10);

        spriteMask.sprite = sprite;
    }
}