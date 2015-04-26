#include "Electromotor.h"
#include "Motor.h"
class SistemPornire
{
	Motor& m;
	Electromotor& e;
public:
	SistemPornire(Motor&, Electromotor&);
	void porneste_motor();
	void opreste_motor();
};