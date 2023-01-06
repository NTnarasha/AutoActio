using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;

namespace XIVAutoAttack.Combos.Basic;


internal abstract class GNBCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static GNBGauge JobGauge => Service.JobGauges.Get<GNBGauge>();

    /// <summary>
    /// ��������
    /// </summary>
    protected static byte Ammo => JobGauge.Ammo;

    /// <summary>
    /// �����ĵڼ���combo
    /// </summary>
    protected static byte AmmoComboStep => JobGauge.AmmoComboStep;

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Gunbreaker };
    private sealed protected override BaseAction Shield => RoyalGuard;

    protected override bool CanHealSingleSpell => false;
    protected override bool CanHealAreaSpell => false;

    protected static byte MaxAmmo => Level >= 88 ? (byte)3 : (byte)2;

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction RoyalGuard { get; } = new(ActionID.RoyalGuard, shouldEndSpecial: true, isTimeline: true);

    /// <summary>
    /// ����ն
    /// </summary>
    public static BaseAction KeenEdge { get; } = new(ActionID.KeenEdge, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction NoMercy { get; } = new(ActionID.NoMercy, isTimeline: true);

    /// <summary>
    /// �б���
    /// </summary>
    public static BaseAction BrutalShell { get; } = new(ActionID.BrutalShell, isTimeline: true);

    /// <summary>
    /// αװ
    /// </summary>
    public static BaseAction Camouflage { get; } = new(ActionID.Camouflage, true, isTimeline: true)
    {
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// ��ħ��
    /// </summary>
    public static BaseAction DemonSlice { get; } = new(ActionID.DemonSlice, isTimeline: true);

    /// <summary>
    /// ���׵�
    /// </summary>
    public static BaseAction LightningShot { get; } = new(ActionID.LightningShot, isTimeline: true);

    /// <summary>
    /// Σ������
    /// </summary>
    public static BaseAction DangerZone { get; } = new(ActionID.DangerZone, isTimeline: true);

    /// <summary>
    /// Ѹ��ն
    /// </summary>
    public static BaseAction SolidBarrel { get; } = new(ActionID.SolidBarrel, isTimeline: true);

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction BurstStrike { get; } = new(ActionID.BurstStrike, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Ammo > 0,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Nebula { get; } = new(ActionID.Nebula, true, isTimeline: true)
    {
        BuffsProvide = Rampart.BuffsProvide,
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// ��ħɱ
    /// </summary>
    public static BaseAction DemonSlaughter { get; } = new(ActionID.DemonSlaughter, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Aurora { get; } = new BaseAction(ActionID.Aurora, true, isTimeline: true);

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction Superbolide { get; } = new(ActionID.Superbolide, true, isTimeline: true);

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction SonicBreak { get; } = new(ActionID.SonicBreak, isTimeline: true);

    /// <summary>
    /// �ַ�ն
    /// </summary>
    public static BaseAction RoughDivide { get; } = new(ActionID.RoughDivide, shouldEndSpecial: true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindTargetForMoving,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction GnashingFang { get; } = new(ActionID.GnashingFang, isTimeline: true)
    {
        ActionCheck = b => JobGauge.AmmoComboStep == 0 && JobGauge.Ammo > 0,
    };

    /// <summary>
    /// ���γ岨
    /// </summary>
    public static BaseAction BowShock { get; } = new(ActionID.BowShock, isTimeline: true);

    /// <summary>
    /// ��֮��
    /// </summary>
    public static BaseAction HeartofLight { get; } = new(ActionID.HeartofLight, true, isTimeline: true);

    /// <summary>
    /// ʯ֮��
    /// </summary>
    public static BaseAction HeartofStone { get; } = new(ActionID.HeartofStone, true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
    };

    /// <summary>
    /// ����֮��
    /// </summary>
    public static BaseAction FatedCircle { get; } = new(ActionID.FatedCircle, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Ammo > 0,
    };

    /// <summary>
    /// Ѫ��
    /// </summary>
    public static BaseAction Bloodfest { get; } = new(ActionID.Bloodfest, true, isTimeline: true)
    {
        ActionCheck = b => MaxAmmo - JobGauge.Ammo > 1,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction DoubleDown { get; } = new(ActionID.DoubleDown, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Ammo > 1,
    };

    /// <summary>
    /// ����צ
    /// </summary>
    public static BaseAction SavageClaw { get; } = new(ActionID.SavageClaw, isTimeline: true)
    {
        ActionCheck = b => Service.IconReplacer.OriginalHook(ActionID.GnashingFang) == ActionID.SavageClaw,
    };

    /// <summary>
    /// ����צ
    /// </summary>
    public static BaseAction WickedTalon { get; } = new(ActionID.WickedTalon, isTimeline: true)
    {
        ActionCheck = b => Service.IconReplacer.OriginalHook(ActionID.GnashingFang) == ActionID.WickedTalon,
    };

    /// <summary>
    /// ˺��
    /// </summary>
    public static BaseAction JugularRip { get; } = new(ActionID.JugularRip, isTimeline: true)
    {
        ActionCheck = b => Service.IconReplacer.OriginalHook(ActionID.Continuation) == ActionID.JugularRip,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction AbdomenTear { get; } = new(ActionID.AbdomenTear, isTimeline: true)
    {
        ActionCheck = b => Service.IconReplacer.OriginalHook(ActionID.Continuation) == ActionID.AbdomenTear,
    };

    /// <summary>
    /// ��Ŀ
    /// </summary>
    public static BaseAction EyeGouge { get; } = new(ActionID.EyeGouge, isTimeline: true)
    {
        ActionCheck = b => Service.IconReplacer.OriginalHook(ActionID.Continuation) == ActionID.EyeGouge,
    };

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction Hypervelocity { get; } = new(ActionID.Hypervelocity, isTimeline: true)
    {
        ActionCheck = b => Service.IconReplacer.OriginalHook(ActionID.Continuation)
        == ActionID.Hypervelocity,
    };

    private protected override bool EmergencyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        //�������� ���л�����ˡ�
        if (Superbolide.ShouldUse(out act) && BaseAction.TankBreakOtherCheck(JobIDs[0], Superbolide.Target)) return true;
        return base.EmergencyAbility(abilityRemain, nextGCD, out act);
    }

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        //ͻ��
        if (RoughDivide.ShouldUse(out act, emptyOrSkipCombo: true)) return true;
        return false;
    }
}

