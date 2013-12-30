import sys
import random

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

def get_pub(x):
    i = 2
    while i < x:
        if gcd(i,x) == 1:
            return i
        i += 1

def get_big_prime():
    x = 4
    while not is_probable_prime(x):
        x = random.getrandbits(128)
    return x

def inverse(a, n):
    t = 0
    newt = 1
    r = n
    newr = a
    while newr != 0:
        quotient = r/newr
        (t, newt) = (newt, t - quotient * newt)
        (r, newr) = (newr, r - quotient * newr)
    if r > 1:
        return "a is not invertible"
    if t < 0:
        t = t + n
    return t

if __name__ == "__main__":
    print "Started...\n"

    p,q,n,t,e,d = 0,0,0,0,0,0
    p = get_big_prime()
    q = get_big_prime()
    """
    p = 160346447632798703581393784108820773322045169743194904454907780390081360266755852879682718179988946314987671092742738819923324343379897011668985971637874112576479418801430535352468318222095962683527502509189813336112393548040189818281599348732338177949551741150011042091345699583357587653952274908352463659871
    q = 164153838291655929433542331561453106119468352174674935375022573517847487402355653643440314038520209461610902776082781883085247249512771575694136671623928001011189596702191111187120987866982521638958298649558268047578458558527250441653586275213188664835150198560887119006119307673857123638864895871188513496629
    """

    n = p*q
    t = (p-1)*(q-1)
    e = get_pub(t)
    d = inverse(e,n)

    print "p = " + str(p)
    print "q = " + str(q)

    print "p: " + str(is_probable_prime(p))
    print "q: " + str(is_probable_prime(q))

    print "n: " + str(n)
    print "t: " + str(t)
    print "e: " + str(e)
    print "d: " + str(d)
