using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefClient.CarVideo.HiH264_DEC
{
    public struct SpsNual
    {
        /// <summary>
        /// 禁止位，初始为0，当网络发现NAL单元有比特错误时可设置该比特为1，以便接收方纠错或丢掉该单元。
        /// </summary>
        public int forbidden_zero_bit;

        /// <summary>
        /// nal重要性指示，标志该NAL单元的重要性，值越大，越重要，解码器在解码处理不过来的时候，可以丢掉重要性为0的NALU。
        /// </summary>
        public int nal_ref_idc;

        /// <summary>
        /// NALU类型
        /// </summary>
        public int nal_unit_type;

        /// <summary>
        /// 码流对应的profile
        /// </summary>
        public int profile_idc;

        /// <summary>
        /// 指码流对应的level级
        /// </summary>
        public int level_idc;

        /// <summary>
        /// 指定了由图像参数集指明的序列参数集
        /// </summary>
        public uint seq_parameter_set_id;

        /// <summary>
        /// YUV格式，范围[0,3]，默认1：yuv420。只有profile达到High422，High444，才支持YUV422和YUV444的格式。
        /// </summary>
        public uint chroma_format_idc;

        /// <summary>
        /// 针对YUV444使用，把Y，U，V当做三个独立的图像进行编码。
        /// </summary>
        public int separate_colour_plane_flag;

        /// <summary>
        /// 针对high模式，允许设置图像深度大于8bit的情况。
        /// </summary>
        public uint bit_depth_luma_minus8;

        /// <summary>
        /// 针对high模式，允许设置图像深度大于8bit的情况。
        /// </summary>
        public uint bit_depth_chroma_minus8;

        /// <summary>
        /// 指定 变换系数解码时的 旁路操作 是否要在 去块效应滤波之前执行
        /// </summary>
        public int qpprime_y_zero_transform_bypass_flag;

        /// <summary>
        /// 缩放比例等
        /// </summary>
        public int seq_scaling_list_present_flag;

        /// <summary>
        /// 缩放比例等
        /// </summary>
        public int seq_scaling_matrix_present_flag;

        /// <summary>
        /// 取值范围[0,12]， log2和minus2都是计算方式的前后缀。关键在于 max_frame_num。很明显看出这个表示的是frame_num的max最大值。frame_num使我们在解码过程中会使用到的一个重要变量，表明了解码顺序。
        /// </summary>
        public uint log2_max_frame_num_minus4;

        /// <summary>
        /// 指定计算pic_order_cnt 的 方式，pic_order_cnt 是图像播放的顺序，也是真实录制的顺序，可以理解为真正的时间轴
        /// </summary>
        public uint pic_order_cnt_type;

        /// <summary>
        /// log2和minus4的前后缀是计算方式，主要含义就是 pic_order_cnt的max最大值的lsb，msb是需要解码器自己算的。这个涉及到POC的计算方法。
        /// </summary>
        public uint log2_max_pic_order_cnt_lsb_minus4;

        /// <summary>
        ///用于计算POC
        /// </summary>
        public int delta_pic_order_always_zero_flag;

        /// <summary>
        /// 取值范围int， 用于计算非参考帧的POC
        /// </summary>
        public int offset_for_non_ref_pic;

        /// <summary>
        /// 用于计算场的POC
        /// </summary>
        public int offset_for_top_to_bottom_field;

        /// <summary>
        /// 范围[0,255]，用于计算POC
        /// </summary>
        public uint num_ref_frames_in_pic_order_cnt_cycle;

        /// <summary>
        /// 范围int，用于计算POC
        /// </summary>
        public int offset_for_ref_frame;

        /// <summary>
        /// 规定了可能在视频序列中任何图像帧间预测的解码过程中用到的短期参考帧和长期参考帧、互补参考场对以及不成对的参考场的最大数量
        /// </summary>
        public uint max_num_ref_frames;

        /// <summary>
        /// 允许码流中 frame num是不连续的。
        /// </summary>
        public int gaps_in_frame_num_value_allowed_flag;

        /// <summary>
        /// minus1是后缀，指明图像宽有多少个MB（每个MB是16x16）。这样很容算出图像亮度宽度 = （pic_width_in_mbs_minus1 + 1）* 16。
        /// </summary>
        public uint pic_width_in_mbs_minus1;

        /// <summary>
        /// minus1是后缀，指明图像高有多少个map unit（每个map unit是16x16 或 16x32），所以图像高度 = （pic_height_in_map_units_minus1 + 1）* 16或者32。
        /// </summary>
        public uint pic_height_in_map_units_minus1;

        /// <summary>
        /// 表明是否存在图像场，0代表存在，有场处理起来就比较麻烦。但是从main profile开始就加入了interlace的交错场功能，也算支持比较早。虽然现在用的不多了。
        /// </summary>
        public int frame_mbs_only_flag;

        /// <summary>
        /// 表明是图像帧还是图像场还是帧场自适应。以上两个参数配合，可以推断出图像的帧场属性
        /// </summary>
        public int mb_adaptive_frame_field_flag;

        /// <summary>
        /// 计算B_SKIP, B_direct等宏块的运动矢量使用。
        /// </summary>
        public int direct_8x8_inference_flag;

        /// <summary>
        /// 图像是否需要crop，因为宏块都是16x16的，但是真实的图形不能都是16对齐的，所以有这个标志用来切除多余的边。为0时表示不需要切。
        /// </summary>
        public int frame_cropping_flag;

        /// <summary>
        /// 裁边的参数
        /// </summary>
        public uint frame_crop_left_offset;

        /// <summary>
        /// 裁边的参数
        /// </summary>
        public uint frame_crop_right_offset;

        /// <summary>
        /// 裁边的参数
        /// </summary>
        public uint frame_crop_top_offset;

        /// <summary>
        /// 裁边的参数
        /// </summary>
        public uint frame_crop_bottom_offset;

        /// <summary>
        /// vui_parameters_present_flag等于1表示vui_parameters()在码流中是存在的，vui_parameters_present_flag等于0表明vui_parameters()在码流中不存在
        /// </summary>
        public int vui_parameters_present_flag;

        /// <summary>
        /// 强制使用Baseline profile进行编码
        /// </summary>
        public int constraint_set0_flag;

        /// <summary>
        /// 强制使用Main profile进行编码
        /// </summary>
        public int constraint_set1_flag;

        /// <summary>
        /// 强制使用Extended profile进行编码
        /// </summary>
        public int constraint_set2_flag;

        /// <summary>
        /// 如果 profile_idc等于66、77或88并且level_ idc等于 11，constraint_set3_flag 等于 1 是指该比特流遵从H.264简表与级别中
        /// 对级别1b的所有规定，constraint_set3_flag等于0是指该比特流可以遵从也可以不遵从H.264简表与级别中有关1b级别的所有规定。
        /// 否则（profile_idc等于100、110、122或144或level_ idc不等于 11），constraint_set3_flag 等于1留作未来使用。根据H.264
        /// 标准的规定，当profile_idc等于100、110、122或144或level_ idc不 等于 11时，比特流中constraint_set3_flag 应等于0。当
        /// profile_idc等于100、110、122或144或level_ idc不等于11时，遵从H.264标准的解码器将忽略constraint_set3_flag 的值。
        /// </summary>
        public int constraint_set3_flag;

        /// <summary>
        /// 应等于0。reserved_zero_4bits 的其他取值将由 ITU-T | ISO/IEC 未来规定。解码器将忽略 reserved_zero_4bits 的值。
        /// </summary>
        public int reserved_zero_4bits;

        /// <summary>
        /// 值等于1时，应 用 264文档8.5节规定的残余颜色变换 。
        ///  residual_colour_transform_flag 等于 0 时则不使用残余颜色变换。当 residual_colour_transform_flag 不存在时，默认其
        ///  值为 0
        /// </summary>
        public int residual_colour_transform_flag;
    }
}