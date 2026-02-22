using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader.IO;
using System;
using System.Linq;


namespace Teleportation.Content.Players
{
  public class PlayerTeleport : ModPlayer
  {

    public bool canTeleport;
    private Vector2[] _coords = new Vector2[10];
    private int locked_item = -1;

    public override void ResetEffects() =>
      canTeleport = false;

    public override void SaveData(TagCompound tag) =>
      tag["coordsTeleport"] = _coords; 

    public override void LoadData(TagCompound tag)
    {
      if(!tag.ContainsKey("coordsTeleport"))
      {
        _coords = new Vector2[10];
        return;
      }

      _coords = tag.GetList<Vector2>("coordsTeleport")
        .ToArray();

      if(_coords.Length < 10)
        Array.Resize(ref _coords, 10);

    }

    public override void SetControls()
    {
      if(!canTeleport || Main.drawingPlayerChat) return;

      Func<int, Keys> toKey = i => (Keys)((int)Keys.D0 + i);

      var state = Main.keyState;
      var oldState = Main.oldKeyState;
      Func<Keys, bool> isDown = k => state.IsKeyDown(k);
      Func<Keys, bool> justPressed =  k => state.IsKeyDown(k) && !oldState.IsKeyDown(k);
      bool isModifier =  isDown(Keys.F) || isDown(Keys.K);

      if(!isModifier)
      {
        locked_item = -1;
        return;
      }

      if(locked_item == -1) locked_item = Player.selectedItem;
      Player.selectedItem = locked_item;

      Player.controlUseItem = false;
      Player.controlUseTile = false;
      Player.releaseUseItem = false;
      Player.controlMount = false;

      for(int i =0; i<10; i++) Player.controlInv = false;

      int GetPressedDigit() => Enumerable.Range(0, 10)
        .FirstOrDefault(i => justPressed(toKey(i)) , -1);

      int digit = GetPressedDigit();

      if(digit == -1) return;

      if(isDown(Keys.K))
      {
        _coords[digit] = Player.position;
        CombatText.NewText(Player.getRect(), Color.LimeGreen, $"Saved at slot {digit}");
      }
      if(isDown(Keys.F) && _coords[digit] != Vector2.Zero)
      {
        Player.Teleport(_coords[digit], 1);
        CombatText.NewText(Player.getRect(), Color.Cyan, $"Teleported to slot {digit}");
      }
    }
  }
}
