using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.GCD
{
    public class MNKGCDMeditation : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {
            
            if (Core.Get<IMemApiMonk>().ChakraCount < 5)
            {
                if(Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
                    return 0;
                if (!Core.Me.GetCurrTarget().IsValid)
                    return 0; 
            }
            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Meditation.GetSpell());
        }
    }
}