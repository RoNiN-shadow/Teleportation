using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace Teleportation.Content.Items 
{
  public class TeleportTracker : ModItem
  {
    public override void SetDefaults()
    {
      Item.width = 24;
      Item.height = 24;
      Item.accessory = true;
      Item.rare = ItemRarityID.Green;
      Item.value = Item.sellPrice(platinum: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
      hideVisual = false;
      player.GetModPlayer<Players.PlayerTeleport>().canTeleport = true;
    }
  }
}
