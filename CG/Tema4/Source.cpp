#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <fstream>

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

		
	}

	void writePixelLog(int linie, int coloana)
	{
		printf("(%d %d)\n", linie, coloana);
		writePixel(linie, coloana);
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

	void DrawEllipseOutline(float razaX, float razaY)
	{
		float grila_linie = 2. / _linii;
		float grila_coloana = 2. / _coloane;
		const float DEG2RAD = 3.14159 / 180;
		float cosval, sinval;

		float diff_x = (_coloane - 2 * razaX) / (float)_coloane;
		float diff_y = (_linii - 2 * razaY) / (float)_linii;

		glLineWidth(5);
		glColor3f(1.0f, 0.0f, 0.0f);
		glBegin(GL_LINE_LOOP);

		for (int i = 0; i<360; i++)
		{
			float rad = i*DEG2RAD;
			cosval = cos(i*DEG2RAD) * razaX * grila_coloana - diff_x;
			sinval = sin(i*DEG2RAD) * razaY * grila_linie - diff_y;
			glVertex2f(cosval, sinval);
		}

		glEnd();
	}

	void DrawEllipse(float razaY, float razaX)
	{
		int x = 0, y = razaY, k, m;
		double razaX2 = razaX*razaX;
		double razaY2 = razaY*razaY;

		double d1 = razaY2 - razaX2*razaY + razaX2 / 4.0;
		double d2;
		writePixel(x, y);
		
		while (razaX2*(y - 0.5) > razaY2*(x + 1))
		{
			if (d1 < 0)
			{
				d1 += razaY2 * (2 * x + 3);
				x++;
			}
			else 
			{
				d1 += razaY2 * (2 * x + 3) + razaX2*(-2 * y + 2);
				x++;
				y--;
			}

			//writePixel(-x + razaX, -y+razaY);
			k = -x + razaX;
			m = -y + razaY;
			while (k < razaX) { writePixel(++k, m); }
		}

		d2 = razaY2*pow(x + 0.5, 2) + razaX2*pow(y - 1.0, 2) - razaX2*razaY2;
		while (y > 0)
		{
			if (d2 < 0)
			{
				d2 += razaY2 * (2 * x + 2) + razaX2*(-2 * y + 3);
				x++;
				y--;
			}
			else
			{
				d2 += razaX2*(-2 * y + 3);
				y--;
			}

			//writePixelLog(-x+razaX, -y+razaY);
			k = -x + razaX;
			m = -y + razaY;
			while (k < razaX) { writePixel(++k, m); }
		}

		DrawEllipseOutline(razaY, razaX);
	}

	void DrawPolygonOutline(Punct* varfuri, int nr_varfuri)
	{
		float grila_linie = 2. / _linii;
		float grila_coloana = 2. / _coloane;
		
		glLineWidth(5);
		glColor3f(1, 0.1, 0.1);
		glBegin(GL_LINE_LOOP);

		for (int i = 0; i < nr_varfuri; i++)
		{
			glVertex2f(-1 + grila_coloana * varfuri[i].X, -1 + grila_linie * varfuri[i].Y);
		}

		glEnd();
	}

	void DrawPolygon(Punct* varfuri, int nr_varfuri)
	{
		DrawPolygonOutline(varfuri, nr_varfuri);
		// lipseste partea de umplere
	}
};

void DisplayCircle(){
	GrilaCarteziana* grila = new GrilaCarteziana(16, 16);
	grila->Draw();
	grila->writePixel(0, 0);
	grila->DrawCircle(14);
}

void DisplayEllipse(){
	GrilaCarteziana* grila = new GrilaCarteziana(28, 28);
	grila->Draw();
	grila->DrawEllipse(14, 8);
	grila->writePixel(8, 14);
}

int ReadPolygon(char * filename, Punct** output)
{
	*output = new Punct[20];
	ifstream file(filename);
	int nr_varfuri, i;
	file >> nr_varfuri;

	i = 0;
	for (; i < nr_varfuri; i++)
	{
		file >> (*output)[i].X >> (*output)[i].Y;
	}

	return i;
}

void DisplyPolygon(){
	GrilaCarteziana* grila = new GrilaCarteziana(16, 16);
	grila->Draw();

	Punct* varfuriPoligon;
	int varfuri = ReadPolygon("poligon.txt", &varfuriPoligon);
	printf("%d varfuri\n", 6);
	for (int i = 0; i < varfuri; i++)
	{
		printf("- %d, %d\n", varfuriPoligon[i].X, varfuriPoligon[i].Y);
	}

	grila->DrawPolygon(varfuriPoligon, 6);
}

void Display(void) {
	printf("Call Display\n");

	// sterge buffer-ul indicat
	glClear(GL_COLOR_BUFFER_BIT);

	switch (prevKey) {
	case '1':
		DisplayCircle();
		break;
	case '2':
		DisplayEllipse();
		break;
	case '3':
		DisplyPolygon();
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