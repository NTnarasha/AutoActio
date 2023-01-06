using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class NINCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static NINGauge JobGauge => Service.JobGauges.Get<NINGauge>();

    /// <summary>
    /// 在风buff中
    /// </summary>
    protected static bool InHuton => JobGauge.HutonTimer > 0;

    /// <summary>
    /// 忍术点数
    /// </summary>
    protected static byte Ninki => JobGauge.Ninki;

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Ninja, ClassJobID.Rogue };
    public class NinAction : BaseAction
    {
        internal BaseAction[] Ninjutsus { get; }
        internal NinAction(ActionID actionID, params BaseAction[] ninjutsus)
            : base(actionID, false, false)
        {
            Ninjutsus = ninjutsus;
        }
    }

    /// <summary>
    /// 隐遁
    /// </summary>
    public static BaseAction Hide { get; } = new(ActionID.Hide, true, isTimeline: true);

    /// <summary>
    /// 双刃旋
    /// </summary>
    public static BaseAction SpinningEdge { get; } = new(ActionID.SpinningEdge, isTimeline: true);

    /// <summary>
    /// 残影
    /// </summary>
    public static BaseAction ShadeShift { get; } = new(ActionID.ShadeShift, true, isTimeline: true);

    /// <summary>
    /// 绝风
    /// </summary>
    public static BaseAction GustSlash { get; } = new(ActionID.GustSlash, isTimeline: true);

    /// <summary>
    /// 飞刀
    /// </summary>
    public static BaseAction ThrowingDagger { get; } = new(ActionID.ThrowingDagger, isTimeline: true);

    /// <summary>
    /// 夺取
    /// </summary>
    public static BaseAction Mug { get; } = new(ActionID.Mug, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Ninki <= 50,
    };

    /// <summary>
    /// 攻其不备
    /// </summary>
    public static BaseAction TrickAttack { get; } = new(ActionID.TrickAttack, isTimeline: true)
    {
        BuffsNeed = new StatusID[] { StatusID.Suiton, StatusID.Hidden },
    };

    /// <summary>
    /// 旋风刃
    /// </summary>
    public static BaseAction AeolianEdge { get; } = new(ActionID.AeolianEdge, isTimeline: true);

    /// <summary>
    /// 血雨飞花
    /// </summary>
    public static BaseAction DeathBlossom { get; } = new(ActionID.DeathBlossom, isTimeline: true);

    /// <summary>
    /// 天之印
    /// </summary>
    public static BaseAction Ten { get; } = new(ActionID.Ten, true, isTimeline: true);

    /// <summary>
    /// 地之印
    /// </summary>
    public static BaseAction Chi { get; } = new(ActionID.Chi, true, isTimeline: true);

    /// <summary>
    /// 人之印
    /// </summary>
    public static BaseAction Jin { get; } = new(ActionID.Jin, true, isTimeline: true);

    /// <summary>
    /// 天地人
    /// </summary>
    public static BaseAction TenChiJin { get; } = new(ActionID.TenChiJin, true, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.Kassatsu, StatusID.TenChiJin },
        ActionCheck = b => JobGauge.HutonTimer > 0,
    };

    /// <summary>
    /// 缩地
    /// </summary>
    public static BaseAction Shukuchi { get; } = new(ActionID.Shukuchi, true, isTimeline: true)
    {
        ChoiceTarget = TargetFilter.FindTargetForMoving,
    };

    /// <summary>
    /// 断绝
    /// </summary>
    public static BaseAction Assassinate { get; } = new(ActionID.Assassinate, isTimeline: true);

    /// <summary>
    /// 命水
    /// </summary>
    public static BaseAction Meisui { get; } = new(ActionID.Meisui, true, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.Suiton },
        ActionCheck = b => JobGauge.Ninki <= 50,
    };

    /// <summary>
    /// 生杀予夺
    /// </summary>
    public static BaseAction Kassatsu { get; } = new(ActionID.Kassatsu, true, isTimeline: true)
    {
        BuffsProvide = TenChiJin.BuffsProvide,
    };

    /// <summary>
    /// 八卦无刃杀
    /// </summary>
    public static BaseAction HakkeMujinsatsu { get; } = new(ActionID.HakkeMujinsatsu, isTimeline: true);

    /// <summary>
    /// 强甲破点突
    /// </summary>
    public static BaseAction ArmorCrush { get; } = new(ActionID.ArmorCrush, isTimeline: true)
    {
        ActionCheck = b => EndAfter(JobGauge.HutonTimer / 1000f, 29) && JobGauge.HutonTimer > 0,
    };

    /// <summary>
    /// 分身之术
    /// </summary>
    public static BaseAction Bunshin { get; } = new(ActionID.Bunshin, true, isTimeline: true)
    {
        ActionCheck = b => Ninki >= 50,
    };

    /// <summary>
    /// 通灵之术·大虾蟆
    /// </summary>
    public static BaseAction HellfrogMedium { get; } = new(ActionID.HellfrogMedium, isTimeline: true)
    {
        ActionCheck = Bunshin.ActionCheck,
    };

    /// <summary>
    /// 六道轮回
    /// </summary>
    public static BaseAction Bhavacakra { get; } = new(ActionID.Bhavacakra, isTimeline: true)
    {
        ActionCheck = Bunshin.ActionCheck,
    };



    /// <summary>
    /// 残影镰鼬
    /// </summary>
    public static BaseAction PhantomKamaitachi { get; } = new(ActionID.PhantomKamaitachi, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.PhantomKamaitachiReady },
    };

    /// <summary>
    /// 月影雷兽牙
    /// </summary>
    public static BaseAction FleetingRaiju { get; } = new(ActionID.FleetingRaiju, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.RaijuReady },
    };

    /// <summary>
    /// 月影雷兽爪
    /// </summary>
    public static BaseAction ForkedRaiju { get; } = new(ActionID.ForkedRaiju, isTimeline: true)
    {
        BuffsNeed = FleetingRaiju.BuffsNeed,
    };

    /// <summary>
    /// 风来刃
    /// </summary>
    public static BaseAction Huraijin { get; } = new(ActionID.Huraijin, isTimeline: true)
    {
        ActionCheck = b => JobGauge.HutonTimer == 0,
    };

    /// <summary>
    /// 梦幻三段
    /// </summary>
    public static BaseAction DreamWithinaDream { get; } = new(ActionID.DreamWithinaDream, isTimeline: true);

    /// <summary>
    /// 风魔手里剑天
    /// </summary>
    public static BaseAction FumaShurikenTen { get; } = new(ActionID.FumaShurikenTen, isTimeline: true);

    /// <summary>
    /// 风魔手里剑人
    /// </summary>
    public static BaseAction FumaShurikenJin { get; } = new(ActionID.FumaShurikenJin, isTimeline: true);

    /// <summary>
    /// 火遁之术天
    /// </summary>
    public static BaseAction KatonTen { get; } = new(ActionID.KatonTen, isTimeline: true);

    /// <summary>
    /// 雷遁之术地
    /// </summary>
    public static BaseAction RaitonChi { get; } = new(ActionID.RaitonChi, isTimeline: true);

    /// <summary>
    /// 土遁之术地
    /// </summary>
    public static BaseAction DotonChi { get; } = new(ActionID.DotonChi, isTimeline: true);

    /// <summary>
    /// 水遁之术人
    /// </summary>
    public static BaseAction SuitonJin { get; } = new(ActionID.SuitonJin, isTimeline: true);


    /// <summary>
    /// 通灵之术
    /// </summary>
    public static NinAction RabbitMedium { get; } = new(ActionID.RabbitMedium);

    /// <summary>
    /// 风魔手里剑
    /// </summary>
    public static NinAction FumaShuriken { get; } = new(ActionID.FumaShuriken, Ten);

    /// <summary>
    /// 火遁之术
    /// </summary>
    public static NinAction Katon { get; } = new(ActionID.Katon, Chi, Ten);

    /// <summary>
    /// 雷遁之术
    /// </summary>
    public static NinAction Raiton { get; } = new(ActionID.Raiton, Ten, Chi);

    /// <summary>
    /// 冰遁之术
    /// </summary>
    public static NinAction Hyoton { get; } = new(ActionID.Hyoton, Ten, Jin);

    /// <summary>
    /// 风遁之术
    /// </summary>
    public static NinAction Huton { get; } = new(ActionID.Huton, Jin, Chi, Ten)
    {
        ActionCheck = b => JobGauge.HutonTimer == 0,
    };

    /// <summary>
    /// 土遁之术
    /// </summary>
    public static NinAction Doton { get; } = new(ActionID.Doton, Jin, Ten, Chi)
    {
        BuffsProvide = new[] { StatusID.Doton },
    };

    /// <summary>
    /// 水遁之术
    /// </summary>
    public static NinAction Suiton { get; } = new(ActionID.Suiton, Ten, Chi, Jin)
    {
        BuffsProvide = new[] { StatusID.Suiton },
        ActionCheck = b => TrickAttack.WillHaveOneChargeGCD(1, 1),
    };

    /// <summary>
    /// 劫火灭却之术
    /// </summary>
    public static NinAction GokaMekkyaku { get; } = new(ActionID.GokaMekkyaku, Chi, Ten);

    /// <summary>
    /// 冰晶乱流之术
    /// </summary>
    public static NinAction HyoshoRanryu { get; } = new(ActionID.HyoshoRanryu, Ten, Jin);

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        if (Shukuchi.ShouldUse(out act, emptyOrSkipCombo: true)) return true;

        return false;
    }
}
