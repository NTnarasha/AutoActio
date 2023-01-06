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
    /// ÊØ»¤
    /// </summary>
    public static BaseAction Defiance { get; } = new(ActionID.Defiance, shouldEndSpecial: true, isTimeline: true);

    /// <summary>
    /// ÖØÅü
    /// </summary>
    public static BaseAction HeavySwing { get; } = new(ActionID.HeavySwing, isTimeline: true);

    /// <summary>
    /// Ð×²ÐÁÑ
    /// </summary>
    public static BaseAction Maim { get; } = new(ActionID.Maim, isTimeline: true);

    /// <summary>
    /// ±©·çÕ¶ ÂÌ¸«
    /// </summary>
    public static BaseAction StormsPath { get; } = new(ActionID.StormsPath, isTimeline: true);

    /// <summary>
    /// ±©·çËé ºì¸«
    /// </summary>
    public static BaseAction StormsEye { get; } = new(ActionID.StormsEye, isTimeline: true)
    {
        ActionCheck = b => Player.WillStatusEndGCD(3, 0, true, StatusID.SurgingTempest),
    };

    /// <summary>
    /// ·É¸«
    /// </summary>
    public static BaseAction Tomahawk { get; } = new(ActionID.Tomahawk, isTimeline: true)
    {
        FilterForTarget = b => TargetFilter.ProvokeTarget(b),
    };

    /// <summary>
    /// ÃÍ¹¥
    /// </summary>
    public static BaseAction Onslaught { get; } = new(ActionID.Onslaught, shouldEndSpecial: true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindTargetForMoving,
    };

    /// <summary>
    /// ¶¯ÂÒ    
    /// </summary>
    public static BaseAction Upheaval { get; } = new(ActionID.Upheaval, isTimeline: true)
    {
        BuffsNeed = new StatusID[] { StatusID.SurgingTempest },
    };

    /// <summary>
    /// ³¬Ñ¹¸«
    /// </summary>
    public static BaseAction Overpower { get; } = new(ActionID.Overpower, isTimeline: true);

    /// <summary>
    /// ÃØÒø±©·ç
    /// </summary>
    public static BaseAction MythrilTempest { get; } = new(ActionID.MythrilTempest, isTimeline: true);

    /// <summary>
    /// ÈºÉ½Â¡Æð
    /// </summary>
    public static BaseAction Orogeny { get; } = new(ActionID.Orogeny, isTimeline: true);

    /// <summary>
    /// Ô­³õÖ®»ê
    /// </summary>
    public static BaseAction InnerBeast { get; } = new(ActionID.InnerBeast, isTimeline: true)
    {
        ActionCheck = b => JobGauge.BeastGauge >= 50 || Player.HasStatus(true, StatusID.InnerRelease),
    };

    /// <summary>
    /// Ô­³õµÄ½â·Å
    /// </summary>
    public static BaseAction InnerRelease { get; } = new(ActionID.InnerRelease, isTimeline: true)
    {
        ActionCheck = InnerBeast.ActionCheck,
    };

    /// <summary>
    /// ¸ÖÌúÐý·ç
    /// </summary>
    public static BaseAction SteelCyclone { get; } = new(ActionID.SteelCyclone, isTimeline: true)
    {
        ActionCheck = InnerBeast.ActionCheck,
    };

    /// <summary>
    /// Õ½º¿
    /// </summary>
    public static BaseAction Infuriate { get; } = new(ActionID.Infuriate, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.InnerRelease },
        ActionCheck = b => HaveHostilesInRange && JobGauge.BeastGauge < 50 && InCombat,
    };

    /// <summary>
    /// ¿ñ±©
    /// </summary>
    public static BaseAction Berserk { get; } = new(ActionID.Berserk, isTimeline: true)
    {
        ActionCheck = b => HaveHostilesInRange && !InnerRelease.IsCoolDown,
    };

    /// <summary>
    /// Õ½Àõ
    /// </summary>
    public static BaseAction ThrillofBattle { get; } = new(ActionID.ThrillofBattle, true, isTimeline: true);

    /// <summary>
    /// Ì©È»×ÔÈô
    /// </summary>
    public static BaseAction Equilibrium { get; } = new(ActionID.Equilibrium, true, isTimeline: true);

    /// <summary>
    /// Ô­³õµÄÓÂÃÍ
    /// </summary>
    public static BaseAction NascentFlash { get; } = new(ActionID.NascentFlash, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindAttackedTarget,
    };

    /// <summary>
    /// ¸´³ð
    /// </summary>
    public static BaseAction Vengeance { get; } = new(ActionID.Vengeance, isTimeline: true)
    {
        BuffsProvide = Rampart.BuffsProvide,
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// Ô­³õµÄÖ±¾õ
    /// </summary>
    public static BaseAction RawIntuition { get; } = new(ActionID.RawIntuition, isTimeline: true)
    {
        ActionCheck = BaseAction.TankDefenseSelf,
    };

    /// <summary>
    /// °ÚÍÑ
    /// </summary>
    public static BaseAction ShakeItOff { get; } = new(ActionID.ShakeItOff, true, isTimeline: true);

    /// <summary>
    /// ËÀ¶·
    /// </summary>
    public static BaseAction Holmgang { get; } = new(ActionID.Holmgang, isTimeline: true)
    {
        ChoiceTarget = (tars, mustUse) => Player,
    };

    /// <summary>
    /// Âù»Ä±ÀÁÑ
    /// </summary>
    public static BaseAction PrimalRend { get; } = new(ActionID.PrimalRend, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.PrimalRendReady }
    };

    private protected override bool EmergencyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        //ËÀ¶· Èç¹ûÑª²»¹»ÁË¡£
        if (Holmgang.ShouldUse(out act) && BaseAction.TankBreakOtherCheck(JobIDs[0], Holmgang.Target)) return true;
        return base.EmergencyAbility(abilityRemain, nextGCD, out act);
    }

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        //Í»½ø
        if (Onslaught.ShouldUse(out act, emptyOrSkipCombo: true)) return true;
        return false;
    }
}
