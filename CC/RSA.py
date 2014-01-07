import sys
import random
import gmpy2

def is_probable_prime(n):
    _mrpt_num_trials = 5
    assert n >= 2
    if n == 2:
        return True
    if n % 2 == 0:
        return False
    s = 0
    d = n-1
    while True:
        quotient, remainder = divmod(d, 2)
        if remainder == 1:
            break
        s += 1
        d = quotient
    assert(2**s * d == n-1)

    def try_composite(a):
        if pow(a, d, n) == 1:
            return False
        for i in range(s):
            if pow(a, 2**i * d, n) == n-1:
                return False
        return True

    for i in range(_mrpt_num_trials):
        a = random.randrange(2, n)
        if try_composite(a):
            return False

    return True

def gcd(a,b):
    while b != 0:
        t = b
        b = a % b
        a = t
    return a

def get_pub(x,y):
    i = 2
    while i < y:
        if gcd(i,x) == 1:
            return i
        i += 1

def get_big_prime():
    x = 4
    while not is_probable_prime(x):
        x = random.getrandbits(512)
    return x

def inverse(a,b):
    origA = a
    X = 0
    prevX = 1
    Y = 1
    prevY = 0
    while b != 0:
        temp = b
        quotient = a/b
        b = a%b
        a = temp
        temp = X
        a = prevX - quotient * X
        prevX = temp
        temp = Y
        Y = prevY - quotient * Y
        prevY = temp

    return origA + prevY

def encrypt(message, modulo, public=65537):
    aux = message.encode('hex')
    return pow(int(aux,16),public,modulo)

def decrypt(cipher, p, q, public=65537):
    private = gmpy2.invert(public,(p-1)*(q-1))
    dp = private % (p-1)
    dq = private % (q-1)
    qinv = gmpy2.invert(q,p)

    dec1 = pow(cipher,dp,p)
    dec2 = pow(cipher,dq,q)
    h = (qinv *(dec1-dec2)) % p
    decoded = hex(dec2 + h*q)
    return decoded[2:].decode('hex')


if __name__ == "__main__":
    print "Started...\n"

    p = get_big_prime()
    q = get_big_prime()

    target = "Abracadabra"
    print target
    e = 17
    x = encrypt(target,p*q,e)
    print x
    y = decrypt(x,p,q,e)
    print y
