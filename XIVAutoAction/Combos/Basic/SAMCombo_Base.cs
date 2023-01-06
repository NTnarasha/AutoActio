using Dalamud.Game.ClientState.JobGauge.Types;
using System;
using XIVAutoAttack.Actions;
using XIVAutoAttack.Actions.BaseAction;
using XIVAutoAttack.Combos.CustomCombo;
using XIVAutoAttack.Data;
using XIVAutoAttack.Helpers;

namespace XIVAutoAttack.Combos.Basic;

internal abstract class SAMCombo_Base<TCmd> : CustomCombo<TCmd> where TCmd : Enum
{
    private static SAMGauge JobGauge => Service.JobGauges.Get<SAMGauge>();

    /// <summary>
    /// ѩ��
    /// </summary>
    protected static bool HasSetsu => JobGauge.HasSetsu;

    /// <summary>
    /// ����
    /// </summary>
    protected static bool HasGetsu => JobGauge.HasGetsu;

    /// <summary>
    /// ����
    /// </summary>
    protected static bool HasKa => JobGauge.HasKa;

    /// <summary>
    /// ����
    /// </summary>
    protected static byte Kenki => JobGauge.Kenki;

    /// <summary>
    /// ��ѹ
    /// </summary>
    protected static byte MeditationStacks => JobGauge.MeditationStacks;

    public sealed override ClassJobID[] JobIDs => new ClassJobID[] { ClassJobID.Samurai };

    /// <summary>
    /// ��������
    /// </summary>
    protected static byte SenCount => (byte)((HasGetsu ? 1 : 0) + (HasSetsu ? 1 : 0) + (HasKa ? 1 : 0));

    protected static bool HaveMoon => Player.HasStatus(true, StatusID.Fugetsu);
    protected static float MoonTime => Player.StatusTime(true, StatusID.Fugetsu);
    protected static bool HaveFlower => Player.HasStatus(true, StatusID.Fuka);
    protected static float FlowerTime => Player.StatusTime(true, StatusID.Fuka);

    #region ����
    /// <summary>
    /// �з�
    /// </summary>
    public static BaseAction Hakaze { get; } = new(ActionID.Hakaze, isTimeline: true);

    /// <summary>
    /// ���
    /// </summary>
    public static BaseAction Jinpu { get; } = new(ActionID.Jinpu, isTimeline: true);

    /// <summary>
    /// �¹�
    /// </summary>
    public static BaseAction Gekko { get; } = new(ActionID.Gekko, isTimeline: true);

