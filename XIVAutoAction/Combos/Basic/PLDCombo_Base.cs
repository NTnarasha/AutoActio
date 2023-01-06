using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using System.Linq;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;
using XIVAutoAttack.Updaters;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class PLDCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static PLDGauge JobGauge => Service.JobGauges.Get<PLDGauge>();

    /// <summary>
    /// �����
    /// </summary>
    protected static byte OathGauge => JobGauge.OathGauge;

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Paladin, ClassJobID.Gladiator };

    private sealed protected override BaseAction Shield => IronWill;

    protected override bool CanHealSingleSpell => TargetUpdater.PartyMembers.Count() == 1 && base.CanHealSingleSpell;

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction IronWill { get; } = new(ActionID.IronWill, shouldEndSpecial: true, isTimeline: true);

    /// <summary>
    /// �ȷ潣
    /// </summary>
    public static BaseAction FastBlade { get; } = new(ActionID.FastBlade, isTimeline: true);

    /// <summary>
    /// ���ҽ�
    /// </summary>
    public static BaseAction RiotBlade { get; } = new(ActionID.RiotBlade, isTimeline: true);

    /// <summary>
    /// ��Ѫ��
    /// </summary>
    public static BaseAction GoringBlade { get; } = new(ActionID.GoringBlade, isEot: true, isTimeline: true)
    {
        TargetStatus = new[]
        {
            StatusID.GoringBlade,
            StatusID.BladeofValor,
        }
    };

    /// <summary>
    /// սŮ��֮ŭ(��Ȩ��)
    /// </summary>
    public static BaseAction RageofHalone { get; } = new(ActionID.RageofHalone, isTimeline: true);

    /// <summary>
    /// Ͷ��
    /// </summary>
    public static BaseAction ShieldLob { get; } = new(ActionID.ShieldLob, isTimeline: true)
    {
        FilterForTarget = b => TargetFilter.ProvokeTarget(b),
    };

    /// <summary>
    /// ս�ӷ�Ӧ
    /// </summary>
    public static BaseAction FightorFlight { get; } = new(ActionID.FightorFlight, true, isTimeline: true);

    /// <summary>
    /// ȫʴն
    /// </summary>
    public static BaseAction TotalEclipse { get; } = new(ActionID.TotalEclipse, isTimeline: true);

    /// <summary>
    /// ����ն
    /// </summary>
    public static BaseAction Prominence { get; } = new(ActionID.Prominence, isTimeline: true);

    /// <summary>
    /// Ԥ��
    /// </summary>
    public static BaseAction Sentinel { get; } = new(ActionID.Sentinel, isTimeline: true, isFriendly: true)
    {
        BuffsProvide = Rampart.BuffsProvide,
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// ������ת
    /// </summary>
    public static BaseAction CircleofScorn { get; } = new(ActionID.CircleofScorn, isTimeline: true);

    /// <summary>
    /// ���֮��
    /// </summary>
    public static BaseAction SpiritsWithin { get; } = new(ActionID.SpiritsWithin, isTimeline: true);

    /// <summary>
    /// ��ʥ����
    /// </summary>
    public static BaseAction HallowedGround { get; } = new(ActionID.HallowedGround, isTimeline: true);

    /// <summary>
    /// ʥ��Ļ��
    /// </summary>
    public static BaseAction DivineVeil { get; } = new(ActionID.DivineVeil, true, isTimeline: true);

    /// <summary>
    /// ���ʺ���
    /// </summary>
    public static BaseAction Clemency { get; } = new(ActionID.Clemency, true, true, isTimeline: true);

    /// <summary>
    /// ��Ԥ
    /// </summary>
    public static BaseAction Intervention { get; } = new(ActionID.Intervention, true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
    };

    /// <summary>
    /// ��ͣ
    /// </summary>
    public static BaseAction Intervene { get; } = new(ActionID.Intervene, shouldEndSpecial: true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindTargetForMoving,
    };

    /// <summary>
    /// ���｣
    /// </summary>
    public static BaseAction Atonement { get; } = new(ActionID.Atonement, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.SwordOath },
    };

    /// <summary>
    /// ���꽣
    /// </summary>
    public static BaseAction Expiacion { get; } = new(ActionID.Expiacion, isTimeline: true);

    /// <summary>
    /// Ӣ��֮��
    /// </summary>
    public static BaseAction BladeofValor { get; } = new(ActionID.BladeofValor, isTimeline: true);

    /// <summary>
    /// ����֮��
    /// </summary>
    public static BaseAction BladeofTruth { get; } = new(ActionID.BladeofTruth, isTimeline: true);

    /// <summary>
    /// ����֮��
    /// </summary>
    public static BaseAction BladeofFaith { get; } = new(ActionID.BladeofFaith, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.ReadyForBladeofFaith },
    };

    /// <summary>
    /// ������
    /// </summary>
    public static BaseAction Requiescat { get; } = new(ActionID.Requiescat, true, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Confiteor { get; } = new(ActionID.Confiteor, isTimeline: true);

    /// <summary>
    /// ʥ��
    /// </summary>
    public static BaseAction HolyCircle { get; } = new(ActionID.HolyCircle, isTimeline: true);

    /// <summary>
    /// ʥ��
    /// </summary>
    public static BaseAction HolySpirit { get; } = new(ActionID.HolySpirit, isTimeline: true);

    /// <summary>
    /// ��װ����
    /// </summary>
    public static BaseAction PassageofArms { get; } = new(ActionID.PassageofArms, true, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Cover { get; } = new(ActionID.Cover, true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
        ActionCheck = b => OathGauge >= 50,
    };

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Sheltron { get; } = new(ActionID.Sheltron, isTimeline: true)
    {
        ActionCheck = Cover.ActionCheck,
    };

    private protected override bool EmergencyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        if (HallowedGround.ShouldUse(out act) && BaseAction.TankBreakOtherCheck(JobIDs[0], HallowedGround.Target)) return true;
        //��ʥ���� ���л�����ˡ�
        return base.EmergencyAbility(abilityRemain, nextGCD, out act);
    }

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        //��ͣ
        if (Intervene.ShouldUse(out act, emptyOrSkipCombo: true)) return true;
        return false;
    }

    private protected override bool HealSingleGCD(out IAction act)
    {
        //���ʺ���
        if (Clemency.ShouldUse(out act)) return true;
        return false;
    }
}
