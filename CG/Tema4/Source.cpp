#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#include "glut.h"

using namespace std;

unsigned char prevKey;

//template <typename T>
//void Swap<T>(T& a, T& b) {
//	T aux = a;
//	a = b;
//	b = aux;
//}

typedef struct { int X; int Y; } Punct;

class GrilaCarteziana
{
	int _linii, _coloane;

public:

	GrilaCarteziana(int linii, int coloane)
	{
		_linii = linii;
		_coloane = coloane;
	}

	void Draw()
	{
		float grila_linie = 2. / _linii;
		float grila_coloana = 2. / _coloane;

		glColor3f(0.0, 0.0, 0.0);
		glLineWidth(1);
		for (int i = 0; i<_linii; i++)
		{
			glBegin(GL_LINES);
			glVertex2f(-1, -1 + grila_linie*i);
			glVertex2f(1, -1 + grila_linie*i);
			glEnd();
		}
		for (int j = 0; j< _coloane; j++){
			glBegin(GL_LINES);
			glVertex2f(-1 + grila_coloana*j, -1);
			glVertex2f(-1 + grila_coloana*j, 1);
			glEnd();
		}
	}

	void writePixel(int linie, int coloana)
	{
		const float DEG2RAD = 3.14159 / 180;
		float grila_linie = 2. / _linii;
		float grila_coloana = 2. / _coloane;

		glPolygonMode(GL_FRONT, GL_FILL);
		glBegin(GL_POLYGON);
		glColor3f(0.0, 0.0, 0.4);

		float cosval, sinval;

		for (int i = 0; i < 360; i++)
		{
			float degInRad = i*DEG2RAD;
			cosval = cos(degInRad)*0.05f + (-1 + grila_coloana * coloana);
			sinval = sin(degInRad)*0.04f + (-1 + grila_linie * linie);

			glVertex2f(cosval, sinval);
		}
		glEnd();

		//printf("(%d %d)\n", linie, coloana);
	}

	void DrawLine(int ax, int ay, int bx, int by){
		Punct a, b;
		a.X = ax;
		a.Y = ay;
		b.X = bx;
		b.Y = by;

		DrawLine(a, b);
	}

	void DrawLine(Punct punctA, Punct punctB){
		float grila_linie = 2. / _linii;
		float grila_coloana = 2. / _coloane;

		Punct punctC, punctD;

		int d, dE, dNE, dSE, dN, dS;
		if (punctA.X > punctB.X)
		{
			Punct aux = punctA;
			punctA = punctB;
			punctB = aux;
		}

		punctD.X = punctB.X - punctA.X;
		punctD.Y = punctB.Y - punctA.Y;

		punctC = punctA;

		if (punctD.Y >= 0)
		{
			if (abs(punctD.Y) <= abs(punctD.X))
			{
				d = 2 * punctD.Y - punctD.X;
				dE = 2 * punctD.Y;
				dNE = 2 * (punctD.Y - punctD.X);

				while (punctC.X <= punctB.X)
				{
					writePixel(punctC.Y, punctC.X);
					if (d <= 0)
					{
						d += dE;
						punctC.X++;
					}
					else
					{
						d += dNE;
						punctC.X++;
						punctC.Y++;
					}
				}
			}
			else
			{
				d = punctD.Y - 2 * punctD.X;
				dN = -2 * punctD.X;
				dNE = 2 * (punctD.Y - punctD.X);

				while (punctC.Y <= punctB.Y)
				{
					writePixel(punctC.Y, punctC.X);
					if (d > 0)
					{
						d += dN;
						punctC.Y++;
					}
					else
					{
						d += dNE;
						punctC.X++;
						punctC.Y++;
					}
				}
			}
		}
		else
		{
			if (abs(punctD.Y) <= abs(punctD.X))
			{
				d = 2 * punctD.Y + punctD.X;
				dE = 2 * punctD.Y;
				dSE = 2 * (punctD.Y + punctD.X);

				while (punctC.X <= punctB.X)
				{
					writePixel(punctC.Y, punctC.X);
					if (d > 0)
					{
						d += dE;
						punctC.X++;
					}
					else
					{
						d += dSE;
						punctC.X++;
						punctC.Y--;
					}
				}
			}
			else
			{
				d = punctD.Y + 2 * punctD.X;
				dS = 2 * punctD.X;
				dSE = 2 * (punctD.Y + punctD.X);

				while (punctC.Y >= punctB.Y)
				{
					writePixel(punctC.Y, punctC.X);
					if (d <= 0)
					{
						d += dS;
						punctC.Y--;
					}
					else
					{
						d += dSE;
						punctC.X++;
						punctC.Y--;
					}
				}
			}
		}
		glLineWidth(5);
		glColor3f(1, 0.1, 0.1);
		glBegin(GL_LINES);
		glVertex2f(-1 + grila_coloana * punctA.X, -1 + grila_linie * punctA.Y);
		glVertex2f(-1 + grila_coloana * punctB.X, -1 + grila_linie * punctB.Y);
		glEnd();
	}

