using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class WARCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static WARGauge JobGauge => Service.JobGauges.Get<WARGauge>();

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Warrior, ClassJobID.Marauder };
    private sealed protected override BaseAction Shield => Defiance;

    /// <summary>
    /// �ػ�
    /// </summary>
    public static BaseAction Defiance { get; } = new(ActionID.Defiance, shouldEndSpecial: true, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction HeavySwing { get; } = new(ActionID.HeavySwing, isTimeline: true);

    /// <summary>
    /// �ײ���
    /// </summary>
    public static BaseAction Maim { get; } = new(ActionID.Maim, isTimeline: true);

    /// <summary>
    /// ����ն �̸�
    /// </summary>
    public static BaseAction StormsPath { get; } = new(ActionID.StormsPath, isTimeline: true);

    /// <summary>
    /// ������ �츫
    /// </summary>
    public static BaseAction StormsEye { get; } = new(ActionID.StormsEye, isTimeline: true)
    {
        ActionCheck = b => Player.WillStatusEndGCD(3, 0, true, StatusID.SurgingTempest),
    };

    /// <summary>
    /// �ɸ�
    /// </summary>
    public static BaseAction Tomahawk { get; } = new(ActionID.Tomahawk, isTimeline: true)
    {
        FilterForTarget = b => TargetFilter.ProvokeTarget(b),
    };

    /// <summary>
    /// �͹�
    /// </summary>
    public static BaseAction Onslaught { get; } = new(ActionID.Onslaught, shouldEndSpecial: true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindTargetForMoving,
    };

    /// <summary>
    /// ����    
    /// </summary>
    public static BaseAction Upheaval { get; } = new(ActionID.Upheaval, isTimeline: true)
    {
        BuffsNeed = new StatusID[] { StatusID.SurgingTempest },
    };

    /// <summary>
    /// ��ѹ��
    /// </summary>
    public static BaseAction Overpower { get; } = new(ActionID.Overpower, isTimeline: true);

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction MythrilTempest { get; } = new(ActionID.MythrilTempest, isTimeline: true);

    /// <summary>
    /// Ⱥɽ¡��
    /// </summary>
    public static BaseAction Orogeny { get; } = new(ActionID.Orogeny, isTimeline: true);

    /// <summary>
    /// ԭ��֮��
    /// </summary>
    public static BaseAction InnerBeast { get; } = new(ActionID.InnerBeast, isTimeline: true)
    {
        ActionCheck = b => JobGauge.BeastGauge >= 50 || Player.HasStatus(true, StatusID.InnerRelease),
    };

    /// <summary>
    /// ԭ���Ľ��
    /// </summary>
    public static BaseAction InnerRelease { get; } = new(ActionID.InnerRelease, isTimeline: true)
    {
        ActionCheck = InnerBeast.ActionCheck,
    };

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction SteelCyclone { get; } = new(ActionID.SteelCyclone, isTimeline: true)
    {
        ActionCheck = InnerBeast.ActionCheck,
    };

    /// <summary>
    /// ս��
    /// </summary>
    public static BaseAction Infuriate { get; } = new(ActionID.Infuriate, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.InnerRelease },
        ActionCheck = b => HaveHostilesInRange && JobGauge.BeastGauge < 50 && InCombat,
    };

    /// <summary>
    /// ��
    /// </summary>
    public static BaseAction Berserk { get; } = new(ActionID.Berserk, isTimeline: true)
    {
        ActionCheck = b => HaveHostilesInRange && !InnerRelease.IsCoolDown,
    };

    /// <summary>
    /// ս��
    /// </summary>
    public static BaseAction ThrillofBattle { get; } = new(ActionID.ThrillofBattle, true, isTimeline: true);

    /// <summary>
    /// ̩Ȼ����
    /// </summary>
    public static BaseAction Equilibrium { get; } = new(ActionID.Equilibrium, true, isTimeline: true);

    /// <summary>
    /// ԭ��������
    /// </summary>
    public static BaseAction NascentFlash { get; } = new(ActionID.NascentFlash, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Vengeance { get; } = new(ActionID.Vengeance, isTimeline: true)
    {
        BuffsProvide = Rampart.BuffsProvide,
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// ԭ����ֱ��
    /// </summary>
    public static BaseAction RawIntuition { get; } = new(ActionID.RawIntuition, isTimeline: true)
    {
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction ShakeItOff { get; } = new(ActionID.ShakeItOff, true, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Holmgang { get; } = new(ActionID.Holmgang, isTimeline: true)
    {
        ChoiceTarget = (tars, mustUse) => Player,
    };

    /// <summary>
    /// ���ı���
    /// </summary>
    public static BaseAction PrimalRend { get; } = new(ActionID.PrimalRend, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.PrimalRendReady }
    };

    private protected override bool EmergencyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        //���� ���Ѫ�����ˡ�
        if (Holmgang.ShouldUse(out act) && BaseAction.TankBreakOtherCheck(JobIDs[0], Holmgang.Target)) return true;
        return base.EmergencyAbility(abilityRemain, nextGCD, out act);
    }

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        //ͻ��
        if (Onslaught.ShouldUse(out act, emptyOrSkipCombo: true)) return true;
        return false;
    }
}
