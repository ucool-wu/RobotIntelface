using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FanucRobot
{
    public class SocketBase
    {
        protected string ipAddr;
        protected int port;
        Socket _sc;
        bool connnected = false;
        public SocketBase(string ip, int port)
        {
            this.ipAddr = ip;
            this.port = port;
        }
        protected ResultMessage CreatResult(bool iserr = false, string msg ="")
        {
            return new ResultMessage(iserr, msg);
        }
        Socket GetAvailableConn()
        {
            if (!connnected)
            {
                _sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _sc.ReceiveTimeout = 2000;
                _sc.SendTimeout = 2000;
                var ar = _sc.BeginConnect(ipAddr, port, null, null);
                if (ar.AsyncWaitHandle.WaitOne(5000))
                {
                    _sc.EndConnect(ar);
                    this.connnected = true;
                }
                else
                {
                    _sc = null;
                }

            }
            return _sc;
        }
        protected ResultMessage Send(byte[] send)
        {
            var socket = GetAvailableConn();
            if (socket != null)
            {
                try
                {
                    socket.Send(send);
                }
                catch
                {
                    this.connnected = false;
                    CreatResult(true, "socket error : send time out.");
                }
                return CreatResult();
            }
            else
            {
                return CreatResult(true, "socket error : connect time out.");
            }
        }
        protected ResultMessage Read(byte[] read)
        {
            var socket = GetAvailableConn();
            if (socket != null)
            {
                try
                {
                    int count = 0;
                    do
                    {
                        Thread.Sleep(20);
                        count += socket.Receive(read, count, socket.Available, SocketFlags.None);
                    } while (socket.Available > 0);
                }
                catch
                {
                    this.connnected = false;
                    return CreatResult(true, "socket error : read time out.");
                }
                return CreatResult();
            }
            else
            {
                return CreatResult(true, "socket error : connect time out.");
            }
        }
        protected ResultMessage SendRead(byte[] send, byte[] read)
        {
            var res = this.Send(send);
            if (res.isError)
            {
                return res;
            }
            else
            {
                return this.Read(read);
            }
        }
        protected void Close()
        {
            this._sc?.Close();
            this._sc.Dispose();
            this.connnected = false;
        }
    }
}
