using CefClient.SuperSocket;
using CefSharp;
using CefSharp.CarVideo;
using CefSharp.CarVideo.Naudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefClient.CarVideo.ConnectServer
{
    public static class AudioConnect
    {
        public static AudioSocketClient AudioSocket;
        public static void AudioStart(byte[] info, int port) {
            StaticResource.AudioReset();
            StaticResource.AudioIsEnd = true;
            AudioPlay.AudioPaly();
            ProcessData.AudioProcessDataStart();
            AudioRecoder.StartRecording();
            AudioConnectServer(info, port);
        }
        public static void AudioStop()
        {
           // AudioSocket.Send(Encoding.UTF8.GetBytes(StaticResource.Sim + ",0x9102,0"));
            StaticResource.AudioIsEnd = false;
            AudioPlay.waveOutCancellation();
            AudioRecoder.waveInCancellation();
            if (AudioSocket!=null) { AudioSocket.Close(); }
            if (StaticResource.VideoType== "Live") { LiveWindow.audioOpen.Enabled = true; }         
        }

        private static void AudioConnectServer(byte[] info,int port)
        {
            AudioSocket = new AudioSocketClient();
            AudioSocket.ConnectServer(port,info);
        }

    }
}