    /// <summary>
    /// ʿ��
    /// </summary>
    public static BaseAction Shifu { get; } = new(ActionID.Shifu, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Kasha { get; } = new(ActionID.Kasha, isTimeline: true);

    /// <summary>
    /// ѩ��
    /// </summary>
    public static BaseAction Yukikaze { get; } = new(ActionID.Yukikaze, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Shoha { get; } = new(ActionID.Shoha, isTimeline: true)
    {
        ActionCheck = b => MeditationStacks == 3
    };
    #endregion

    #region AoE

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Fuga { get; } = new(ActionID.Fuga, isTimeline: true);

    /// <summary>
    /// ���
    /// </summary>
    public static BaseAction Fuko { get; } = new(ActionID.Fuko, isTimeline: true);

    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction Mangetsu { get; } = new(ActionID.Mangetsu, isTimeline: true)
    {
        OtherIDsCombo = new[]
        {
            ActionID.Fuga,ActionID.Fuko
        }
    };
    /// <summary>
    /// ӣ��
    /// </summary>
    public static BaseAction Oka { get; } = new(ActionID.Oka, isTimeline: true)
    {
        OtherIDsCombo = new[]
        {
            ActionID.Fuga,ActionID.Fuko
        }
    };

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction Shoha2 { get; } = new(ActionID.Shoha2, isTimeline: true)
    {
        ActionCheck = b => MeditationStacks == 3
    };

    /// <summary>
    /// ����ն��
    /// </summary>
    public static BaseAction OgiNamikiri { get; } = new(ActionID.OgiNamikiri, isTimeline: true)
    {
        BuffsNeed = new[] { StatusID.OgiNamikiriReady },
        ActionCheck = b => !IsMoving
    };

    /// <summary>
    /// �ط�ն��
    /// </summary>
    public static BaseAction KaeshiNamikiri { get; } = new(ActionID.KaeshiNamikiri, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Kaeshi == Dalamud.Game.ClientState.JobGauge.Enums.Kaeshi.NAMIKIRI
    };
    #endregion

    #region �Ӻ���
    /// <summary>
    /// �˰���
    /// </summary>
    public static BaseAction Higanbana { get; } = new(ActionID.Higanbana, isEot: true, isTimeline: true)
    {
        ActionCheck = b => !IsMoving && SenCount == 1,
        TargetStatus = new[] { StatusID.Higanbana },
    };

    /// <summary>
    /// �����彣
    /// </summary>
    public static BaseAction TenkaGoken { get; } = new(ActionID.TenkaGoken, isTimeline: true)
    {
        ActionCheck = b => !IsMoving && SenCount == 2,
    };

    /// <summary>
    /// ����ѩ�»�
    /// </summary>
    public static BaseAction MidareSetsugekka { get; } = new(ActionID.MidareSetsugekka, isTimeline: true)
    {
        ActionCheck = b => !IsMoving && SenCount == 3,
    };

    /// <summary>
    /// ��ط�
    /// </summary>
    public static BaseAction TsubameGaeshi { get; } = new(ActionID.TsubameGaeshi, isTimeline: true);

    /// <summary>
    /// �ط��彣
    /// </summary>
    public static BaseAction KaeshiGoken { get; } = new(ActionID.KaeshiGoken, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Kaeshi == Dalamud.Game.ClientState.JobGauge.Enums.Kaeshi.GOKEN
    };

    /// <summary>
    /// �ط�ѩ�»�
    /// </summary>
    public static BaseAction KaeshiSetsugekka { get; } = new(ActionID.KaeshiSetsugekka, isTimeline: true)
    {
        ActionCheck = b => JobGauge.Kaeshi == Dalamud.Game.ClientState.JobGauge.Enums.Kaeshi.SETSUGEKKA
    };
    #endregion

    #region ����
    /// <summary>
    /// ����
    /// </summary>
    public static BaseAction ThirdEye { get; } = new(ActionID.ThirdEye, true, isTimeline: true);

    /// <summary>
    /// ���
    /// </summary>
    public static BaseAction Enpi { get; } = new(ActionID.Enpi, isTimeline: true);

    /// <summary>
    /// ����ֹˮ
    /// </summary>
    public static BaseAction MeikyoShisui { get; } = new(ActionID.MeikyoShisui, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.MeikyoShisui },
    };

    /// <summary>
    /// Ҷ��
    /// </summary>
    public static BaseAction Hagakure { get; } = new(ActionID.Hagakure, isTimeline: true)
    {
        ActionCheck = b => SenCount > 0
    };

    /// <summary>
    /// ��������
    /// </summary>
    public static BaseAction Ikishoten { get; } = new(ActionID.Ikishoten, isTimeline: true)
    {
        BuffsProvide = new[] { StatusID.OgiNamikiriReady },
        ActionCheck = b => InCombat
    };
    #endregion

    #region ��ɱ��
    /// <summary>
    /// ��ɱ��������
    /// </summary>
    public static BaseAction HissatsuShinten { get; } = new(ActionID.HissatsuShinten, isTimeline: true)
    {
        ActionCheck = b => Kenki >= 25
    };

    /// <summary>
    /// ��ɱ��������
    /// </summary>
    public static BaseAction HissatsuGyoten { get; } = new(ActionID.HissatsuGyoten, isTimeline: true)
    {
        ActionCheck = b => Kenki >= 10 && !Player.HasStatus(true, StatusID.Bind1, StatusID.Bind2)
    };

    /// <summary>
    /// ��ɱ����ҹ��
    /// </summary>
    public static BaseAction HissatsuYaten { get; } = new(ActionID.HissatsuYaten, isTimeline: true)
    {
        ActionCheck = HissatsuGyoten.ActionCheck
    };

    /// <summary>
    /// ��ɱ��������
    /// </summary>
    public static BaseAction HissatsuKyuten { get; } = new(ActionID.HissatsuKyuten, isTimeline: true)
    {
        ActionCheck = b => Kenki >= 25
    };

    /// <summary>
    /// ��ɱ��������
    /// </summary>
    public static BaseAction HissatsuGuren { get; } = new(ActionID.HissatsuGuren, isTimeline: true)
    {
        ActionCheck = b => Kenki >= 25
    };

    /// <summary>
    /// ��ɱ������Ӱ
    /// </summary>
    public static BaseAction HissatsuSenei { get; } = new(ActionID.HissatsuSenei, isTimeline: true)
    {
        ActionCheck = b => Kenki >= 25
    };
    #endregion

    private protected override bool MoveAbility(byte abilityRemain, out IAction act)
    {
        if (HissatsuGyoten.ShouldUse(out act, emptyOrSkipCombo: true)) return true;
        return false;
    }
}