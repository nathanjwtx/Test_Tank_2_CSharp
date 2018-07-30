using Godot;
using System;

public class TileSetMaker : Node
{
    public Vector2 Tilesize = new Vector2(128, 128);
    private Sprite _sprite;

    public override void _Ready()
    {
        _sprite = (Sprite) GetNode("Sprite");
        var texture = _sprite.Texture;
        var texwidth = texture.GetWidth() / Tilesize.x;
        var texheight = texture.GetHeight() / Tilesize.y;
        TileSet tileSet = new TileSet();

        for (int x = 0; x < texwidth; x++)
        {
            for (int y = 0; y < texheight; y++)
            {
                var region = new Rect2(x * Tilesize.y, y * Tilesize.y, Tilesize.x, Tilesize.y);
                var id = x + y * 10;
                tileSet.CreateTile(id);
                tileSet.TileSetTexture(id, texture);
                tileSet.TileSetRegion(id, region);
            }
        }

        ResourceSaver.Save("res://terrain/terrain_tiles.tres", tileSet);
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
