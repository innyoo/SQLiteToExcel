﻿using Org.BouncyCastle.Math.EC.Rfc8032;
using System.Collections;

namespace SQLiteToExcel.DAL
{
    internal static class Languagehelper
    {
        public static Hashtable D1 = new Hashtable();
        public static Hashtable D2 = new Hashtable();
        
        public static string GetNLS(object key)
        {
            if (D2.ContainsKey($"{key}"))
            {
                return (string)D2[$"{key}"];
            }
            else
            {
                return "?";
            }
        }

        //D1.Add("9000", "System power on");
        //D2.Add("9000", "系统启动");
        public static void LanguageHelper()
        {
            D1.Add("9000", "System power on");
            D2.Add("9000", "系统启动");
            D1.Add("9001", "System power off");
            D2.Add("9001", "系统关机");

            D1.Add("1000", "CAN bus error");
            D2.Add("1000", "CAN总线错误");
            D1.Add("4000", "CAN bus OK");
            D2.Add("4000", "CAN总线正常");
            D1.Add("1001", "Pump offline");
            D2.Add("1001", "泵离线");
            D1.Add("4001", "Pump online");
            D2.Add("4001", "泵在线");
            D1.Add("1002", "Console offline");
            D2.Add("1002", "主机离线");
            D1.Add("4002", "Console online");
            D2.Add("4002", "主机在线");
            D1.Add("1003", "Sensor board offline");
            D2.Add("1003", "传感器HUB离线");
            D1.Add("4003", "Sensor board online");
            D2.Add("4003", "传感器HUB在线");
            D1.Add("1004", "Ppre-Oxy offline");
            D2.Add("1004", "P2膜前压力传感器离线");
            D1.Add("4004", "Ppre-Oxy online");
            D2.Add("4004", "P2膜前压力传感器在线");
            D1.Add("1005", "Ppost-Oxy offline");
            D2.Add("1005", "P3膜后压力传感器离线");
            D1.Add("4005", "Ppost-Oxy online");
            D2.Add("4005", "P3膜后压力传感器在线");

            //   D1.Add("1006", "SaO₂ Offline");
            //   D2.Add("1006", "SaO₂传感器离线");
            //   D1.Add("4006", "SaO₂ Online");
            //   D2.Add("4006", "SaO₂传感器在线");
            //   D1.Add("1007", "SvO₂ Offline");
            //   D2.Add("1007", "SvO₂传感器离线");
            //   D1.Add("4007", "SvO₂ Online");
            //   D2.Add("4007", "SvO₂传感器在线");

            D1.Add("1006", "Arterial T2 offline");
            D2.Add("1006", "T2动脉温度传感器离线");
            D1.Add("4006", "Arterial T2 online");
            D2.Add("4006", "T2动脉温度传感器在线");
            D1.Add("1007", "Venous T1 offline");
            D2.Add("1007", "T1静脉温度传感器离线");
            D1.Add("4007", "Venous T1 online");
            D2.Add("4007", "T1静脉温度传感器在线");


            D1.Add("4008", "Pump started");
            D2.Add("4008", "泵启动");
            D1.Add("4009", "Pump stop");
            D2.Add("4009", "泵停止");
            D1.Add("1010", "Flow above alarm limit");
            D2.Add("1010", "流量高于报警限");
            D1.Add("4010", "Recovered from flow above alarm limit");
            D2.Add("4010", "流量高限报警恢复");
            D1.Add("1011", "Flow below alarm limit");
            D2.Add("1011", "流量低于报警限");
            D1.Add("4011", "Recovered from flow below alarm limit");
            D2.Add("4011", "流量低限报警恢复");

            D1.Add("1015", "Flow ≤ 0L/min");
            D2.Add("1015", "流量≤0L/min");
            D1.Add("4015", "Recovered from flow ≤ 0L/min");
            D2.Add("4015", "流量≤0L/min报警恢复");


            D1.Add("1087", "Flow measurement out of range");
            D2.Add("1087", "流量测量超出范围");
            D1.Add("4087", "Flow measurement out of range alarm recovery");
            D2.Add("4087", "流量测量超出范围报警恢复");
            D1.Add("1088", "P1 measurement out of range");
            D2.Add("1088", "P1压力测量超出范围");
            D1.Add("4088", "P1 measurement out of range alarm recovery");
            D2.Add("4088", "P1压力测量超出范围报警恢复");
            D1.Add("1089", "P2 measurement out of range");
            D2.Add("1089", "P2压力测量超出范围");
            D1.Add("4089", "P2 measurement out of range alarm recovery");
            D2.Add("4089", "P2压力测量超出范围报警恢复");
            D1.Add("1090", "P3 measurement out of range");
            D2.Add("1090", "P3压力测量超出范围");
            D1.Add("4090", "P3 measurement out of range alarm recovery");
            D2.Add("4090", "P3压力测量超出范围报警恢复");
            D1.Add("1091", "T1 measurement out of range");
            D2.Add("1091", "T1静脉温度测量超出范围");
            D1.Add("4091", "T1 measurement out of range alarm recovery");
            D2.Add("4091", "T1静脉温度测量超出范围报警恢复");
            D1.Add("1092", "T2 measurement out of range");
            D2.Add("1092", "T2动脉温度测量超出范围");
            D1.Add("4092", "T2 measurement out of range alarm recovery");
            D2.Add("4092", "T2动脉温度测量超出范围报警恢复");

            D1.Add("1093", "Abnormally flow control");
            D2.Add("1093", "流量控制异常");
            D1.Add("4093", "Abnormally flow control alarm recovery");
            D2.Add("4093", "流量控制异常报警恢复");


            D1.Add("1012", "Bubbles detected in circuit");
            D2.Add("1012", "管路有气泡");
            D1.Add("4012", "Bubbles no longer detected at sensor location");
            D2.Add("4012", "管路气泡消失(传感器位置）");
            D1.Add("2013", "RPM above alarm limit");
            D2.Add("2013", "泵转速高于报警限");
            D1.Add("4013", "Recovered from RPM above alarm aimit");
            D2.Add("4013", "泵转速高限报警恢复");
            D1.Add("2014", "RPM below alarm limit");
            D2.Add("2014", "泵转速低于报警限");
            D1.Add("4014", "Recovered from RPM below alarm limit");
            D2.Add("4014", "泵转速低限报警恢复");

            D1.Add("1115", "PaO₂ above alarm limit");
            D2.Add("1115", "PaO₂传感器高于报警限");
            D1.Add("4115", "Recovered from PaO₂ above alarm limit");
            D2.Add("4115", "PaO₂传感器高限报警恢复");
            D1.Add("1116", "PaO₂ below alarm limit");
            D2.Add("1116", "PaO₂传感器低于报警限");
            D1.Add("4116", "Recovered from PaO₂ below alarm limit");
            D2.Add("4116", "PaO₂传感器低限报警恢复");

            D1.Add("2017", "Bottom battery is low");
            D2.Add("2017", "下电池电量低");
            D1.Add("4017", "Recovered from bottom battery low");
            D2.Add("4017", "下电池电量正常");
            D1.Add("2018", "Top battery is low");
            D2.Add("2018", "上电池电量低");
            D1.Add("4018", "Recovered from top battery low");
            D2.Add("4018", "上电池电量正常");

            D1.Add("1019", "Ppre-Pump above alarm limit");
            D2.Add("1019", "P1泵前压力传感器高于报警限");
            D1.Add("4019", "Recovered from Ppre-Pump above alarm limit");
            D2.Add("4019", "P1泵前压力传感器高限报警恢复");
            D1.Add("1020", "Ppre-Pump below alarm limit");
            D2.Add("1020", "P1泵前压力传感器低于报警限");
            D1.Add("4020", "Recovered from Ppre-Pump below alarm limit");
            D2.Add("4020", "P1泵前压力传感器低限报警恢复");

            D1.Add("1021", "Ppre-Oxy above alarm limit");
            D2.Add("1021", "P2膜前压力传感器高于报警限");
            D1.Add("4021", "Recovered from Ppre-Oxy above alarm limit");
            D2.Add("4021", "P2膜前压力传感器高限报警恢复");
            D1.Add("1022", "Ppre-Oxy below alarm limit");
            D2.Add("1022", "P2膜前压力传感器低于报警限");
            D1.Add("4022", "Recovered from Ppre-Oxy below alarm limit");
            D2.Add("4022", "P2膜前压力传感器低限报警恢复");

            D1.Add("1023", "Ppost-Oxy above alarm limit");
            D2.Add("1023", "P3膜后压力传感器高于报警限");
            D1.Add("4023", "Recovered from Ppost-Oxy above alarm limit");
            D2.Add("4023", "P3膜后压力传感器高限报警恢复");
            D1.Add("1024", "Ppost-Oxy below alarm limit");
            D2.Add("1024", "P3膜后压力传感器低于报警限");
            D1.Add("4024", "Recovered from Ppost-Oxy below alarm limit");
            D2.Add("4024", "P3膜后压力传感器低限报警恢复");


            D1.Add("1025", "ΔP above alarm limit");
            D2.Add("1025", "跨膜压差高于报警限");
            D1.Add("4025", "Recovered from ΔP above alarm limit");
            D2.Add("4025", "跨膜压差高限报警恢复");
            D1.Add("2026", "ΔP below alarm limit");
            D2.Add("2026", "跨膜压差低于报警限");
            D1.Add("4026", "Recovered from ΔP below alarm limit");
            D2.Add("4026", "跨膜压差低限报警恢复");

            D1.Add("2027", "SaO₂ above alarm limit");
            D2.Add("2027", "SaO₂传感器高于报警限");
            D1.Add("4027", "Recovered from SaO₂ above alarm limit");
            D2.Add("4027", "SaO₂传感器高限报警恢复");
            D1.Add("1028", "SaO₂ below alarm limit");
            D2.Add("1028", "SaO₂传感器低于报警限");
            D1.Add("4028", "Recovered from SaO₂ below alarm limit");
            D2.Add("4028", "SaO₂传感器低限报警恢复");

            D1.Add("2029", "Hb Above alarm limit");
            D2.Add("2029", "血红蛋白传感器高于报警限");
            D1.Add("4029", "Recovered from Hb above alarm limit");
            D2.Add("4029", "血红蛋白传感器高限报警恢复");
            D1.Add("1030", "Hb below alarm limit");
            D2.Add("1030", "血红蛋白传感器低于报警限");
            D1.Add("4030", "Recovered from Hb below alarm limit");
            D2.Add("4030", "血红蛋白传感器低限报警恢复");

            //   D1.Add("1031", "ARTERY TEMP Above Alarm Limit");
            //   D2.Add("1031", "T2动脉温度传感器高于报警限");
            //   D1.Add("4031", "Recovered from ARTERY TEMP Above Alarm Limit");
            //   D2.Add("4031", "T2动脉温度传感器高限报警恢复");
            //   D1.Add("1032", "ARTERY TEMP Below Alarm Limit");
            //   D2.Add("1032", "T2动脉温度传感器低于报警限");
            //   D1.Add("4032", "Recovered from ARTERY TEMP Below Alarm Limit");
            //   D2.Add("4032", "T2动脉温度传感器低限报警恢复");


            D1.Add("1033", "Pump disconnected");
            D2.Add("1033", "泵连接断开");
            D1.Add("4033", "Pump connected");
            D2.Add("4033", "泵已连接");

            D1.Add("4034", "Bottom battery disconnected");
            D2.Add("4034", "下电池连接断开");
            D1.Add("4035", "Bottom battery connected");
            D2.Add("4035", "下电池已连接");

            D1.Add("4036", "Top battery disconnected");
            D2.Add("4036", "上电池连接断开");
            D1.Add("4037", "Top battery connected");
            D2.Add("4037", "上电池已连接");

            D1.Add("4038", "AC power disconnected");
            D2.Add("4038", "适配器连接断开");
            D1.Add("4039", "AC power connected");
            D2.Add("4039", "适配器已连接");

            D1.Add("4040", "DC power disconnected");
            D2.Add("4040", "车载适配器连接断开");
            D1.Add("4041", "DC power connected");
            D2.Add("4041", "车载适配器已连接");

            D1.Add("1042", "PaO₂ disconnected");
            D2.Add("1042", "PaO₂传感器连接断开");
            D1.Add("4042", "PaO₂ connected");
            D2.Add("4042", "PaO₂传感器已连接");

            D1.Add("1043", "Sensor board disconnected");
            D2.Add("1043", "传感器Hub连接断开");
            D1.Add("4043", "Sensor board connected");
            D2.Add("4043", "传感器Hub已连接");

            D1.Add("1044", "Ppre-Pump disconnected");
            D2.Add("1044", "P1泵前压力传感器断开");
            D1.Add("4044", "Ppre-Pump connected");
            D2.Add("4044", "P1泵前压力传感器已连接");

            D1.Add("1045", "Ppre-Oxy disconnected");
            D2.Add("1045", "P2膜前压力传感器断开");
            D1.Add("4045", "Ppre-Oxy connected");
            D2.Add("4045", "P2膜前压力传感器已连接");

            D1.Add("1046", "Ppost-Oxy disconnected");
            D2.Add("1046", "P3膜后压力传感器断开");
            D1.Add("4046", "Ppost-Oxy connected");
            D2.Add("4046", "P3膜后压力传感器已连接");


            //   D1.Add("2047", "SvO₂ Disconnected");
            //   D2.Add("2047", "SvO₂传感器断开");
            //   D1.Add("4047", "SvO₂ Connected");
            //   D2.Add("4047", "SvO₂传感器已连接");

            //   D1.Add("2048", "SaO₂ Disconnected");
            //   D2.Add("2048", "SaO₂传感器断开");
            //   D1.Add("4048", "SaO₂ Connected");
            //   D2.Add("4048", "SaO₂传感器已连接");

            D1.Add("1047", "Venous T1 sensor disconnected");
            D2.Add("1047", "T1静脉温度传感器断开");
            D1.Add("4047", "Venous T1 sensor connected");
            D2.Add("4047", "T1静脉温度传感器已连接");

            D1.Add("1048", "Arterial T2 sensor disconnected");
            D2.Add("1048", "T2动脉温度传感器断开");
            D1.Add("4048", "Arterial T2 sensor connected");
            D2.Add("4048", "T2动脉温度传感器已连接");

            D1.Add("1049", "Bottom battery offline");
            D2.Add("1049", "下电池离线");
            D1.Add("4049", "Bottom battery online");
            D2.Add("4049", "下电池在线");

            D1.Add("1050", "Top battery offline");
            D2.Add("1050", "上电池离线");
            D1.Add("4050", "Top battery online");
            D2.Add("4050", "上电池在线");

            D1.Add("2051", "Bottom battery error (%1)");
            D2.Add("2051", "下电池故障(%1)");
            D1.Add("4051", "Recovered from bottom battery error");
            D2.Add("4051", "下电池故障恢复");

            D1.Add("2052", "Top battery error (%1)");
            D2.Add("2052", "上电池故障(%1)");
            D1.Add("4052", "Recovered from top battery error");
            D2.Add("4052", "上电池故障恢复");

            D1.Add("1053", "Pump software error (%1)");
            D2.Add("1053", "泵软件故障(%1)");
            //D1.Add("4053", "Recovered from Pumpdrive Software Error");
            //D2.Add("4053", "泵软件故障恢复");

            D1.Add("1094", "Pump communication error (%1)");
            D2.Add("1094", "泵通讯故障(%1)");

            D1.Add("1095", "T1 hardware error (%1)");
            D2.Add("1095", "T1硬件故障(%1)");
            D1.Add("1096", "T2 hardware error (%1)");
            D2.Add("1096", "T2硬件故障(%1)");
            D1.Add("1099", "T1 fault (%1)");
            D2.Add("1099", "T1异常(%1)");
            D1.Add("1100", "T2 fault (%1)");
            D2.Add("1100", "T2异常(%1)");
            D1.Add("1097", "Hub fault (%1)");
            D2.Add("1097", "Hub异常(%1)");
            D1.Add("1098", "Hub hardware error (%1)");
            D2.Add("1098", "Hub硬件故障(%1)");

            D1.Add("2101", "Flow sensor fault (%1)");
            D2.Add("2101", "流量传感器故障(%1)");

            D1.Add("1054", "Pump unable to start");
            D2.Add("1054", "泵无法启动");
            D1.Add("4054", "Pump already started");
            D2.Add("4054", "泵已启动");
            D1.Add("1055", "Pump unable to stop");
            D2.Add("1055", "泵无法关闭");
            D1.Add("4055", "Pump already stopped");
            D2.Add("4055", "泵已关闭");
            D1.Add("1056", "Failed to set pump speed");
            D2.Add("1056", "设置泵转速失败");


            //    D1.Add("1057", "SvO₂ Above Alarm Limit");
            //    D2.Add("1057", "SvO₂传感器高于报警限");
            //    D1.Add("4057", "Recovered from SvO₂ Above Alarm Limit");
            //    D2.Add("4057", "SvO₂传感器高限报警恢复");
            //    D1.Add("1058", "SvO₂ Below Alarm Limit");
            //    D2.Add("1058", "SvO₂传感器低于报警限");
            //    D1.Add("4058", "Recovered from SvO₂ Below Alarm Limit");
            //    D2.Add("4058", "SvO₂传感器低限报警恢复");

            D1.Add("1057", "Arterial T2 above alarm limit");
            D2.Add("1057", "T2动脉温度传感器高于报警限");
            D1.Add("4057", "Recovered from arterial T2 above alarm limit");
            D2.Add("4057", "T2动脉温度传感器高限报警恢复");
            D1.Add("1058", "Arterial T2 below alarm limit");
            D2.Add("1058", "T2动脉温度传感器低于报警限");
            D1.Add("4058", "Recovered from arterial T2 below alarm limit");
            D2.Add("4058", "T2动脉温度传感器低限报警恢复");

            D1.Add("1059", "Venous T1 above alarm limit");
            D2.Add("1059", "T1静脉温度传感器高于报警限");
            D1.Add("4059", "Recovered from venous T1 above alarm limit");
            D2.Add("4059", "T1静脉温度传感器高限报警恢复");
            D1.Add("1060", "Venous T1 below alarm limit");
            D2.Add("1060", "T1静脉温度传感器低于报警限");
            D1.Add("4060", "Recovered from venous T1 below alarm limit");
            D2.Add("4060", "T1静脉温度传感器低限报警恢复");

            D1.Add("1061", "Flow sensor disconnected");
            D2.Add("1061", "流量传感器连接断开");
            D1.Add("4061", "Flow sensor connected");
            D2.Add("4061", "流量传感器已连接");

            D1.Add("3062", "Power Adapter disconnected");
            D2.Add("3062", "电源已断开");

            D1.Add("1063", "Bottom battery depleted");
            D2.Add("1063", "下电池电量耗尽");

            D1.Add("1064", "Top Battery depleted");
            D2.Add("1064", "上电池电量耗尽");

            //E3065
            D1.Add("3065", "Please insert battery");
            D2.Add("3065", "请插入电池");

            D1.Add("1066", "Pump speed error");
            D2.Add("1066", "泵转速失速");

            D1.Add("4066", "Pump speed normal");
            D2.Add("4066", "泵转速正常");

            D1.Add("3067", "Pump selfcheck failed (%1)");
            D2.Add("3067", "泵自检异常(%1)");

            D1.Add("1068", "Pump hardware error (%1)");
            D2.Add("1068", "泵硬件故障(%1)");
            //D1.Add("4068", "Recovered from Pumpdrive Hardware Error");
            //D2.Add("4068", "泵硬件故障恢复");

            D1.Add("3069", "Pre-Pump pressure regulated mode activated");
            D2.Add("3069", "P1泵前压力进入稳压模式");

            D1.Add("2070", "Alarm light fault");
            D2.Add("2070", "报警灯故障");
            D1.Add("2071", "Audio fault");
            D2.Add("2071", "声音故障");
            D1.Add("2072", "Red alarm light fault");
            D2.Add("2072", "红色报警灯故障");
            D1.Add("2073", "Orange-yellow alarm light fault");
            D2.Add("2073", "橙黄色报警灯故障");
            D1.Add("1072", "T1 sensor mis-inserted into T2 connector");
            D2.Add("1072", "T1传感器错插到T2的接头");
            D1.Add("1073", "T2 sensor mis-inserted into T1 connector");
            D2.Add("1073", "T2传感器错插到T1的接头");
            D1.Add("1074", "BPU fault (%1)");
            D2.Add("1074", "BPU 异常(%1)");

            D1.Add("1075", "Hardware fault");
            D2.Add("1075", "硬件故障");

            D1.Add("1076", "Pump stopped triggered by bubble alarm");
            D2.Add("1076", "气泡报警停泵");

            D1.Add("1077", "P1 pressure is lower than the regulated limit");
            D2.Add("1077", "P1泵前压力低于稳压限值");

            D1.Add("1078", "P1 pressure failure");
            D2.Add("1078", "P1压力传感器失效");
            D1.Add("1079", "P2 pressure failure");
            D2.Add("1079", "P2压力传感器失效");
            D1.Add("1080", "P3 pressure failure");
            D2.Add("1080", "P3压力传感器失效");


            D1.Add("1102", "Display panel selfcheck error (%1)");
            D2.Add("1102", "控制面板自检错误 (%1)");

            D1.Add("1103", "Pump selfcheck error (%1)");
            D2.Add("1103", "泵自检错误 (%1)");

            D1.Add("1104", "Pump selfcheck error (%1)");
            D2.Add("1104", "泵自检错误 (%1)");

            D1.Add("1105", "System selfcheck error (%1)");
            D2.Add("1105", "主机自检错误 (%1)");

            D1.Add("1106", "Hub selfcheck error (%1)");
            D2.Add("1106", "Hub自检错误 (%1)");

            D1.Add("1107", "P1 selfcheck error (%1)");
            D2.Add("1107", "P1自检错误 (%1)");

            D1.Add("1108", "P2 selfcheck error (%1)");
            D2.Add("1108", "P2自检错误 (%1)");

            D1.Add("1109", "P3 selfcheck error (%1)");
            D2.Add("1109", "P3自检错误 (%1)");

            D1.Add("1110", "T1 selfcheck error (%1)");
            D2.Add("1110", "T1自检错误 (%1)");

            D1.Add("1111", "T2 selfcheck error (%1)");
            D2.Add("1111", "T2自检错误 (%1)");

            D1.Add("1112", "Abnormally stop of pump");
            D2.Add("1112", "泵异常停止");

            D1.Add("1113", "Insufficient storage space");
            D2.Add("1113", "存储空间不足");


            D1.Add("4095", "Need to exception self-test, %1");
            D2.Add("4095", "需要执行异常自检, %1");


            D1.Add("4078", "Powering off, battery depleted");
            D2.Add("4078", "关机,电池电量耗尽.");

            D1.Add("3079", "Flow selfcheck failed (%1)");
            D2.Add("3079", "流量传感器自检异常(%1)");

            D1.Add("3080", "T1 selfcheck failed (%1)");
            D2.Add("3080", "T1温度传感器自检异常(%1)");

            D1.Add("3081", "T2 selfcheck failed (%1)");
            D2.Add("3081", "T2温度传感器自检异常(%1)");

            D1.Add("3082", "P1 selfcheck failed (%1)");
            D2.Add("3082", "P1压力传感器自检异常(%1)");

            D1.Add("3083", "P2 selfcheck failed (%1)");
            D2.Add("3083", "P2压力传感器自检异常(%1)");

            D1.Add("3084", "P3 selfcheck failed (%1)");
            D2.Add("3084", "P3压力传感器自检异常(%1)");

            D1.Add("2074", "BPU fan fault (%1)");
            D2.Add("2074", "BPU风扇故障(%1)");

            D1.Add("4085", "BPU abnormally shutdown");
            D2.Add("4085", "BPU 异常关机");

            D1.Add("1086", "BPU single fault (%1)");
            D2.Add("1086", "BPU 单一故障(%1)");

            D1.Add("4114", "Application reboot recovery");
            D2.Add("4114", "应用重启恢复");

            //LABEL
            D1.Add("8000", "PRIMING MODE");
            D2.Add("8000", "预充模式");

            D1.Add("8001", "Pumpdrive");
            D2.Add("8001", "泵");

            D1.Add("8002", "Temperature");
            D2.Add("8002", "温度");

            D1.Add("8003", "Flow");
            D2.Add("8003", "流量");

            D1.Add("8004", "Ppre-Pump");
            D2.Add("8004", "P1泵前压力");

            D1.Add("8005", "Ppre-Oxy");
            D2.Add("8005", "P2膜前压力");

            D1.Add("8006", "Ppost-Oxy");
            D2.Add("8006", "P3膜后压力");

            D1.Add("8007", "Hb");
            D2.Add("8007", "血红蛋白");

            D1.Add("8008", "MODULE");
            D2.Add("8008", "模块");

            D1.Add("8009", "PUMP");
            D2.Add("8009", "泵驱动控制");

            D1.Add("8010", "ZERO PRESSURE SENSOR");
            D2.Add("8010", "压力传感器清零");

            D1.Add("8011", "ZERO FLOW SENSOR");
            D2.Add("8011", "流量传感器清零");

            D1.Add("8012", "IS THE SYSTEM FREE OF LIQUID? PRESSURE SENSORS SHOULD BE CALIBRATED DRY.");
            D2.Add("8012", "确保系统管路中没有液体,压力传感器应在管内没有液体时校准.");

            D1.Add("8013", "PASSIVE FILLING OF PRIMING FLUID. ACTIVE VENTING OF BUBBLE FROM SYSTEM.     DO NOT START PUMP DRY.");
            D2.Add("8013", "将预冲液体灌入管路中,灌输完之后,启动泵将管内气泡排出.注意：不要在没有液体的状况下启动泵.");

            D1.Add("8014", "ENSURE ZERO FLOW IN SYSTEM AND THAT PUMP IS STOPPED. SET CLAMPS TO FLOW SENSOR.");
            D2.Add("8014", "停止泵后确保系统管路中液体没有流动时再校准.可将夹子夹住流量传感器管道的两端来确保液体不再流动.");

            D1.Add("8015", "SET ZERO");
            D2.Add("8015", "清零");

            D1.Add("8016", "FLOW");
            D2.Add("8016", "流量");

            D1.Add("8017", "SPEED");
            D2.Add("8017", "转速");

            D1.Add("8018", "BATTERY REMAINING");
            D2.Add("8018", "电池预计可用");

            D1.Add("8019", "ΔPmem");
            D2.Add("8019", "跨膜压差");

            D1.Add("8020", "Arterial T2");
            D2.Add("8020", "T2动脉温度");

            D1.Add("8021", "START TIME");
            D2.Add("8021", "开机时间");

            D1.Add("8022", "RUN TIME");
            D2.Add("8022", "运行时长");

            D1.Add("8023", "STOP WATCH");
            D2.Add("8023", "定时器");

            D1.Add("8024", "MUTE ALARM");
            D2.Add("8024", "静音");

            D1.Add("8025", "MIN");
            D2.Add("8025", "分");

            D1.Add("8026", "HOUR");
            D2.Add("8026", "时");

            D1.Add("8027", "DAY");
            D2.Add("8027", "日");

            D1.Add("8028", "WEEK");
            D2.Add("8028", "周");

            D1.Add("8029", "MENU");
            D2.Add("8029", "菜单");

            D1.Add("8030", "PARAMETER CONFIGURATION");
            D2.Add("8030", "参数设置");

            D1.Add("8031", "GENERAL SETTING");
            D2.Add("8031", "通用设置");

            D1.Add("8032", "SYSTEM INFO");
            D2.Add("8032", "系统信息");

            D1.Add("8033", "BUBBLE SENSOR");
            D2.Add("8033", "气泡");

            D1.Add("8034", "PRESSURE");
            D2.Add("8034", "压力");

            D1.Add("8035", "O₂ SATURATION");
            D2.Add("8035", "氧饱和度");

            D1.Add("8036", "HEMOGLOBIN");
            D2.Add("8036", "血红蛋白");

            D1.Add("8037", "PARTIAL PRESSURE O₂");
            D2.Add("8037", "氧分压");

            D1.Add("8038", "TEMPERATURE");
            D2.Add("8038", "温度");

            D1.Add("8039", "RESET SETTING");
            D2.Add("8039", "恢复默认设置");

            D1.Add("8040", "SET ALARM LIMITS");
            D2.Add("8040", "报警限设置");

            D1.Add("8041", "SET");
            D2.Add("8041", "设置");

            D1.Add("8042", "LOWER LIMIT");
            D2.Add("8042", "下限");

            D1.Add("8043", "UPPER LIMIT");
            D2.Add("8043", "上限");

            D1.Add("8044", "ACTIVATE ALARM");
            D2.Add("8044", "启用报警");

            D1.Add("8045", "DEACTIVATE ALARM");
            D2.Add("8045", "禁用报警");

            D1.Add("8046", "OTHER CONFIGURATION");
            D2.Add("8046", "其它");

            D1.Add("8047", "SET ALARM");
            D2.Add("8047", "报警设置");

            D1.Add("8048", "PRE-PUMP PRESSURE");
            D2.Add("8048", "P1泵前压力");

            D1.Add("8049", "PRE-OXY PRESSURE");
            D2.Add("8049", "P2膜前压力");

            D1.Add("8050", "POST-OXY PRESSURE");
            D2.Add("8050", "P3膜后压力");

            D1.Add("8051", "ΔPmem(P2-P3)");
            D2.Add("8051", "跨膜压差(P2-P3)");

            D1.Add("8052", "Reset to default setting?");
            D2.Add("8052", "是否要重置配置?");

            D1.Add("8053", "YES");
            D2.Add("8053", "是");

            D1.Add("8054", "NO");
            D2.Add("8054", "否");

            D1.Add("8055", "LANGUAGE");
            D2.Add("8055", "语言");

            D1.Add("8056", "DISPLAY");
            D2.Add("8056", "显示");

            D1.Add("8057", "VOLUME");
            D2.Add("8057", "音量");

            D1.Add("8058", "TIME & DATE");
            D2.Add("8058", "日期");

            D1.Add("8059", "LOCK SETTING");
            D2.Add("8059", "锁屏");

            D1.Add("8060", "DATA & UPGRADE");
            D2.Add("8060", "数据和升级");

            D1.Add("8061", "SET SCREEN BRIGHTNESS");
            D2.Add("8061", "设置屏幕亮度");

            D1.Add("8062", "SET SPEAKER VOLUME");
            D2.Add("8062", "设置音量");

            D1.Add("8063", "RESET RUN TIME");
            D2.Add("8063", "重置系统运行时间");

            D1.Add("8064", "SET SYSTEM DATE & TIME");
            D2.Add("8064", "设置系统日期和时间");

            D1.Add("8065", "YEAR");
            D2.Add("8065", "年");

            D1.Add("8066", "MONTH");
            D2.Add("8066", "月");

            D1.Add("8067", "DAY");
            D2.Add("8067", "日");

            D1.Add("8068", "HOUR");
            D2.Add("8068", "时");

            D1.Add("8069", "MINUTE");
            D2.Add("8069", "分");

            D1.Add("8070", "SECOND");
            D2.Add("8070", "秒");

            D1.Add("8071", "RESET");
            D2.Add("8071", "重置");

            D1.Add("8072", "SET LOCK TIME");
            D2.Add("8072", "设置锁屏时间");

            D1.Add("8073", "SET STOPWATCH");
            D2.Add("8073", "设置定时器");

            D1.Add("8074", "CURRENT STOPWATCH");
            D2.Add("8074", "当前定时器");

            D1.Add("8075", "START");
            D2.Add("8075", "开始");

            D1.Add("8076", "STOP");
            D2.Add("8076", "结束");

            D1.Add("8077", "USB DEVICE IS READY");
            D2.Add("8077", "USB设备准备就绪");

            D1.Add("8078", "NO USB DEVICE DETECTED");
            D2.Add("8078", "没有发现USB设备");

            D1.Add("8079", "ALARM HISTORY");
            D2.Add("8079", "历史报警");

            D1.Add("8080", "SYSTEM INFO");
            D2.Add("8080", "系统信息");

            D1.Add("8081", "BATTERY INFO");
            D2.Add("8081", "电池信息");

            D1.Add("8082", "ALARM DETAIL");
            D2.Add("8082", "报警详情");

            D1.Add("8083", "Help message for %1");
            D2.Add("8083", "%1 的帮助信息");

            D1.Add("8084", "BOTTOM BATTERY");
            D2.Add("8084", "下电池");

            D1.Add("8085", "TOP BATTERY");
            D2.Add("8085", "上电池");

            D1.Add("8086", "STATUS");
            D2.Add("8086", "状态");

            D1.Add("8087", "ERROR");
            D2.Add("8087", "错误码");

            D1.Add("8088", "AVAILABLE POWER");
            D2.Add("8088", "可用电量");

            D1.Add("8089", "RATED CAPACITY");
            D2.Add("8089", "额定容量");

            D1.Add("8090", "CURRENT CAPACITY");
            D2.Add("8090", "当前容量");

            D1.Add("8091", "Connected");
            D2.Add("8091", "已连接");

            D1.Add("8092", "Disconnected");
            D2.Add("8092", "未连接");

            D1.Add("8093", "Charging");
            D2.Add("8093", "充电");

            D1.Add("8094", "Discharging");
            D2.Add("8094", "放电");

            D1.Add("8095", "No Error");
            D2.Add("8095", "无错误");

            D1.Add("8096", "SOFTWARE VER.");
            D2.Add("8096", "软件版本");

            D1.Add("8097", "HARDWARE VER.");
            D2.Add("8097", "硬件版本");

            D1.Add("8098", "PROTOCOL VER.");
            D2.Add("8098", "协议版本");

            D1.Add("8099", "DISPLAY PANEL");
            D2.Add("8099", "面板控制");

            D1.Add("8100", "SYSTEM");
            D2.Add("8100", "主机");

            D1.Add("8101", "SYSTEM IS LOCKED, PLEASE UNLOCK FIRST");
            D2.Add("8101", "系统已锁定请先解锁");

            D1.Add("8102", "CONFIRM SYSTEM SHUTDOWN?");
            D2.Add("8102", "确定关闭系统?");

            D1.Add("8103", "PUMP IS RUNNING! CONFIRM PUMP STOP?");
            D2.Add("8103", "泵正在运行! 确定关闭泵?");

            D1.Add("8104", "Venous T1");
            D2.Add("8104", "T1静脉温度");

            D1.Add("8105", "CLEAR ALARM");
            D2.Add("8105", "清除报警");

            D1.Add("8106", "CLEAR");
            D2.Add("8106", "清除");

            D1.Add("8107", "%1min");
            D2.Add("8107", "%1分钟");

            D1.Add("8108", "CLEAR All");
            D2.Add("8108", "清除所有");

            D1.Add("8109", "CONFIRM ALARM DEACTIVATION?");
            D2.Add("8109", "确认禁用报警?");

            D1.Add("8110", "MUTE");
            D2.Add("8110", "声音暂停");

            D1.Add("8111", "UNMUTE");
            D2.Add("8111", "声音开启");

            D1.Add("8112", "Confirm set zero?");
            D2.Add("8112", "确认清零?");

            D1.Add("8113", "Cannot set zero while pump is running");
            D2.Add("8113", "泵在运行时无法清零");

            D1.Add("8177", "Cannot set zero while pump is disconnected");
            D2.Add("8177", "泵断开时无法清零");

            D1.Add("8114", "PULSATILE MODE");
            D2.Add("8114", "搏动模式");

            D1.Add("8115", "ACTIVATE");
            D2.Add("8115", "已启用");

            D1.Add("8116", "DEACTIVATE");
            D2.Add("8116", "已禁用");

            D1.Add("8117", "This event requires no action");
            D2.Add("8117", "本事件无需处理");
            D1.Add("8118", "Event is currently active");
            D2.Add("8118", "本事件活动中");
            D1.Add("8119", "Event has been recovered");
            D2.Add("8119", "本事件已恢复");
            D1.Add("8120", "Event has been handled");
            D2.Add("8120", "本事件已处理");


            D1.Add("8121", "CHECK RED ALARM LIGHT");
            D2.Add("8121", "红色报警灯自检");
            D1.Add("8122", "CHECK ORANGE-YELLOW ALARM LIGHT");
            D2.Add("8122", "橙黄色报警灯自检");
            D1.Add("8123", "AUDIO CHECK");
            D2.Add("8123", "声音自检");
            D1.Add("8124", "CHECK PUMPDRIVER");
            D2.Add("8124", "泵自检");

            D1.Add("8125", "START");
            D2.Add("8125", "启动");
            D1.Add("8126", "SKIP");
            D2.Add("8126", "跳过");

            D1.Add("8127", "IS THE RED ALARM LIGHT VISIBLE ABOVE THE DISPLAY?");
            D2.Add("8127", "请确认屏幕上方红色报警灯是否亮起?");
            D1.Add("8128", "IS THE ORANGE-YELLOW ALARM LIGHT VISIBLE ABOVE THE DISPLAY?");
            D2.Add("8128", "请确认屏幕上方橙黄色报警灯是否亮起?");
            D1.Add("8129", "IS THE \"DI-DI-DI\" SOUND AUDIBLE?");
            D2.Add("8129", "请确认是否听到“嘀嘀嘀”的声音?");
            D1.Add("8130", "PASSIVE FILLING OF PRIMING FLUID. ACTIVE VENTING OF BUBBLE FROM SYSTEM.     DO NOT START PUMP DRY.");
            D2.Add("8130", "请连接泵,并确认泵头未安装 或 已安装并且泵头内充满液体,然后点击启动按钮");

            D1.Add("8131", "SELFCHECK MODE");
            D2.Add("8131", "自检模式");

            D1.Add("8132", "ZERO FLOW MODE");
            D2.Add("8132", "零流模式");

            D1.Add("8133", "REGULATED PRESSURE MODE");
            D2.Add("8133", "稳压模式");

            D1.Add("8134", "EXIT");
            D2.Add("8134", "退出");
            D1.Add("8135", "ENTER");
            D2.Add("8135", "进入");

            D1.Add("8136", "AUTO STOP PUMP WHEN ALARM IS TRIGGERED");
            D2.Add("8136", "报警时自动停泵");

            D1.Add("8137", "PUMP DRIVER");
            D2.Add("8137", "泵驱动");

            D1.Add("8138", "PUMP CONTROL");
            D2.Add("8138", "泵控制");

            D1.Add("8139", "SENSOR HUB");
            D2.Add("8139", "传感器HUB");
            D1.Add("8140", "The pump fails self inspection and cannot be used normally. Please contact the manufacturer for after-sales treatment.");
            D2.Add("8140", "泵自检失败,无法正常使用,请联系厂家进行售后处理.");
            D1.Add("8141", "Running System Self Check, Please Wait...");
            D2.Add("8141", "自检中,请稍候...");

            D1.Add("8142", "SYSTEM LOCKED. CONFIRM UNLOCK?");
            D2.Add("8142", "系统已锁定,是否解锁?");

            D1.Add("8143", "Bubble Size is %1mm");
            D2.Add("8143", "气泡值是：%1mm");

            D1.Add("8144", "Display.U");
            D2.Add("8144", "升级面板");
            D1.Add("8145", "Module.U");
            D2.Add("8145", "升级模块");
            D1.Add("8146", "Export data");
            D2.Add("8146", "导出数据");

            D1.Add("8147", "Battery depleted");
            D2.Add("8147", "电池电量耗尽");

            D1.Add("8148", "Battery offline");
            D2.Add("8148", "电池离线");

            D1.Add("8149", "Battery Error");
            D2.Add("8149", "电池故障");

            D1.Add("8150", "If backflow is detected, pump speed is automatically regulated to maintain zero flow.");
            D2.Add("8150", "如有反流,则进入零流量状态.");

            D1.Add("8151", "System upgrade cannot be performed while pump is running");
            D2.Add("8151", "泵在运行,不能升级！");

            D1.Add("8152", "Please insert an USB Storage Device");
            D2.Add("8152", "请插入U盘！");

            D1.Add("8153", "Upgrade started");
            D2.Add("8153", "升级开始");

            D1.Add("8154", "Failed to open source file");
            D2.Add("8154", "源文件打开失败！");

            D1.Add("8155", "Failed to open destination file");
            D2.Add("8155", "目标文件打开失败！");

            D1.Add("8156", "Destination file is corrupted");
            D2.Add("8156", "目标文件损坏！");

            //    D1.Add("8157", "Copy 'ecmo.db'&'Ecmo-Logs' to USB!");
            //    D2.Add("8157", "已拷贝ecmo.db和Ecmo-Logs到U盘！");
            D1.Add("8157", "Copied 'ecmo.db' to USB device");
            D2.Add("8157", "已拷贝ecmo.db到U盘！");

            D1.Add("8158", "STOP TIMER");
            D2.Add("8158", "点击停止定时器");

            D1.Add("8159", "CLEAR ALARM");
            D2.Add("8159", "清除报警");

            D1.Add("8160", "Confirm activating zero flow mode?");
            D2.Add("8160", "是否进入零流量状态?");

            D1.Add("8161", "Operation interrupted");
            D2.Add("8161", "操作被打断！");

            D1.Add("8162", "Copy start");
            D2.Add("8162", "拷贝开始");

            D1.Add("8163", "U-Disc Status");
            D2.Add("8163", "优盘状态");

            D1.Add("8164", "OK");
            D2.Add("8164", "好");

            D1.Add("8165", "Fully Charged");
            D2.Add("8165", "已充满电");

            D1.Add("8166", "Module not connected!");
            D2.Add("8166", "模块未连接!");

            D1.Add("8167", "This event has been handled");
            D2.Add("8167", "本事件已处理");

            D1.Add("8168", "Flow zeroed successfully");
            D2.Add("8168", "流量清零成功");

            D1.Add("8169", "Failed to zero flow");
            D2.Add("8169", "流量清零失败");

            D1.Add("8170", "RECOVERY MODE");
            D2.Add("8170", "恢复模式");
            D1.Add("8171", "RESTORE PREVIOUS OPERATING PARAMETERS? IF YES IS SELECTED, PRESSURE MEASUREMENT MAY BECOME INACCURATE.");
            D2.Add("8171", "是否恢复之前运行模式? 如果选择是, 压力测量值可能不准确。");

            D1.Add("8172", "SWITCH MODE");
            D2.Add("8172", "切换模式");
            D1.Add("8173", "PUMP IS ALREADY RUNNING, SKIP SELF-TEST? SELECT YES TO SKIP SELF-TEST BUT PRESSURE MEASUREMENT MAY BECOME INACCURATE. IF NO IS SELECTED, THE PUMP WILL STOP AND SELF-CHECK WILL RUN.");
            //"IS IT OPERATING IN EMERGENCY MODE? IF YES, THE PRESSURE MAY BE INACCURATE, IF NOT, THE PUMP WILL BE STOPPED!");
            D2.Add("8173", "泵已在运行，是否跳过自检？选择是，则跳过自检，压力测量值可能不准。选择否，则停泵进入自检。");

            D1.Add("8174", "Flow zeroing...");
            D2.Add("8174", "流量清零中...");

            D1.Add("8175", "MAINTENANCE");
            D2.Add("8175", "维护");

            D1.Add("8176", "TOUCHSCREEN CALIBRATION");
            D2.Add("8176", "触摸屏校准");
        }



    }

}
