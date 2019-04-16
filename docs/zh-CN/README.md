[![](https://img.shields.io/badge/telegram-channel-blue.svg)](https://t.me/x2tap)
[![](https://img.shields.io/badge/telegram-chat-blue.svg)](https://t.me/x2tapChat)
[![](https://img.shields.io/badge/status-testing-red.svg)](https://github.com/hacking001/x2tap/releases)
[![](https://travis-ci.org/hacking001/x2tap.svg?branch=master)](https://travis-ci.org/hacking001/x2tap)

# x2tap
基于 TUN/TAP、tun2socks、v2ray 实现的 VPN 工具

**最新测试版发布于 [releases](https://github.com/hacking001/x2tap/releases) 中**

支持 Shadowsocks 和 VMess 协议的代理（通过 v2ray 实现）
# TODO
- [x] 导入订阅（Shadowsocks、VMess）
- [x] 直接使用 Windows API 获取流量信息
- [x] 外置规则列表（参见 [MODE.md](MODE.md)）
- [x] 本地 DNS 代理
- [x] 添加 ShadowsocksR 支持
- [x] 自定义 DNS 设置
- [ ] 添加 Socks5 支持
- [ ] 为 Shadowsocks 增加 OBFS 插件支持
- [ ] 全局例外 IP
- [ ] 增加 `SSD://` URI 支持
- [ ] IPv6 转发支持

# 依赖
- [TAP-Windows](https://build.openvpn.net/downloads/releases/latest/tap-windows-latest-stable.exe)
- [shadowsocksr-native](https://github.com/ShadowsocksR-Live/shadowsocksr-native)
- [v2ray-core](https://github.com/v2ray/v2ray-core/releases)
- [dnscrypt-proxy](https://github.com/jedisct1/dnscrypt-proxy)
- [tun2socks](https://github.com/hacking001/x2tap/tree/master/binaries/)

# 截图
![](screenshots/main.png)

# 编译指南
参见 [BUILD.md](BUILD.md)

# 仓库镜像
- [Github](https://github.com/hacking001/x2tap)
- [GitLab](https://gitlab.com/hacking001/x2tap)

# 用户协议
- 请在遵循当地法律的情况下使用，不得用于违法用途