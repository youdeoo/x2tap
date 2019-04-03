#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import hashlib

markdown = '''| 文件名 | MD5 | SHA1 | SHA256 |
| :-: | :-: | :-: | :-: |
| x2tap.x64.7z | {0} | {1} | {2} |
| x2tap.x86.7z | {3} | {4} | {5} |
'''

def hash_file(filename, algorithm):
	with open(filename, 'rb') as f:
		buffer = f.read(4096)
		while len(buffer) > 0:
			algorithm.update(buffer)
			buffer = f.read(1024)
	return algorithm.hexdigest()

def md5_file(filename):
	return hash_file(filename, hashlib.md5())

def sha1_file(filename):
	return hash_file(filename, hashlib.sha1())

def sha256_file(filename):
	return hash_file(filename, hashlib.sha256())

x2tap_x64_location = '..\\x2tap\\bin\\x64\\Release\\x2tap\\x2tap.x64.7z'
x2tap_x86_location = '..\\x2tap\\bin\\x86\\Release\\x2tap\\x2tap.x86.7z'

hashs = [
	md5_file(x2tap_x64_location),
	sha1_file(x2tap_x64_location),
	sha256_file(x2tap_x64_location),
	md5_file(x2tap_x86_location),
	sha1_file(x2tap_x86_location),
	sha256_file(x2tap_x86_location)
]

print(markdown.format(hashs[0], hashs[1], hashs[2], hashs[3], hashs[4], hashs[5]))