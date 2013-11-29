#! /usr/bin/env python

# parolabc == qasomacc :D


import sys
import math

def subkeys(key_str):
	PC1 = (57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4)
	PC2 = (14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32)	
	K = list()
	
	aux = list(bin(int(key_str[:8],16))[2:].zfill(32) + bin(int(key_str[8:],16))[2:].zfill(32))
	
	K.append(list())
	for i in range(64):
		K[0].append('0')
	
	i = 0
	for j in PC1:
		K[0][i] = aux[j-1]
		i += 1
	
	del aux
	
	for i in range(56,64):
		K[0].pop()
	
	L = list()
	R = list()

	L.append("".join(K[0])[:28])
	R.append("".join(K[0])[28:])
	
	for i in range(1,17):
		if i in (1,2,9,16):
			x = 1
		else:
			x = 2		
		L.append(L[i-1][x:]+L[i-1][:x])
		R.append(R[i-1][x:]+R[i-1][:x])
	
	for i in range(1,17):
		K.append(list())
		for j in range(56):
			K[i].append('0')
		
		j = 0
		for k in PC2:
			K[i][j] = ("".join(L[i])+"".join(R[i]))[k-1]
			j += 1
		for j in range(48,56):
			K[i].pop()
		K[i] = "".join(K[i])
			
	return K
	
def fc(right,rkey):
		E = (32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1)
		P = (16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25)
		S = list()
		S.append((14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7, 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8, 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0, 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13))
		S.append((15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10, 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5, 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15, 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9))
		S.append((10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8, 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1, 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7, 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12))
		S.append((7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15, 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9, 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4, 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14))
		S.append((2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9, 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6, 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14, 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3))
		S.append((12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11, 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8, 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6, 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13))
		S.append((4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1, 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6, 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2, 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12))
		S.append((13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7, 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2, 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8, 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11))
		
		exr = list()
		for i in range(48):
			exr.append('0')
		i = 0
		for j in E:
			exr[i] = right[j-1]
			i += 1
			
		RES = bin(int("".join(exr),2)^int(rkey,2))[2:].zfill(48)
		RES2 = ""
		
		for i in range(8):
			block = RES[i*6:i*6+6]
			RES2 += bin(S[i][int(block[0]+block[5],2)*16+int(block[1:5],2)])[2:].zfill(4)
		
		RES = list()
		for i in range(32):
			RES.append('0')
			
		i = 0
		for j in P:
			RES[i] = RES2[j-1]
			i += 1
		return "".join(RES)

def encode(message,key_str,rounds=16):
	K = subkeys(key_str)
	IP = (58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7)
	PI = (40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25)
	M = list(bin(int(message[:8],16))[2:].zfill(32) + bin(int(message[8:],16))[2:].zfill(32))
	
	aux = list()
	for i in range(64):
		aux.append('0')
		
	i = 0
	for j in IP:
		aux[i] = M[j-1]
		i += 1
	
	L = list()
	R = list()
	
	L.append("".join(aux)[:32])
	R.append("".join(aux)[32:])
	
	for i in range(1,rounds+1):
		L.append(R[i-1])
		R.append(bin(int(L[i-1],2)^int(fc(R[i-1],K[i]),2))[2:].zfill(32))
		#print "K" + str(i) + ":\t" + "".join(K[i])
	
	M = list(R[rounds]+L[rounds])
	aux = list()
	for i in range(64):
		aux.append('0')
	i = 0
	for j in PI:
		aux[i] = M[j-1]
		i += 1
	
	res = "".join(aux)
	return (hex(int(res[:20],2))[2:].zfill(5)+hex(int(res[20:40],2))[2:].zfill(5)+hex(int(res[40:],2))[2:].zfill(6)).upper().zfill(16)
	
	
def xor_hex(hex1,hex2):
		return hex(int(hex1[:5],16)^(int(hex2[:5],16)))[2:].zfill(5)+hex(int(hex1[5:10],16)^(int(hex2[5:10],16)))[2:].zfill(5)+hex(int(hex1[10:],16)^(int(hex2[10:],16)))[2:].zfill(6)
	
def cbc_encode(block,key_str):
		cipher_block = ""
		vector = key_str
		for i in range(len(block)/16):
			vector = encode(xor_hex(block[i*16:i*16+16],vector),key_str)
			cipher_block += vector
		return cipher_block
		

if __name__ == "__main__":		
	if len(sys.argv) != 3:
		print "Syntax: " + sys.argv[0] + " MESSAGE KEY"
		exit()

	if len(sys.argv[2]) > 8:
		print "Key can't be lengthier than 8."
		exit()

	msg = sys.argv[1].encode('hex')
	key = sys.argv[2].encode('hex')

	if len(key) < 16:
		key = key.zfill(16)

	msg = msg.zfill(int(math.ceil(len(msg)/16.0))*16)
		
	print "Message:"
	if len(msg) != 16:
		for i in range(len(msg)/16):
			print "\t" + msg[i*16:i*16+16]
	else:
		print "\t" + msg
	print ""
	print "Key:\t" + key
	print ""
	print "Cipher:"
	aux = ""
	if len(msg) != 16:
		cipher = cbc_encode(msg,key)
		for i in range(len(cipher)/16):
				print "\t" + cipher[(i*16):(i*16+16)]
				aux += cipher[(i*16):(i*16+16)].decode('hex')
	else:
		print "\t" + encode(msg,key)

