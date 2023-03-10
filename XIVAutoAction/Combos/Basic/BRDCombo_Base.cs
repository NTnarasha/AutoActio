using Dalamud.Game.ClientState.JobGauge.Enums;
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

internal abstract class BRDCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static BRDGauge JobGauge => Service.JobGauges.Get<BRDGauge>();

    /// <summary>
    /// 诗心数量
    /// </summary>
    protected static byte Repertoire => JobGauge.Repertoire;

    /// <summary>
    /// 当前正在唱的歌
    /// </summary>
    protected static Song Song => JobGauge.Song;

    /// <summary>
    /// 上一首唱的歌
    /// </summary>
    protected static Song LastSong => JobGauge.LastSong;

    /// <summary>
    /// 灵魂之声
    /// </summary>
    protected static byte SoulVoice => JobGauge.SoulVoice;

    /// <summary>
    /// 这首歌啊在多久后还在唱嘛(是否已经结束)
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    protected static bool SongEndAfter(float time)
    {
        return EndAfter(JobGauge.SongTimer / 1000f, time) && JobGauge.SongTimer / 1000f <= time;
    }

    /// <summary>
    /// 这首歌啊在多久后还在唱嘛
    /// </summary>
    /// <param name="abilityCount"></param>
    /// <param name="gctCount"></param>
    /// <returns></returns>
    protected static bool SongEndAfterGCD(uint gctCount = 0, uint abilityCount = 0)
    {
        return EndAfterGCD(JobGauge.SongTimer / 1000f, gctCount, abilityCount);
    }

    public sealed override ClassJobID[] JobIDs => new[] { ClassJobID.Bard, ClassJobID.Archer };


    /// <summary>
    /// 强力射击
    /// </summary>
    public static BaseAction HeavyShoot { get; } = new(ActionID.HeavyShoot, isTimeline: true) { BuffsProvide = new[] { StatusID.StraightShotReady } };

    /// <summary>
    /// 直线射击
    /// </summary>
    public static BaseAction StraitShoot { get; } = new(ActionID.StraitShoot, isTimeline: true) { BuffsNeed = new[] { StatusID.StraightShotReady } };

    /// <summary>
    /// 毒咬箭
    /// </summary>
    public static BaseAction VenomousBite { get; } = new(ActionID.VenomousBite, isEot: true, isTimeline: true)
    {
        TargetStatus = new[] { StatusID.VenomousBite, StatusID.CausticBite }
    };

    /// <summary>
    /// 风蚀箭
    /// </summary>
    public static BaseAction Windbite { get; } = new(ActionID.Windbite, isEot: true, isTimeline: true)
    {
        TargetStatus = new[] { StatusID.Windbite, StatusID.Stormbite }
    };

    /// <summary>
    /// 伶牙俐齿
    /// </summary>
    public static BaseAction IronJaws { get; } = new(ActionID.IronJaws, isEot: true, isTimeline: true)
    {
        TargetStatus = VenomousBite.TargetStatus.Union(Windbite.TargetStatus).ToArray(),
        ActionCheck = b => b.HasStatus(true, VenomousBite.TargetStatus) & b.HasStatus(true, Windbite.TargetStatus),
    };

    /// <summary>
    /// 放浪神的小步舞曲
    /// </summary>
    public static BaseAction WanderersMinuet { get; } = new(ActionID.WanderersMinuet, isTimeline: true);

    /// <summary>
    /// 贤者的叙事谣
    /// </summary>
    public static BaseAction MagesBallad { get; } = new(ActionID.MagesBallad, isTimeline: true);

    /// <summary>
    /// 军神的赞美歌
    /// </summary>
    public static BaseAction ArmysPaeon { get; } = new(ActionID.ArmysPaeon, isTimeline: true);

    /// <summary>
    /// 战斗之声
    /// </summary>
    public static BaseAction BattleVoice { get; } = new(ActionID.BattleVoice, true, isTimeline: true);

    /// <summary>
    /// 猛者强击
    /// </summary>
    public static BaseAction RagingStrikes { get; } = new(ActionID.RagingStrikes, true, isTimeline: true);

    /// <summary>
    /// 光明神的最终乐章
    /// </summary>
    public static BaseAction RadiantFinale { get; } = new(ActionID.RadiantFinale, true, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Coda.Any(s => s != Song.NONE),
    };

    /// <summary>
    /// 纷乱箭
    /// </summary>
    public static BaseAction Barrage { get; } = new(ActionID.Barrage, isTimeline: true);

    /// <summary>
    /// 九天连箭
    /// </summary>
    public static BaseAction EmpyrealArrow { get; } = new(ActionID.EmpyrealArrow, isTimeline: true);

    /// <summary>
    /// 完美音调
    /// </summary>
    public static BaseAction PitchPerfect { get; } = new(ActionID.PitchPerfect, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Song == Song.WANDERER,
    };

    /// <summary>
    /// 失血箭
    /// </summary>
    public static BaseAction Bloodletter { get; } = new(ActionID.Bloodletter, isTimeline: true);

    /// <summary>
    /// 死亡箭雨
    /// </summary>
    public static BaseAction RainofDeath { get; } = new(ActionID.RainofDeath, isTimeline: true);

    /// <summary>
    /// 连珠箭
    /// </summary>
    public static BaseAction QuickNock { get; } = new(ActionID.QuickNock, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.ShadowbiteReady }
    };

    /// <summary>
    /// 影噬箭
    /// </summary>
    public static BaseAction Shadowbite { get; } = new(ActionID.Shadowbite, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.ShadowbiteReady }
    };

    /// <summary>
    /// 光阴神的礼赞凯歌
    /// </summary>
    public static BaseAction WardensPaean { get; } = new(ActionID.WardensPaean, true, isTimeline: true);

    /// <summary>
    /// 大地神的抒情恋歌
    /// </summary>
    public static BaseAction NaturesMinne { get; } = new(ActionID.NaturesMinne, true, isTimeline: true);

    /// <summary>
    /// 侧风诱导箭
    /// </summary>
    public static BaseAction Sidewinder { get; } = new(ActionID.Sidewinder, isTimeline: true);

    /// <summary>
    /// 绝峰箭
    /// </summary>
    public static BaseAction ApexArrow { get; } = new(ActionID.ApexArrow, isTimeline: true)
    {
        ActionCheck = b => JobGauge.SoulVoice >= 20 && !Player.HasStatus(true, StatusID.BlastArrowReady),
    };

    /// <summary>
    /// 爆破箭
    /// </summary>
    public static BaseAction BlastArrow { get; } = new(ActionID.BlastArrow, isTimeline: true)
    {
        ActionCheck = b => Player.HasStatus(true, StatusID.BlastArrowReady),
    };

    /// <summary>
    /// 行吟
    /// </summary>
    public static BaseAction Troubadour { get; } = new(ActionID.Troubadour, true, isTimeline: true)
    {
        ActionCheck = b => !Player.HasStatus(false, StatusID.Troubadour,
            StatusID.Tactician1,
            StatusID.Tactician2,
            StatusID.ShieldSamba),
    };

    private protected override bool EmergencyAbility(byte abilityRemain, IAction nextGCD, out IAction act)
    {
        //有某些非常危险的状态。
        if (CommandController.EsunaOrShield && TargetUpdater.WeakenPeople.Any() || TargetUpdater.DyingPeople.Any())
        {
            if (WardensPaean.ShouldUse(out act, mustUse: true)) return true;
        }
        return base.EmergencyAbility(abilityRemain, nextGCD, out act);
    }
}