	void DrawCircleArc(int raza)
	{
		float grila_linie = 2. / _linii;
		float grila_coloana = 2. / _coloane;
		const float DEG2RAD = 3.14159 / 180;
		float cosval, sinval;

		glLineWidth(5);
		glColor3f(1.0f, 0.0f, 0.0f);
		glBegin(GL_LINE_STRIP);

		cosval = (float)raza * cos(359 * DEG2RAD);
		sinval = (float)raza * sin(359 * DEG2RAD);
		for (int j = 0; j < 360; j++)
		{
			cosval = (float)raza * cos(j * DEG2RAD) * grila_coloana - 1;
			sinval = (float)raza * sin(j * DEG2RAD) * grila_linie - 1;
			glVertex2f(cosval, sinval);
		}
		glEnd();
	}

	void DrawCirclePoints(int x, int y)
	{
		writePixel(x, y);
		writePixel(x, y - 1);
		writePixel(x, y + 1);
	}

	void DrawCircle(int raza)
	{
		int x = 0, y = raza;
		double d = 5. / 4 - raza;
		
		DrawCirclePoints(x, y);
		while (y > x)
		{
			
			if (d < 0)
			{
				d += 2 * x + 3;
				x++;
			}
			else
			{
				d += 2 * (x - y) + 5;
				x++;
				y--;
			}
			DrawCirclePoints(x, y);
		}

		DrawCircleArc(raza);
	}
};

void DisplayLines(){
	GrilaCarteziana* grila = new GrilaCarteziana(16, 16);
	grila->Draw();
	grila->writePixel(0, 0);
	
	grila->DrawLine(0, 16, 16, 11);
	grila->DrawLine(0, 0, 16, 7);
}

void DisplayCircle(){
	GrilaCarteziana* grila = new GrilaCarteziana(16, 16);
	grila->Draw();
	grila->writePixel(0, 0);

	grila->DrawCircle(14);
}

void Display(void) {
	printf("Call Display\n");

	// sterge buffer-ul indicat
	glClear(GL_COLOR_BUFFER_BIT);

	switch (prevKey) {
	case '1':
		DisplayLines();
		break;
	case '2':
		DisplayCircle();
		break;
	default:
		break;
	}

	// forteaza redesenarea imaginii
	glFlush();
}

void Init(void) {
	glClearColor(1.0, 1.0, 1.0, 1.0);
	glLineWidth(2);
	glPointSize(4);
	glPolygonMode(GL_FRONT, GL_LINE);
}

void Reshape(int w, int h) {
	//printf("Call Reshape : latime = %d, inaltime = %d\n", w, h);
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
}

void KeyboardFunc(unsigned char key, int x, int y) {
	//printf("Ati tastat <%c>. Mouse-ul este in pozitia %d, %d.\n", key, x, y);

	prevKey = key;
	if (key == 27) // escape
		exit(0);
	glutPostRedisplay();
}

int main(int argc, char** argv) {

	glutInit(&argc, argv);
	glutInitWindowSize(300, 300);
	glutInitWindowPosition(100, 100);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutCreateWindow(argv[0]);
	Init();
	glutReshapeFunc(Reshape);
	glutKeyboardFunc(KeyboardFunc);
	glutDisplayFunc(Display);
	glutMainLoop();

	return 0;
}