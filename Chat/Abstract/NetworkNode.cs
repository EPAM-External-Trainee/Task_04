namespace Chat.Abstract
{
    public abstract class NetworkNode
    {
        protected NetworkNode(string ip, int port)
        {
            LocalHostIp = ip;
            LocalHostPort = port;
        }

        protected string LocalHostIp { get; set; }

        protected int LocalHostPort { get; set; }
    }
}