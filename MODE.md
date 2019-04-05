# Mode
```
# Telegram, 0
31.13.86.0/22
74.86.235.0/24
91.108.56.0/23
91.108.56.0/22
91.108.4.0/22
109.239.140.0/24
149.154.164.0/22
149.154.167.0/24
149.154.168.0/22
149.154.172.0/22
```
- Where `Telegram` represents the name of the rule
- `0` means to use the proxy
- `1` means not to use the proxy

The above rules, then, are expressed as：
- Mode name: `Telegram`
- The following IP CIDR all use proxies, and mismatched requests bypass the agent

```
# Telegram, 1
31.13.86.0/22
74.86.235.0/24
91.108.56.0/23
91.108.56.0/22
91.108.4.0/22
109.239.140.0/24
149.154.164.0/22
149.154.167.0/24
149.154.168.0/22
149.154.172.0/22
```
The above rules, then, are expressed as：
- Mode name：`Telegram`
- The following IP CIDR all bypass the agent, and mismatched requests all use proxies