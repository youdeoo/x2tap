# Mode
```
# Telegram, 1, 0
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
- first `1` means to use the proxy
- second `0` means not bypass China

The above rules, then, are expressed as：
- Mode name: `Telegram`
- The following IP CIDR all use proxies, and mismatched requests bypass the proxies
- Not will bypass China

```
# Telegram, 0, 1
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
- Bypass China