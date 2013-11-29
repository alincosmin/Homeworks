import sys
import math

def get_K(t):
		if t >= 60:
			return bin(int("ca62c1d6",16))[2:].zfill(32)
		if t >= 40:
			return bin(int("8f1bbcdc",16))[2:].zfill(32)
		if t >= 20:
			return bin(int("6ed9eba1",16))[2:].zfill(32)
		return bin(int("5a827999",16))[2:].zfill(32)
		
def rotl(x,n):
	return x[n:]+x[:n]
	
def sum5(a1,a2,a3,a4,a5):
    return mysum(a1,mysum(a2,mysum(a3,mysum(a4,a5))))

def hexedwt(w,i):
    return rotl(bin(int(w[i-3],2)^int(w[i-8],2)^int(w[i-14],2)^int(w[i-16],2))[2:].zfill(32),1)

def gen_words(M):
    words = []
    for t in range(16):
        words.append(M[t*32:(t+1)*32])
    for t in range(16,80):
        words.append(hexedwt(words,t))
    return words

def prepare(input):
    msg = input
    l = len(msg)
    k = (448-l-1) % 512;
    msg += '1'+'0'*k
    msg += bin(l)[2:].zfill(64)
    return msg

def mysum(a,b):
    return bin((int(a,2)+int(b,2))%pow(2,32))[2:].zfill(32)

def fct(a1,a2,a3,k):
    if k<20:
        return bin((int(a1,2)&int(a2,2))^(~int(a1,2)&int(a3,2)))[2:].zfill(32)
    if k<40:
        return bin(int(a1,2)^int(a2,2)^int(a3,2))[2:].zfill(32)
    if k<60:
        return bin((int(a1,2)&int(a2,2))^(int(a1,2)&int(a3,2))^(int(a2,2)&int(a3,2)))[2:].zfill(32)
    if k<80:
        return bin(int(a1,2)^int(a2,2)^int(a3,2))[2:].zfill(32)

def my_sha1(input):
    N = len(input)/512
    H = []
    H.append([])
    H[0].append(bin(int("67452301",16))[2:].zfill(32))
    H[0].append(bin(int("efcdab89",16))[2:].zfill(32))
    H[0].append(bin(int("98badcfe",16))[2:].zfill(32))
    H[0].append(bin(int("10325476",16))[2:].zfill(32))
    H[0].append(bin(int("c3d2e1f0",16))[2:].zfill(32))

    for i in range(1,N+1):
        M = input[(i-1)*512:i*512]
        block_words = gen_words(M)

        a = H[i-1][0]
        b = H[i-1][1]
        c = H[i-1][2]
        d = H[i-1][3]
        e = H[i-1][4]

        for t in range(80):
            T = sum5(rotl(a,5),fct(b,c,d,t),e,get_K(t),block_words[t])
            e = d
            d = c
            c = rotl(b,30)
            b = a
            a = T

        H.append([])
        H[i].append(mysum(a,H[i-1][0]))
        H[i].append(mysum(b,H[i-1][1]))
        H[i].append(mysum(c,H[i-1][2]))
        H[i].append(mysum(d,H[i-1][3]))
        H[i].append(mysum(e,H[i-1][4]))

    return hex(int(H[N][0],2))[2:-1].zfill(8)+hex(int(H[N][1],2))[2:-1].zfill(8)+hex(int(H[N][2],2))[2:-1].zfill(8)+hex(int(H[N][3],2))[2:-1].zfill(8)+hex(int(H[N][4],2))[2:-1].zfill(8)
        
if __name__ == "__main__":
        fin = open("input.txt","rb")
        x = fin.read(1)
        message = ""
        while x != "":
                message += bin(ord(x))[2:].zfill(8)
                x = fin.read(1)
        print my_sha1(prepare(message))