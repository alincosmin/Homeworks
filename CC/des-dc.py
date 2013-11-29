if __name__ == "__main__":
	import sys
	import imp
	import os
	import binascii
	import string
	
	mydes = imp.load_source('des-enc', 'des-enc.py')
	
	#pwd = sys.argv[1].encode("hex")
	pwd = "1A624C89520DEC46"
	
	pairs = list()
	pairs.append(list())
	pairs.append(list())
	pairs.append(list())
	
	"""
	x = binascii.b2a_hex(os.urandom(4))
	pairs[0].append((binascii.b2a_hex(os.urandom(4))+x).upper())
	pairs[0].append((binascii.b2a_hex(os.urandom(4))+x).upper())
	
	x = binascii.b2a_hex(os.urandom(4))
	pairs[1].append((binascii.b2a_hex(os.urandom(4))+x).upper())
	pairs[1].append((binascii.b2a_hex(os.urandom(4))+x).upper())
	
	x = binascii.b2a_hex(os.urandom(4))
	pairs[2].append((binascii.b2a_hex(os.urandom(4))+x).upper())
	pairs[2].append((binascii.b2a_hex(os.urandom(4))+x).upper())
	"""
	
	pairs[0].append("748502cd38451097")
	pairs[0].append("3874756438451097")
	pairs[1].append("486911026acdff31")
	pairs[1].append("375bd31f6acdff31")
	pairs[2].append("357418da013fec86")
	pairs[2].append("12549847013fec86")
	
	pairs2 = list()
	pairs2.append(list())
	pairs2.append(list())
	pairs2.append(list())
	
	pairs2[0].append(mydes.encode(pairs[0][0],pwd,3))
	pairs2[0].append(mydes.encode(pairs[0][1],pwd,3))
	pairs2[1].append(mydes.encode(pairs[1][0],pwd,3))
	pairs2[1].append(mydes.encode(pairs[1][1],pwd,3))
	pairs2[2].append(mydes.encode(pairs[2][0],pwd,3))
	pairs2[2].append(mydes.encode(pairs[2][1],pwd,3))
	
	print "Password:\t" + pwd
	print "\nPairs:"
	print "\t" + pairs[0][0] + "\t" + pairs2[0][0]
	print "\t" + pairs[0][1] + "\t" + pairs2[0][1]
	print "\t" + "-"*16 + "\t" + "-"*16
	print "\t" + pairs[1][0] + "\t" + pairs2[1][0]
	print "\t" + pairs[1][1] + "\t" + pairs2[1][1]
	print "\t" + "-"*16 + "\t" + "-"*16
	print "\t" + pairs[2][0] + "\t" + pairs2[2][0]
	print "\t" + pairs[2][1] + "\t" + pairs2[2][1]