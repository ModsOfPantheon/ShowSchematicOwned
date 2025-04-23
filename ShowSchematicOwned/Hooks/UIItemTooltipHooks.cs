using HarmonyLib;
using Il2Cpp;
using Il2CppPantheonPersist;

namespace ShowSchematicOwned.Hooks;

[HarmonyPatch(typeof(UIItemTooltip), nameof(UIItemTooltip.Show))]
public class UIItemTooltipHooks
{
    private static void Postfix(UIItemTooltip __instance, Item item)
    {
        if (item.SlotType == SlotType.Schematic)
        {
            return;
        }
        
        var localPlayer = EntityPlayerGameObject.LocalPlayer.Cast<EntityPlayerGameObject>();

        var inventory = localPlayer.Inventory;
        
        var abilityRequiredText = UIItemTooltip.Instance.AbilityRequiredText;

        if (item.Template.ItemTypeId == ItemType.Schematic)
        {
            foreach (var inventoryItem in inventory.items)
            {
                if (inventoryItem.Value.SlotType == SlotType.Schematic && inventoryItem.Value.ItemId == item.ItemId)
                {
                    abilityRequiredText.gameObject.active = true;
                    abilityRequiredText.text = "Already have one";
                    break;
                }
            }
        }
    }
}