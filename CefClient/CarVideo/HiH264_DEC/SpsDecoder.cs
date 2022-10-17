using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefClient.CarVideo.HiH264_DEC
{
    public class SpsDecoder
    {
        private string debugLog = ConfigurationManager.AppSettings["debugLog"];

        public void log_SPS(SpsNual sps)
        {
            LogHelper.WriteLog("-------------------------------------------------------------------------------");
            LogHelper.WriteLog("[ SPS ]");
            LogHelper.WriteLog("        forbidden_zero_bit = " + sps.forbidden_zero_bit);
            LogHelper.WriteLog("        nal_ref_idc = " + sps.nal_ref_idc);
            LogHelper.WriteLog("        nal_unit_type = " + sps.nal_unit_type);
            LogHelper.WriteLog("        residual_colour_transform_flag = " + sps.residual_colour_transform_flag);
            LogHelper.WriteLog("        offset_for_non_ref_pic = " + sps.offset_for_non_ref_pic);
            LogHelper.WriteLog("        offset_for_ref_frame = " + sps.offset_for_ref_frame);
            LogHelper.WriteLog("        offset_for_top_to_bottom_field = " + sps.offset_for_top_to_bottom_field);
            LogHelper.WriteLog("        constraint_set0_flag = " + sps.constraint_set0_flag);
            LogHelper.WriteLog("        constraint_set1_flag = " + sps.constraint_set1_flag);
            LogHelper.WriteLog("        constraint_set2_flag = " + sps.constraint_set2_flag);
            LogHelper.WriteLog("        constraint_set3_flag = " + sps.constraint_set3_flag);
            LogHelper.WriteLog("        profile_idc = " + sps.profile_idc);
            LogHelper.WriteLog("        level_idc = " + sps.level_idc);
            LogHelper.WriteLog("        seq_parameter_set_id = " + sps.seq_parameter_set_id);
            LogHelper.WriteLog("        chroma_format_idc = " + sps.chroma_format_idc);
            LogHelper.WriteLog("        bit_depth_luma_minus8 = " + sps.bit_depth_luma_minus8);
            LogHelper.WriteLog("        bit_depth_chroma_minus8 = " + sps.bit_depth_chroma_minus8);
            LogHelper.WriteLog("        qpprime_y_zero_transform_bypass_flag = " + sps.qpprime_y_zero_transform_bypass_flag);
            LogHelper.WriteLog("        seq_scaling_matrix_present_flag = " + sps.seq_scaling_matrix_present_flag);
            LogHelper.WriteLog("        log2_max_frame_num_minus4 = " + sps.log2_max_frame_num_minus4);
            LogHelper.WriteLog("        pic_order_cnt_type = " + sps.pic_order_cnt_type);
            LogHelper.WriteLog("        log2_max_pic_order_cnt_lsb_minus4 = " + sps.log2_max_pic_order_cnt_lsb_minus4);
            LogHelper.WriteLog("        max_num_ref_frames = " + sps.max_num_ref_frames);
            LogHelper.WriteLog("        gaps_in_frame_num_value_allowed_flag = " + sps.gaps_in_frame_num_value_allowed_flag);
            LogHelper.WriteLog("        pic_width_in_mbs_minus1 = " + sps.pic_width_in_mbs_minus1);
            LogHelper.WriteLog("        pic_height_in_map_units_minus1 = " + sps.pic_height_in_map_units_minus1);
            LogHelper.WriteLog("        frame_mbs_only_flag = " + sps.frame_mbs_only_flag);
            if (sps.frame_mbs_only_flag == 0)
            {
                LogHelper.WriteLog("        mb_adaptive_frame_field_flag = " + sps.mb_adaptive_frame_field_flag);
            }
            LogHelper.WriteLog("        direct_8x8_inference_flag = " + sps.direct_8x8_inference_flag);
            LogHelper.WriteLog("        frame_cropping_flag = " + sps.frame_cropping_flag);
            if (sps.frame_cropping_flag == 1)
            {
                LogHelper.WriteLog("        frame_crop_left_offset = " + sps.frame_crop_left_offset);
                LogHelper.WriteLog("        frame_crop_right_offset = " + sps.frame_crop_right_offset);
                LogHelper.WriteLog("        frame_crop_top_offset = " + sps.frame_crop_top_offset);
                LogHelper.WriteLog("        frame_crop_bottom_offset = " + sps.frame_crop_bottom_offset);
            }
            LogHelper.WriteLog("        vui_parameters_present_flag = " + sps.vui_parameters_present_flag);
        }

        private static int U(int iBitCount, byte[] bData, ref int iStartBit)
        {
            int iRet = 0;
            for (int i = 0; i < iBitCount; i++)
            {
                iRet = iRet << 1;
                if ((0x80 >> (iStartBit % 8)) == (bData[iStartBit / 8] & (0x80 >> (iStartBit % 8))))
                {
                    iRet += 1;
                }
                iStartBit++;
            }
            return iRet;
        }

        private static UInt32 Ue(byte[] bData, ref int iStartBit)
        {
            //计算0bit的个数
            int nZeroNum = 0;
            while (iStartBit < bData.Length * 8)
            {
                if ((0x80 >> (iStartBit % 8)) == (bData[iStartBit / 8] & (0x80 >> (iStartBit % 8)))) //&:按位与，%取余
                {
                    break;
                }
                nZeroNum++;
                iStartBit++;
            }
            nZeroNum = nZeroNum + 1;
            //计算结果
            UInt32 dwRet = 0;
            for (UInt32 i = 0; i < nZeroNum; i++)
            {
                dwRet <<= 1;
                if ((0x80 >> (iStartBit % 8)) == (bData[iStartBit / 8] & (0x80 >> (iStartBit % 8))))
                {
                    dwRet += 1;
                }
                iStartBit++;
            }
            return dwRet - 1;
        }

        private static Int32 Se(byte[] bData, ref int iStartBit)
        {
            //计算0bit的个数
            int nZeroNum = 0;
            while (iStartBit < bData.Length * 8)
            {
                if ((0x80 >> (iStartBit % 8)) == (bData[iStartBit / 8] & (0x80 >> (iStartBit % 8)))) //&:按位与，%取余
                {
                    break;
                }
                nZeroNum++;
                iStartBit++;
            }
            //计算结果
            Int32 dwRet = 0;
            for (UInt32 i = 0; i < nZeroNum; i++)
            {
                dwRet <<= 1;
                if ((0x80 >> (iStartBit % 8)) == (bData[iStartBit / 8] & (0x80 >> (iStartBit % 8))))
                {
                    dwRet += 1;
                }
                iStartBit++;
            }
            if ((0x80 >> (iStartBit % 8)) == (bData[iStartBit / 8] & (0x80 >> (iStartBit % 8))))
            {
                dwRet = 0 - dwRet;
            }
            iStartBit++;
            return dwRet;
        }

        public SpsNual H264_decode_sps(byte[] bData, ref int width, ref int height)
        {
            SpsNual sps = new SpsNual();
            int StartBit = 0;
            sps.forbidden_zero_bit = U(1, bData, ref StartBit);
            sps.nal_ref_idc = U(2, bData, ref StartBit);
            sps.nal_unit_type = U(5, bData, ref StartBit);
            sps.profile_idc = U(8, bData, ref StartBit);
            sps.constraint_set0_flag = U(1, bData, ref StartBit);//(buf[1] & 0x80)>>7;
            sps.constraint_set1_flag = U(1, bData, ref StartBit);//(buf[1] & 0x40)>>6;
            sps.constraint_set2_flag = U(1, bData, ref StartBit);//(buf[1] & 0x20)>>5;
            sps.constraint_set3_flag = U(1, bData, ref StartBit);//(buf[1] & 0x10)>>4;
            sps.reserved_zero_4bits = U(4, bData, ref StartBit);
            sps.level_idc = U(8, bData, ref StartBit);
            sps.seq_parameter_set_id = Ue(bData, ref StartBit);
            sps.chroma_format_idc = 0;
            if (sps.profile_idc == 100 || sps.profile_idc == 110 || sps.profile_idc == 122 || sps.profile_idc == 244
            || sps.profile_idc == 44 || sps.profile_idc == 83 || sps.profile_idc == 86 || sps.profile_idc == 118 || sps.profile_idc == 128)
            {
                sps.chroma_format_idc = Ue(bData, ref StartBit);
                if (sps.chroma_format_idc == 3)
                {
                    sps.residual_colour_transform_flag = U(1, bData, ref StartBit);
                }
                sps.bit_depth_luma_minus8 = Ue(bData, ref StartBit);
                sps.bit_depth_chroma_minus8 = Ue(bData, ref StartBit);
                sps.qpprime_y_zero_transform_bypass_flag = U(1, bData, ref StartBit);
                sps.seq_scaling_matrix_present_flag = U(1, bData, ref StartBit);
                int[] seq_scaling_list_present_flag = new int[8];
                if (1 == sps.seq_scaling_matrix_present_flag)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        seq_scaling_list_present_flag[i] = U(1, bData, ref StartBit);
                    }
                }
            }
            sps.log2_max_frame_num_minus4 = Ue(bData, ref StartBit);
            sps.pic_order_cnt_type = Ue(bData, ref StartBit);
            if (sps.pic_order_cnt_type == 0)
            {
                sps.log2_max_pic_order_cnt_lsb_minus4 = Ue(bData, ref StartBit);
            }
            else if (sps.pic_order_cnt_type == 1)
            {
                sps.delta_pic_order_always_zero_flag = U(1, bData, ref StartBit);
                sps.offset_for_non_ref_pic = Se(bData, ref StartBit);
                sps.offset_for_top_to_bottom_field = Se(bData, ref StartBit);
                sps.num_ref_frames_in_pic_order_cnt_cycle = Ue(bData, ref StartBit);

                int[] offset_for_ref_frame = new int[sps.num_ref_frames_in_pic_order_cnt_cycle];
                for (int i = 0; i < sps.num_ref_frames_in_pic_order_cnt_cycle; i++)
                    offset_for_ref_frame[i] = Se(bData, ref StartBit);
            }
            sps.max_num_ref_frames = Ue(bData, ref StartBit);
            sps.gaps_in_frame_num_value_allowed_flag = U(1, bData, ref StartBit);
            sps.pic_width_in_mbs_minus1 = Ue(bData, ref StartBit);
            sps.pic_height_in_map_units_minus1 = Ue(bData, ref StartBit);
            int frame_mbs_only_flag = U(1, bData, ref StartBit);
            if (0 == frame_mbs_only_flag)
            {
                sps.mb_adaptive_frame_field_flag = U(1, bData, ref StartBit);
            }
            sps.direct_8x8_inference_flag = U(1, bData, ref StartBit);
            sps.frame_cropping_flag = U(1, bData, ref StartBit);

            sps.frame_crop_left_offset = 0;
            sps.frame_crop_right_offset = 0;
            sps.frame_crop_top_offset = 0;
            sps.frame_crop_bottom_offset = 0;

            if (1 == sps.frame_cropping_flag)
            {
                sps.frame_crop_left_offset = Ue(bData, ref StartBit);
                sps.frame_crop_right_offset = Ue(bData, ref StartBit);
                sps.frame_crop_top_offset = Ue(bData, ref StartBit);
                sps.frame_crop_bottom_offset = Ue(bData, ref StartBit);
            }
            sps.vui_parameters_present_flag = U(1, bData, ref StartBit);
            if (1 == sps.vui_parameters_present_flag)
            {
                int aspect_ratio_info_present_flag = U(1, bData, ref StartBit);
                if (1 == aspect_ratio_info_present_flag)
                {
                    int aspect_ratio_idc = U(8, bData, ref StartBit);
                    if (aspect_ratio_idc == 255)
                    {
                        int sar_width = U(16, bData, ref StartBit);
                        int sar_height = U(16, bData, ref StartBit);
                    }
                }
                int overscan_info_present_flag = U(1, bData, ref StartBit);
                if (1 == overscan_info_present_flag)
                {
                    int overscan_appropriate_flagu = U(1, bData, ref StartBit);
                }
                int video_signal_type_present_flag = U(1, bData, ref StartBit);
                if (1 == video_signal_type_present_flag)
                {
                    int video_format = U(3, bData, ref StartBit);
                    int video_full_range_flag = U(1, bData, ref StartBit);
                    int colour_description_present_flag = U(1, bData, ref StartBit);
                    if (1 == colour_description_present_flag)
                    {
                        int colour_primaries = U(8, bData, ref StartBit);
                        int transfer_characteristics = U(8, bData, ref StartBit);
                        int matrix_coefficients = U(8, bData, ref StartBit);
                    }
                }
                int chroma_loc_info_present_flag = U(1, bData, ref StartBit);
                if (1 == chroma_loc_info_present_flag)
                {
                    UInt32 chroma_sample_loc_type_top_field = Ue(bData, ref StartBit);
                    UInt32 chroma_sample_loc_type_bottom_field = Ue(bData, ref StartBit);
                }
                int timing_info_present_flag = U(1, bData, ref StartBit);

                if (1 == timing_info_present_flag)
                {
                    int num_units_in_tick = U(32, bData, ref StartBit);
                    int time_scale = U(32, bData, ref StartBit);
                    int fixed_frame_rate_flag = U(1, bData, ref StartBit);
                }
            }

            // 宽高计算公式
            width = ((int)sps.pic_width_in_mbs_minus1 + 1) * 16;
            height = (2 - (int)frame_mbs_only_flag) * ((int)sps.pic_height_in_map_units_minus1 + 1) * 16;

            if (1 == sps.frame_cropping_flag)
            {
                int crop_unit_x;
                int crop_unit_y;
                if (0 == sps.chroma_format_idc) // monochrome
                {
                    crop_unit_x = 1;
                    crop_unit_y = 2 - frame_mbs_only_flag;
                }
                else if (1 == sps.chroma_format_idc) // 4:2:0
                {
                    crop_unit_x = 2;
                    crop_unit_y = 2 * (2 - frame_mbs_only_flag);
                }
                else if (2 == sps.chroma_format_idc) // 4:2:2
                {
                    crop_unit_x = 2;
                    crop_unit_y = 2 - frame_mbs_only_flag;
                }
                else // 3 == sps.chroma_format_idc   // 4:4:4
                {
                    crop_unit_x = 1;
                    crop_unit_y = 2 - frame_mbs_only_flag;
                }
                width -= crop_unit_x * ((int)sps.frame_crop_left_offset + (int)sps.frame_crop_right_offset);
                height -= crop_unit_y * ((int)sps.frame_crop_top_offset + (int)sps.frame_crop_bottom_offset);
            }
            if (debugLog == "true")
            {
                log_SPS(sps);
            }
            return sps;
        }
    }
}