using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;
using XIVAutoAttack.Updaters;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class SCHCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static SCHGauge JobGauge => Service.JobGauges.Get<SCHGauge>();

    /// <summary>
    /// 契约槽
    /// </summary>
    protected static byte FairyGauge => JobGauge.FairyGauge;

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Scholar };

    private sealed protected override BaseAction Raise => Resurrection;
    /// <summary>
    /// 有豆子
    /// </summary>
    protected static bool HasAetherflow => JobGauge.Aetherflow > 0;
    /// <summary>
    /// 有大天使
    /// </summary>
    protected static bool HasSeraph => JobGauge.SeraphTimer > 0;

    #region 治疗
    /// <summary>
    /// 医术
    /// </summary>
    public static BaseAction Physick { get; } = new(ActionID.Physick, true, isTimeline: true);

    /// <summary>
    /// 鼓舞激励之策
    /// </summary>
    public static BaseAction Adloquium { get; } = new(ActionID.Adloquium, true, isTimeline: true)
    {
        ActionCheck = b => !b.HasStatus(false, StatusID.EukrasianDiagnosis,
            StatusID.EukrasianPrognosis,
            StatusID.Galvanize),
    };


    /// <summary>
    /// 复生
    /// </summary>
    public static BaseAction Resurrection { get; } = new(ActionID.Resurrection, true, isTimeline: true);

    /// <summary>
    /// 士气高扬之策
    /// </summary>
    public static BaseAction Succor { get; } = new(ActionID.Succor, true, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.Galvanize },
    };

    /// <summary>
    /// 生命活性法
    /// </summary>
    public static BaseAction Lustrate { get; } = new(ActionID.Lustrate, true, isTimeline: true)
    {
        ActionCheck = b => HasAetherflow
    };

    /// <summary>
    /// 野战治疗阵
    /// </summary>
    public static BaseAction SacredSoil { get; } = new(ActionID.SacredSoil, true, isTimeline: true)
    {
        ActionCheck = b => HasAetherflow && !IsMoving,
    };

    /// <summary>
    /// 不屈不挠之策
    /// </summary>
    public static BaseAction Indomitability { get; } = new(ActionID.Indomitability, true, isTimeline: true)
    {
        ActionCheck = b => HasAetherflow
    };

    /// <summary>
    /// 深谋远虑之策
    /// </summary>
    public static BaseAction Excogitation { get; } = new(ActionID.Excogitation, true, isTimeline: true)
    {
        ActionCheck = b => HasAetherflow
    };

    /// <summary>
    /// 慰藉
    /// </summary>
    public static BaseAction Consolation { get; } = new(ActionID.Consolation, true, isTimeline: true)
    {
        ActionCheck = b => HasSeraph,
    };

    /// <summary>
    /// 生命回生法
    /// </summary>
    public static BaseAction Protraction { get; } = new(ActionID.Protraction, true, isTimeline: true);
    #endregion
    #region 进攻
    /// <summary>
    /// 毒菌 猛毒菌 蛊毒法
    /// </summary>
    public static BaseAction Bio { get; } = new(ActionID.Bio, isEot: true, isTimeline: true)
    {
        TargetStatus = new StatusID[] { StatusID.Bio, StatusID.Bio2, StatusID.Biolysis },
    };

    /// <summary>
    /// 毁灭 气炎法 魔炎法 死炎法 极炎法
    /// </summary>
    public static BaseAction Ruin { get; } = new(ActionID.Ruin, isTimeline: true);

    /// <summary>
    /// 毁坏
    /// </summary>
    public static BaseAction Ruin2 { get; } = new(ActionID.Ruin2, isTimeline: true);

    /// <summary>
    /// 能量吸收
    /// </summary>
    public static BaseAction EnergyDrain { get; } = new(ActionID.EnergyDrain, isTimeline: true)
    {
        ActionCheck = b => HasAetherflow
    };

    /// <summary>
    /// 破阵法
    /// </summary>
    public static BaseAction ArtofWar { get; } = new(ActionID.ArtofWar, isTimeline: true);//裂阵法 25866
    #endregion
    #region 仙女
    /// <summary>
    /// 炽天召唤
    /// </summary>
    public static BaseAction SummonSeraph { get; } = new(ActionID.SummonSeraph, true, isTimeline: true)
    {
        ActionCheck = b => TargetUpdater.HavePet,
    };

    /// <summary>
    /// 朝日召唤
    /// </summary>
    public static BaseAction SummonEos { get; } = new(ActionID.SummonEos, isTimeline: true)//夕月召唤 17216
    {
        ActionCheck = b => !TargetUpdater.HavePet && (!Player.HasStatus(true, StatusID.Dissipation) || Dissipation.WillHaveOneCharge(30) && Dissipation.EnoughLevel),
    };

    /// <summary>
    /// 仙光的低语/天使的低语
    /// </summary>
    public static BaseAction WhisperingDawn { get; } = new(ActionID.WhisperingDawn, isTimeline: true)
    {
        ActionCheck = b => TargetUpdater.HavePet,
    };

    /// <summary>
    /// 异想的幻光/炽天的幻光
    /// </summary>
    public static BaseAction FeyIllumination { get; } = new(ActionID.FeyIllumination, isTimeline: true)
    {
        ActionCheck = b => TargetUpdater.HavePet,
    };

    /// <summary>
    /// 转化
    /// </summary>
    public static BaseAction Dissipation { get; } = new(ActionID.Dissipation, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.Dissipation },
        ActionCheck = b => !HasAetherflow && !HasSeraph && InCombat && TargetUpdater.HavePet,
    };

    /// <summary>
    /// 以太契约-异想的融光
    /// </summary>
    public static BaseAction Aetherpact { get; } = new(ActionID.Aetherpact, true, isTimeline: true)
    {
        ActionCheck = b => JobGauge.FairyGauge >= 10 && TargetUpdater.HavePet && !HasSeraph
    };

    /// <summary>
    /// 异想的祥光
    /// </summary>
    public static BaseAction FeyBlessing { get; } = new(ActionID.FeyBlessing, isTimeline: true)
    {
        ActionCheck = b => !HasSeraph && TargetUpdater.HavePet,
    };
    #endregion
    #region 其他
    /// <summary>
    /// 以太超流
    /// </summary>
    public static BaseAction Aetherflow { get; } = new(ActionID.Aetherflow, isTimeline: true)
    {
        ActionCheck = b => InCombat && !HasAetherflow
    };

    /// <summary>
    /// 秘策
    /// </summary>
    public static BaseAction Recitation { get; } = new(ActionID.Recitation, isTimeline: true);

    /// <summary>
    /// 连环计
    /// </summary>
    public static BaseAction ChainStratagem { get; } = new(ActionID.ChainStratagem, isTimeline: true)
    {
        ActionCheck = b => InCombat && IsTargetBoss
    };

    /// <summary>
    /// 展开战术
    /// </summary>
    public static BaseAction DeploymentTactics { get; } = new(ActionID.DeploymentTactics, true, isTimeline: true)
    {
        ChoiceTarget = (friends, mustUse) =>
        {
            foreach (var friend in friends)
            {
                if (friend.HasStatus(true, StatusID.Galvanize)) return friend;
            }
            return null;
        },
    };

    /// <summary>
    /// 应急战术
    /// </summary>
    public static BaseAction EmergencyTactics { get; } = new(ActionID.EmergencyTactics, isTimeline: true);

    /// <summary>
    /// 疾风怒涛之计
    /// </summary>
    public static BaseAction Expedient { get; } = new(ActionID.Expedient, isTimeline: true);
    #endregion
}