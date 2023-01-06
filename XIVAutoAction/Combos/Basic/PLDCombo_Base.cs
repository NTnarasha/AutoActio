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
    /// 忠义度
    /// </summary>
    protected static byte OathGauge => JobGauge.OathGauge;

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Paladin, ClassJobID.Gladiator };

    private sealed protected override BaseAction Shield => IronWill;

    protected override bool CanHealSingleSpell => TargetUpdater.PartyMembers.Count() == 1 && base.CanHealSingleSpell;

    /// <summary>
    /// 钢铁信念
    /// </summary>
    public static BaseAction IronWill { get; } = new(ActionID.IronWill, shouldEndSpecial: true, isTimeline: true);

    /// <summary>
    /// 先锋剑
    /// </summary>
    public static BaseAction FastBlade { get; } = new(ActionID.FastBlade, isTimeline: true);

    /// <summary>
    /// 暴乱剑
    /// </summary>
    public static BaseAction RiotBlade { get; } = new(ActionID.RiotBlade, isTimeline: true);

    /// <summary>
    /// 沥血剑
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
    /// 战女神之怒(王权剑)
    /// </summary>
    public static BaseAction RageofHalone { get; } = new(ActionID.RageofHalone, isTimeline: true);

    /// <summary>
    /// 投盾
    /// </summary>
    public static BaseAction ShieldLob { get; } = new(ActionID.ShieldLob, isTimeline: true)
    {
        FilterForTarget = b => TargetFilter.ProvokeTarget(b),
    };

    /// <summary>
    /// 战逃反应
    /// </summary>
    public static BaseAction FightorFlight { get; } = new(ActionID.FightorFlight, true, isTimeline: true);

    /// <summary>
    /// 全蚀斩
    /// </summary>
    public static BaseAction TotalEclipse { get; } = new(ActionID.TotalEclipse, isTimeline: true);

    /// <summary>
    /// 日珥斩
    /// </summary>
    public static BaseAction Prominence { get; } = new(ActionID.Prominence, isTimeline: true);

    /// <summary>
    /// 预警
    /// </summary>
    public static BaseAction Sentinel { get; } = new(ActionID.Sentinel, isTimeline: true, isFriendly: true)
    {
        BuffsProvide = Rampart.BuffsProvide,
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// 厄运流转
    /// </summary>
    public static BaseAction CircleofScorn { get; } = new(ActionID.CircleofScorn, isTimeline: true);

    /// <summary>
    /// 深奥之灵
    /// </summary>
    public static BaseAction SpiritsWithin { get; } = new(ActionID.SpiritsWithin, isTimeline: true);

    /// <summary>
    /// 神圣领域
    /// </summary>
    public static BaseAction HallowedGround { get; } = new(ActionID.HallowedGround, isTimeline: true);

    /// <summary>
    /// 圣光幕帘
    /// </summary>
    public static BaseAction DivineVeil { get; } = new(ActionID.DivineVeil, true, isTimeline: true);

    /// <summary>
    /// 深仁厚泽
    /// </summary>
    public static BaseAction Clemency { get; } = new(ActionID.Clemency, true, true, isTimeline: true);

    /// <summary>
    /// 干预
    /// </summary>
    public static BaseAction Intervention { get; } = new(ActionID.Intervention, true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
    };

    /// <summary>
    /// 调停
    /// </summary>
    public static BaseAction Intervene { get; } = new(ActionID.Intervene, shouldEndSpecial: true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindTargetForMoving,
    };

    /// <summary>
    /// 赎罪剑
    /// </summary>
    public static BaseAction Atonement { get; } = new(ActionID.Atonement, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.SwordOath },
    };

    /// <summary>
    /// 偿赎剑
    /// </summary>
    public static BaseAction Expiacion { get; } = new(ActionID.Expiacion, isTimeline: true);

    /// <summary>
    /// 英勇之剑
    /// </summary>
    public static BaseAction BladeofValor { get; } = new(ActionID.BladeofValor, isTimeline: true);

    /// <summary>
    /// 真理之剑
    /// </summary>
    public static BaseAction BladeofTruth { get; } = new(ActionID.BladeofTruth, isTimeline: true);

    /// <summary>
    /// 信念之剑
    /// </summary>
    public static BaseAction BladeofFaith { get; } = new(ActionID.BladeofFaith, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.ReadyForBladeofFaith },
    };

    /// <summary>
    /// 安魂祈祷
    /// </summary>
    public static BaseAction Requiescat { get; } = new(ActionID.Requiescat, true, isTimeline: true);

    /// <summary>
    /// 悔罪
    /// </summary>
    public static BaseAction Confiteor { get; } = new(ActionID.Confiteor, isTimeline: true);

    /// <summary>
    /// 圣环
    /// </summary>
    public static BaseAction HolyCircle { get; } = new(ActionID.HolyCircle, isTimeline: true);

    /// <summary>
    /// 圣灵
    /// </summary>
    public static BaseAction HolySpirit { get; } = new(ActionID.HolySpirit, isTimeline: true);

    /// <summary>
    /// 武装戍卫
    /// </summary>
    public static BaseAction PassageofArms { get; } = new(ActionID.PassageofArms, true, isTimeline: true);

    /// <summary>
    /// 保护
    /// </summary>
    public static BaseAction Cover { get; } = new(ActionID.Cover, true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
        ActionCheck = b => OathGauge >= 50,
    };

    /// <summary>
    /// 盾阵
    /// </summary>
    public static BaseAction Sheltron { get; } = new(ActionID.Sheltron, isTimeline: true)
    {
        ActionCheck = Cover.ActionCheck,
    };

    private protected override bool EmergencyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        if (HallowedGround.ShouldUse(out act) && BaseAction.TankBreakOtherCheck(JobIDs[0], HallowedGround.Target)) return true;
        //神圣领域 如果谢不够了。
        return base.EmergencyAbility(abilityRemain, nextGCD, out act);
    }

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        //调停
        if (Intervene.ShouldUse(out act, emptyOrSkipCombo: true)) return true;
        return false;
    }

    private protected override bool HealSingleGCD(out IAction act)
    {
        //深仁厚泽
        if (Clemency.ShouldUse(out act)) return true;
        return false;
    }
}
