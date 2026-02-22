using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Teleportation.Content.NPCs
{
  public class Mechanic : GlobalNPC
  {
    public override void ModifyShop(NPCShop shop)
    {
      if(!(shop.NpcType == NPCID.Mechanic)) return;

      shop.Add(ModContent.ItemType<Content.Items.TeleportTracker>(), Condition.DownedMoonLord);
    }
  }
}
