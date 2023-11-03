using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_RiddleofWind : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            {
                return -1;
            }
            if (!Qt.GetQt("疾风极意"))
            {
                return -1;
            }
            if (!SpellsDefine.RiddleofWind.IsReady())
            {
                return -1;
            }

            return 0;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.RiddleofWind.GetSpell());
        }

    }
}