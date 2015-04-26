class Autoturism
{
	Electromotor electromotor; //autoturismul poseda un electromotor
	Motor motor; //autoturismul poseda un motor
	SistemPornire sistem_pornire; //autoturismul poseda un sistem de pornire
public:
	Autoturism();
	void porneste_autoturism();
	void condu_la_destinatie();
	void parcheaza_autoturism();
};