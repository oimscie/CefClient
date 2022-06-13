using CefSharp;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace CefClient.Camera.ConnectServer
{
    public class Parse
    {
        /// <summary>
        /// 处理原始包,分离码流
        /// </summary>
        public void HkPsParse()
        {
            bool flag = true;
            byte[] temp = null;
            while (StaticResource.CameraVideoIsEnd)
            {
                if (StaticResource.CameraOriginalvideo.Count > 2)
                {
                    if (flag)
                    {
                        StaticResource.CameraOriginalvideo.TryDequeue(out temp);
                        flag = false;
                    }
                    try
                    {
                        int cursor = 0;//游标
                        for (int index= cursor; index < temp.Length-4; index++) {
                            if (temp[index] == 11 && temp[index + 1] == 22 && temp[index + 2] == 33 && temp[index + 3] == 44) {
                                byte[] result = new byte [index- cursor];
                                Buffer.BlockCopy(temp, cursor, result,0, index - cursor);
                                index += 4;
                                cursor = index;
                                GCHandle hObject = GCHandle.Alloc(result, GCHandleType.Pinned);
                                IntPtr pObject = hObject.AddrOfPinnedObject();
                                ValueTuple<IntPtr, uint> ValueTuple = new ValueTuple<IntPtr, uint>
                                {
                                    Item1 = pObject,
                                    Item2 = (uint)result.Length
                                };
                                StaticResource.video.Enqueue(ValueTuple);
                            }
                        }
                        if (cursor<temp.Length) {
                            byte[] temp2 = new Byte[temp.Length-cursor];
                            Buffer.BlockCopy(temp,cursor,temp2,0, temp.Length - cursor);
                            ///取出下一个包
                            if (StaticResource.CameraOriginalvideo.Count > 0)
                            {
                                StaticResource.CameraOriginalvideo.TryDequeue(out byte[] temp3);
                                ///上一包的剩余数据与下一包拼接进入下一循环
                                temp = temp2.Concat(temp3).ToArray();
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    Thread.Sleep(2);
                }
            }

        }
    }
}
