namespace Linto.Sage.GCD;

/*public class SGEGCDInsGCD : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!Qt.GetQt("小停一下"))
        {
            return -1;
        }
        //检查队列是否有技能
        if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
        {
           // if (AI.Instance.BattleData.HighPrioritySlots_GCD.Peek().CastTime.TotalSeconds > 0)
                if (Core.Resolve<MemApiMove>().IsMoving())
                    return -1;
                return 1;
        }

        if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek().Actions.Count>= 1)
            {
                return 1;
            }

        return -1;
    }

    public void Build(Slot slot)
    {
        if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
        {
            if (AI.Instance.BattleData.NextSlot != null)
                if (AI.Instance.BattleData.NextSlot != null)
                {
                    var slot1 = AI.Instance.BattleData.HighPrioritySlots_GCD.Peek();
                    AI.Instance.BattleData.NextSlot.Add();  
                }
                else
                    AI.Instance.BattleData.NextSlot = new Slot().Add(AI.Instance.BattleData.HighPrioritySlots_GCD.Peek());
        }

        if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
        {
            if (AI.Instance.BattleData.NextSlot != null)
                AI.Instance.BattleData.NextSlot.Add(AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek());
            else
                AI.Instance.BattleData.NextSlot =
                    new Slot().Add(AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek());
        }
        
    }
}*/