# x2tap 1.0 技术核心
1. x2tap 其实只是一个配置文件生成器、订阅管理器而已
2. x2tap 负责将 tun2socks、shadowsocksr-native、v2ray-core 整合起来，促使他们能够变成一个支持 UDP 转发的工具
3. shadowsocksr-native 和 v2ray-core 都能够创建一个 Socks5 入口，然后处理后交给远端服务器
4. tun2socks 负责操作虚拟网卡，将数据转换成 Socks5 的协议然后交给 shadowsocksr-native 或者 v2ray-core 的入口，由他们处理数据
5. x2tap 在启动 tun2socks 后，会修改路由表，将远端服务器的地址解析成 IP，然后打入路由表，指向出口网关
6. 在将远端服务器的路由写完之后，会写令一个路由规则来使当前机器的所有数据都走虚拟网卡上去
7. 此时，现在的网卡数据都已经走向虚拟网卡，tun2socks 会将 TCP、UDP 数据转换成 Socks5 协议交给 shadowsocksr-native 或者 v2ray-core 处理

这就是整个 x2tap 的核心内容了吧

*如果有不详细，或者写的不好的地方，请上 Telegram 或者直接 issue 问我！*

**欢迎提交 PR 改进此项目，x2tap 2.0 正在构建中！**